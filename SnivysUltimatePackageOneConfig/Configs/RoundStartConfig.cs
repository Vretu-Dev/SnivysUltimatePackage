using System.ComponentModel;

namespace SnivysUltimatePackageOneConfig.Configs
{
    public class RoundStartConfig
    {
        public bool IsEnabled { get; set; } = true;
        
        [Description("A simple check to see if the Escape Door Final should be opened on round start")]
        public bool EscapeDoorOpen { get; set; } = true;

        [Description("Should the Escape Door Final remained locked on round start")]
        public bool EscapeDoorLock { get; set; } = true;
        
        [Description("Allows for the amount of respawns to be adjusted by round start")]
        public bool AdjustRespawnTokens { get; set; } = true;

        [Description("The amount of respawn tokens to add to the MTF side on round start")]
        public int AddMtfRespawnTokens { get; set; } = 1;
        
        [Description("The amount of respawn tokens to add to the Chaos side on round start")]
        public int AddCiRespawnTokens { get; set; } = 1;

        [Description("Allows for decontamination changes to be made")]
        public bool DecontaminationChanges { get; set; } = false;

        [Description("The time for decontamination to be set to, by default the time is 705 seconds (11 Minutes 45 Seconds, base game), adding 195 will make it 900 seconds (15 Minutes)")]
        public float DecontaminationTime { get; set; } = 195f;

    }
}