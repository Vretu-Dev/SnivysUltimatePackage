namespace SnivysUltimatePackageOneConfig.Configs.ServerEventsConfigs
{
    public class AfterHoursConfig
    {
        public string StartEventCassieMessage { get; set; } = "After Hours";
        public string StartEventCassieText { get; set; } = "After Hours. (After Hours event started)";
        public int TeslaActivationChance { get; set; } = 25;
        public int IntercomTime { get; set; } = 5;
    }
}