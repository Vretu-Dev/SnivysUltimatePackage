using System;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Loader;
using VVUP.ServerEvents.ServerEventsEventHandlers;

namespace VVUP.OperationCrossfireServerEvent
{
    public class Plugin : Plugin<OperationCrossfireConfig>
    {
        public override PluginPriority Priority { get; } = PluginPriority.Lower;
        public static Plugin Instance;
        public override string Name => "VVUP: Operation Crossfire Server Event";
        public override string Author { get; } = "Vicious Vikki";
        public override string Prefix { get; } = "VVUP.OperationCrossfireServerEvent";
        public override Version Version { get; } = Base.Plugin.Instance.Version;
        public override Version RequiredExiledVersion { get; } = Base.Plugin.Instance.RequiredExiledVersion;

        public OperationCrossfireEventHandlers OperationCrossfireEventHandlers { get; set; }

        public override void OnEnabled()
        {
            if (!Loader.Plugins.Any(plugin => plugin.Prefix == "VVUP.Base"))
            {
                Log.Error("VVUP: Base Plugin is not present, disabling module");
                base.OnDisabled();
                return;
            }
            if (!Loader.Plugins.Any(plugin => plugin.Prefix == "VVUP.ServerEvents"))
            {
                Log.Error("VVUP: Base Plugin is not present, disabling module");
                base.OnDisabled();
                return;
            }
            Instance = this;
            OperationCrossfireEventHandlers = new OperationCrossfireEventHandlers();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            OperationCrossfireEventHandlers = null;
            Instance = null;
            base.OnDisabled();
        }
    }
}