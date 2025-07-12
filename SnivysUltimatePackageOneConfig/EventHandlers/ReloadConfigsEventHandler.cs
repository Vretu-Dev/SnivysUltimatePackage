using Exiled.API.Features;

namespace SnivysUltimatePackageOneConfig.EventHandlers
{
    public class ReloadConfigsEventHandler(Plugin plugin)
    {
        public Plugin Plugin = plugin;
        public static void OnReloadingConfigs()
        {
            Log.Info("VVUP: Reloading configs for Snivy's Ultimate Plugin Package One Config");
            Plugin.Instance.Config.LoadConfigs();
            Log.Info("VVUP: Configs reloaded successfully");
        }
    }
}