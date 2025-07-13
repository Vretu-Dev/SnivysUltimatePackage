using Exiled.API.Features;

namespace SnivysFreeCustomRolesSC
{
    public class ReloadConfigsEventHandler
    {
        public Plugin Plugin;
        public ReloadConfigsEventHandler(Plugin plugin) => Plugin = plugin;
        public static void OnReloadingConfigs()
        {
            Log.Info("VVUP: Reloading configs for Snivy's Free Custom Roles Split Config");
            Plugin.Instance.Config.LoadConfigs();
            Log.Info("VVUP: Configs reloaded successfully");
        }
    }
}