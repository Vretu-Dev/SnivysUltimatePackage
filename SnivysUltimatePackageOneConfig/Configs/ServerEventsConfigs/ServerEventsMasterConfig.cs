using System.Collections.Generic;
using System.ComponentModel;

namespace SnivysUltimatePackageOneConfig.Configs.ServerEventsConfigs
{
    public class ServerEventsMasterConfig
    {
        [Description("Is the plugin enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Can events randomly start?")]
        public bool RandomlyStartingEvents { get; set; } = false;

        [Description("What is the chance of the events running?")]
        public int RandomEventStartingChance { get; set; } = 0;

        [Description(
            "The list of events that can randomly start, Valid options: Blackout, 173Infection, 173Hydra, Chaotic, Short, FreezingTemps, NameRedacted, VariableLights")]
        public List<string> RandomEventsAllowedToStart { get; set; } = new List<string>
        {
            "Blackout",
            "173Infection",
            "173Hydra",
            "Chaotic",
            "Short",
            "FreezingTemps",
            "NameRedacted",
            "VariableLights"
        };
        
        
        //Independent Event Configs
        public BlackoutConfig BlackoutConfig { get; set; } = new();
        public ChaoticConfig ChaoticConfig { get; set; } = new();
        public FreezingTemperaturesConfig FreezingTemperaturesConfig { get; set; } = new();
        public NameRedactedConfig NameRedactedConfig { get; set; } = new();
        public PeanutInfectionConfig PeanutInfectionConfig { get; set; } = new();
        public PeanutHydraConfig PeanutHydraConfig { get; set; } = new();
        public ShortConfig ShortConfig { get; set; } = new();
        public VariableLightsConfig VariableLightsConfig { get; set; } = new();
        public AfterHoursConfig AfterHoursConfig { get; set; } = new();
        public SnowballsVsScpsConfig SnowballsVsScpsConfig { get; set; } = new();
        public GravityConfig GravityConfig { get; set; } = new();
    }
}