using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Plant.Mom.Api.Entities;

public class LightingConfiguration
{
    public LightingConfiguration()
    {
        LightingDaySchedules = new List<LightingDaySchedule>();
        LightingTimeSchedules = new List<LightingTimeSchedule>();
    }

    public int Id { get; set; }
    [JsonIgnore]
    public int MomConfigurationId { get; set; }
    public string? Name { get; set; }

    [JsonProperty("days")]
    public IList<LightingDaySchedule> LightingDaySchedules { get; set; }
    [JsonProperty("times")]
    public IList<LightingTimeSchedule> LightingTimeSchedules { get; set; }

    [JsonIgnore]
    public bool Valid => !string.IsNullOrEmpty(Name)
        && LightingDaySchedules != null
        && LightingTimeSchedules != null
        && !LightingDaySchedules.Any()
        && !LightingTimeSchedules.Any();
}

public class LightingTimeSchedule
{
    public int Id { get; set; }
    [JsonIgnore]
    public int LightingConfigurationId { get; set; }
    public string? From { get; set; }
    public string? To { get; set; }

    [JsonIgnore]
    public bool Valid => !string.IsNullOrWhiteSpace(From) && !string.IsNullOrWhiteSpace(To);
}

public class LightingDaySchedule
{
    public int Id { get; set; }
    [JsonIgnore]
    public int LightingConfigurationId { get; set; }
    public bool Monday { get; set; }
    public bool Tuesday { get; set; }
    public bool Wednesday { get; set; }
    public bool Thursday { get; set; }
    public bool Friday { get; set; }
    public bool Saturday { get; set; }
    public bool Sunday { get; set; }
}