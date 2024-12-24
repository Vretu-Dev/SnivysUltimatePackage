namespace SnivysUltimatePackage.Configs
{
    public class Scp1576SpectatorViewerConfig
    {
        public bool IsEnabled { get; set; } = true;
        public string Scp1576Text { get; set; } = "<size=24><align=left>Spectators: %spectators%\nTime before next spawn wave: %timebeforespawnwave% seconds</align></size>";
        public float Scp1576TextDuration { get; set; } = 15f;
    }
} 