namespace SnivysUltimatePackageOneConfig.Configs
{
    public class VoteConfig
    {
        public bool IsEnabled { get; set; } = true;

        public ushort MapBroadcastTime { get; set; } = 10;

        public string MapBroadcastText { get; set; } =
            "<size=30>A vote has started!. Use .vote in player console [~]. Prompt: %prompt%, Vote: %option1%, %option2%, %option3%, %option4%, %option5%";

        public float VoteDuration { get; set; } = 30f;
    }
}