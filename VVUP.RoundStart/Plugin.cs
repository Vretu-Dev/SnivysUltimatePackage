using System;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Loader;
using Server = Exiled.Events.Handlers.Server;

namespace VVUP.RoundStart
{
    public class Plugin : Plugin<Config>
    {
        public override PluginPriority Priority { get; } = PluginPriority.Low;
        public static Plugin Instance;
        public override string Name { get; } = "VVUP: Round Start";
        public override string Author { get; } = "Vicious Vikki";
        public override string Prefix { get; } = "VVUP.RS";
        public override Version Version { get; } = Base.Plugin.Instance.Version;
        public override Version RequiredExiledVersion { get; } = Base.Plugin.Instance.RequiredExiledVersion;
        public EventHandlers EventHandlers;

        public override void OnEnabled()
        {
            if (!Loader.Plugins.Any(plugin => plugin.Prefix == "VVUP.Base"))
            {
                Log.Error("VVUP: Base Plugin is not present, disabling module");
                base.OnDisabled();
                return;
            }

            Instance = this;
            EventHandlers = new EventHandlers(this);
            Server.RoundStarted += EventHandlers.OnRoundStarted;
            Base.Plugin.Instance.VvupRs = true;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Server.RoundStarted -= EventHandlers.OnRoundStarted;
            EventHandlers = null;
            Instance = null;
            base.OnDisabled();
        }
    }
}