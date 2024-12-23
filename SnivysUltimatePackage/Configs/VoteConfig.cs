namespace SnivysUltimatePackage.Configs
{
    public class VoteConfig
    {
        public static bool IsEnabled { get; set; } = true;

        public static ushort MapBroadcastTime { get; set; } = 10;

        public static string MapBroadcastText { get; set; } =
            "<size=30>A vote has started!. Use .vote in player console [~]";

        public static float VoteDuration { get; set; } = 30f;
    }
}