using System.ComponentModel;

namespace SnivysUltimatePackage.Configs.ServerEventsConfigs
{
    public class OperationCrossfireConfig
    {
        [Description("How often should the event check for updates? (in Seconds)")]
        public float CheckForEventsInterval { get; set; } = 1f;
        
        [Description("The ratios of MTF, Scientist, and D-Class that spawns during the event. Must add up to 1")]
        public float MtfRatio { get; set; } = 0.5f;
        public float ScientistRatio { get; set; } = 0.25f;
        public float DClassRatio { get; set; } = 0.25f;

        [Description("What should the broadcast say for someone who connects during the event?")]
        public string PlayerConnectDuringEventMessage { get; set; } =
            "There's an active event going on, please hang tight.";
        [Description("How long should this be displayed to the user?")]
        public float PlayerConnectDuringEventMessageDisplayDuration { get; set; } = 10f;

        public uint PrototypeKeycardBasicId { get; set; } = 44;

        public float ScientistPercentageRequiredToWin { get; set; } = 0.5f;
        
        public float MtfPercentageRequiredToWin { get; set; } = 0.2f;

        public string MtfScientistWinMessage { get; set; } =
            "MTF and Scientists has successfully completed their objectives";

        public string ClassDWinMessage { get; set; } =
            "Class-D has successfully caused enough disturbance to the MTF and Scientists";
        public string TieMessage { get; set; } = "Both sides has been unable to complete their objectives";

        [Description("How long should the victory message be displayed for a victory condition?")]
        public int EndOfRoundTime { get; set; } = 10;
        [Description("How long should the event last? (This uses the nuke timer)")]
        public int EventDuration { get; set; } = 1800;

        public string MtfScientistObjective1 { get; set; } = "Find and refine prototype keycard";
        public string MtfScientistObjective2 { get; set; } = "Unlock SCP-914";
        public string MtfObjective3 { get; set; } = "Escort Scientists out of the facility";
        public string ScientistObjective3 { get; set; } = "Escape the facility";
        public string ClassDObjective1 { get; set; } = "Find and hold the prototype keycard";
        public string ClassDObjective2 { get; set; } = "Kill MTF and Scientists";

        public int StartingBroadcastTime { get; set; } = 10;
    }
}