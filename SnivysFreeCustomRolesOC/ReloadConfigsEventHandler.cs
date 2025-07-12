using Exiled.API.Features;

namespace SnivysFreeCustomRolesOC
{
    public class ReloadConfigsEventHandler(Plugin plugin)
    {
        public Plugin Plugin = plugin;
        public static void OnReloadingConfigs()
        {
            Log.Info("VVUP: Reloading configs for Snivy's Free Custom Roles One Config");
            Plugin.Instance.Config.LoadConfigs();
            Log.Info("VVUP: Configs reloaded successfully");
        }
    }
}