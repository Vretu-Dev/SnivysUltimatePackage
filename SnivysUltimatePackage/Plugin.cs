using System;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles;
using Exiled.CustomRoles.API.Features;

namespace SnivysUltimatePackage
{
    public class Plugin : Plugin<Config>
    {
        public override PluginPriority Priority { get; } = PluginPriority.Higher;
        public static Plugin Instance;
        public override string Name { get; } = "Snivy's Ultimate Plugin Package";
        public override string Author { get; } = "Vicious Vikki";
        public override string Prefix { get; } = "VVUltimatePluginPackage";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(9, 0, 0,4);

        public override void OnEnabled()
        {
            CustomAbility.RegisterAbilities();
            Instance = this;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            CustomAbility.UnregisterAbilities();
            Instance = null;
            base.OnDisabled();
        }
    }
}