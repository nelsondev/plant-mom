using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Plant.Mom.Api.Entities;

public class LightingConfiguration
{
    public LightingConfiguration()
    {
        LightingDaySchedule = new();
        LightingTimeSchedules = new List<LightingTimeSchedule>();
    }

    public int Id { get; set; }
    [JsonIgnore]
    public int MomConfigurationId { get; set; }
    [JsonIgnore]
    public int LightingDayScheduleId { get; set; }
    public string? Name { get; set; }
    public bool Enabled { get; set; }

    [JsonProperty("days")]
    public LightingDaySchedule LightingDaySchedule { get; set; }
    [JsonProperty("times")]
    public IList<LightingTimeSchedule> LightingTimeSchedules { get; set; }

    [JsonIgnore]
    public bool Valid => !string.IsNullOrEmpty(Name)
        && LightingDaySchedule != null
        && LightingTimeSchedules != null
        && !LightingTimeSchedules.Any();
}

public class LightingTimeSchedule
{
    public LightingTimeSchedule()
    {
        From = "00:00";
        To = "00:00";
    }

    public int Id { get; set; }
    [JsonIgnore]
    public int LightingConfigurationId { get; set; }
    public string From { get; set; }
    public string To { get; set; }

    [JsonIgnore]
    public bool Valid => !string.IsNullOrWhiteSpace(From) && !string.IsNullOrWhiteSpace(To);

    public bool IsEnabled 
    { 
        get
        {
            TimeSpan start = TimeSpan.Parse(From);
            TimeSpan end = TimeSpan.Parse(To);
            TimeSpan now = DateTime.Now.TimeOfDay;

            return (now > start) && (now < end);
        }
    }
}

public class LightingDaySchedule
{
    public int Id { get; set; }
    public bool Monday { get; set; }
    public bool Tuesday { get; set; }
    public bool Wednesday { get; set; }
    public bool Thursday { get; set; }
    public bool Friday { get; set; }
    public bool Saturday { get; set; }
    public bool Sunday { get; set; }

    public bool IsEnabled
    {
        get
        {
            if (Monday && DateTime.Now.DayOfWeek == DayOfWeek.Monday) return true;
            if (Tuesday && DateTime.Now.DayOfWeek == DayOfWeek.Tuesday) return true;
            if (Wednesday && DateTime.Now.DayOfWeek == DayOfWeek.Wednesday) return true;
            if (Friday && DateTime.Now.DayOfWeek == DayOfWeek.Friday) return true;
            if (Saturday && DateTime.Now.DayOfWeek == DayOfWeek.Saturday) return true;
            if (Sunday && DateTime.Now.DayOfWeek == DayOfWeek.Sunday) return true;
            return false;
        }
    }
}