namespace SnivysUltimatePackage.Configs
{
    public class VoteConfig
    {
        public bool IsEnabled { get; set; } = true;

        public ushort MapBroadcastTime { get; set; } = 10;

        public string MapBroadcastText { get; set; } =
            "<size=30>A vote has started!. Use .vote in player console [~]";

        public float VoteDuration { get; set; } = 30f;
    }
}