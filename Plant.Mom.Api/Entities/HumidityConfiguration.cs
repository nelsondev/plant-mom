using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Plant.Mom.Api.Entities;

public class HumidityConfiguration
{
    public HumidityConfiguration()
    {
        HumidityDaySchedules = new List<HumidityDaySchedule>();
        HumidityTimeSchedules = new List<HumidityTimeSchedule>();
    }

    public int Id { get; set; }
    [JsonIgnore]
    public int MomConfigurationId { get; set; }
    public string? Name { get; set; }
    public double High { get; set; }
    public double Low { get; set; }

    [JsonProperty("days")]
    public IList<HumidityDaySchedule> HumidityDaySchedules { get; set; }
    [JsonProperty("times")]
    public IList<HumidityTimeSchedule> HumidityTimeSchedules { get; set; }

    [JsonIgnore]
    public bool Valid => !string.IsNullOrEmpty(Name)
        && HumidityDaySchedules != null
        && HumidityTimeSchedules != null
        && !HumidityDaySchedules.Any()
        && !HumidityTimeSchedules.Any();
}

public class HumidityTimeSchedule
{
    public int Id { get; set; }
    [JsonIgnore]
    public int HumidityConfigurationId { get; set; }
    public string? From { get; set; }
    public string? To { get; set; }

    [JsonIgnore]
    public bool Valid => !string.IsNullOrWhiteSpace(From) && !string.IsNullOrWhiteSpace(To);
}

public class HumidityDaySchedule
{
    public int Id { get; set; }
    [JsonIgnore]
    public int HumidityConfigurationId { get; set; }
    public bool Monday { get; set; }
    public bool Tuesday { get; set; }
    public bool Wednesday { get; set; }
    public bool Thursday { get; set; }
    public bool Friday { get; set; }
    public bool Saturday { get; set; }
    public bool Sunday { get; set; }
}