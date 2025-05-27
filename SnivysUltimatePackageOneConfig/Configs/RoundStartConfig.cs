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

        [Description("Allows for decontamination changes to be made")]
        public bool DecontaminationChanges { get; set; } = false;

        [Description("The time for decontamination to be set to, by default the time is 705 seconds (11 Minutes 45 Seconds, base game), adding 195 will make it 900 seconds (15 Minutes)")]
        public float DecontaminationTime { get; set; } = 195f;

    }
}