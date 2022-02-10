using Newtonsoft.Json;

namespace Plant.Mom.Api.Entities;

public class MomConfiguration
{
    public MomConfiguration()
    {
        HumidityConfigurations = new List<HumidityConfiguration>();
        LightingConfigurations = new List<LightingConfiguration>();
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public bool Enabled { get; set; }
    public double Temperature { get; set; }

    public IList<HumidityConfiguration> HumidityConfigurations { get; set; }
    public IList<LightingConfiguration> LightingConfigurations { get; set; }

    [JsonIgnore]
    public bool Valid => HumidityConfigurations != null
        && LightingConfigurations != null
        && !HumidityConfigurations.Any()
        && !LightingConfigurations.Any()
        && HumidityConfigurations.Count(x => x.Enabled) == 1
        && LightingConfigurations.Count(x => x.Enabled) == 1
        && !string.IsNullOrEmpty(Name);
}
