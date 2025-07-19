using Exiled.API.Interfaces;

namespace VVUP.ScpChanges
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        
        public string Scp1576Text { get; set; } = "<size=24><align=left>Spectators: %spectators%. Time before next spawn wave: %timebeforespawnwave% seconds</align></size>";
        public float Scp1576TextDuration { get; set; } = 15f;
    }
}