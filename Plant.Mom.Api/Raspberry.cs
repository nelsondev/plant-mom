using Microsoft.EntityFrameworkCore;
using Plant.Mom.Api.Entities;
using System.Device.Gpio;
using System.Device.Analog;
using Iot.Device.DHTxx;
using UnitsNet;

namespace Plant.Mom.Api
{
    public class Raspberry : IHostedService, IDisposable
    {
        private readonly PlantContext _context;
        private Timer? _timer;

        public Raspberry(PlantContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _timer?.Dispose();
            GC.SuppressFinalize(this);
        } 

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(Run, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(30));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void Run(object? state)
        {
            MomConfiguration? configuration = _context.Moms
                .Include(x => x.HumidityConfigurations)
                .ThenInclude(x => x.HumidityDaySchedule)
                .Include(x => x.HumidityConfigurations)
                .ThenInclude(x => x.HumidityTimeSchedules)
                .Include(x => x.LightingConfigurations)
                .ThenInclude(x => x.LightingDaySchedule)
                .Include(x => x.LightingConfigurations)
                .ThenInclude(x => x.LightingTimeSchedules)
                .FirstOrDefault(x => x.Enabled);

            if (configuration == null || !configuration.Valid) return;

            HumidityConfiguration humidity = configuration.HumidityConfigurations.First(x => x.Enabled);
            LightingConfiguration lighting = configuration.LightingConfigurations.First(x => x.Enabled);

            using GpioController controller = new();
            

            // Dummy pins for now
            controller.OpenPin(Const.GPIO_PIN_HUMIDITY);
            controller.OpenPin(Const.GPIO_PIN_LIGHTING);
            controller.OpenPin(Const.GPIO_PIN_HUMIDITY_SENSOR);
            controller.OpenPin(Const.GPIO_PIN_FAN);

            controller.SetPinMode(Const.GPIO_PIN_HUMIDITY_SENSOR, PinMode.Input);

            using Dht11 sensor = new(Const.GPIO_PIN_HUMIDITY_SENSOR, controller.NumberingScheme, controller);
            
            // Control humidity
            if (sensor.TryReadHumidity(out RelativeHumidity sensorHumidity))
            {
                if (humidity.IsEnabled(sensorHumidity.Percent) && humidity.HumidityDaySchedule.IsEnabled && humidity.HumidityTimeSchedules.Any(x => x.IsEnabled))
                {
                    controller.Write(Const.GPIO_PIN_HUMIDITY, PinValue.High);
                    controller.Write(Const.GPIO_PIN_FAN, PinValue.High);
                }
                else
                {
                    controller.Write(Const.GPIO_PIN_HUMIDITY, PinValue.Low);
                    controller.Write(Const.GPIO_PIN_FAN, PinValue.Low);
                }
            }

            // Report temperature
            if (sensor.TryReadTemperature(out Temperature sensorTemperature))
            {
                configuration.Temperature = sensorTemperature.DegreesCelsius;
                _context.SaveChanges();
            }

            // Control lighting
            if (lighting.LightingDaySchedule.IsEnabled && lighting.LightingTimeSchedules.Any(x => x.IsEnabled))
            {
                controller.Write(Const.GPIO_PIN_LIGHTING, PinValue.High);
            }
            else
            {
                controller.Write(Const.GPIO_PIN_LIGHTING, PinValue.Low);
            }
        }
    }
}
