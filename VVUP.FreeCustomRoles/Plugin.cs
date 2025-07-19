using System;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;
using Exiled.Loader;
using VVUP.CustomRoles.API;

namespace VVUP.FreeCustomRoles
{
    public class Plugin : Plugin<MasterConfig>
    {
        public override PluginPriority Priority { get; } = PluginPriority.Lower;
        public static Plugin Instance;
        public override string Name { get; } = "VVUP: Free Custom Roles";
        public override string Author { get; } = "Vicious Vikki";
        public override string Prefix { get; } = "VVUP.FCR";
        public override Version Version { get; } = Base.Plugin.Instance.Version;
        public override Version RequiredExiledVersion { get; } = Base.Plugin.Instance.RequiredExiledVersion;

        public override void OnEnabled()
        {
            Instance = this;
            if (!Loader.Plugins.Any(plugin => plugin.Prefix == "VVUP.Base"))
            {
                Log.Error("VVUP Free Custom Roles: Base Plugin is not present, disabling module");
                base.OnDisabled();
                return;
            }
            if (!Loader.Plugins.Any(plugin => plugin.Prefix == "VVUP.CustomRoles"))
            {
                Log.Error("VVUP Free Custom Roles: Custom Roles Module is not present, disabling module");
                base.OnDisabled();
                return;
            }

            Config.LoadConfigs();
            
            Config.FreeCustomRoles1.Register();
            Config.FreeCustomRoles2.Register();
            Config.FreeCustomRoles3.Register();
            Config.FreeCustomRoles4.Register();
            Config.FreeCustomRoles5.Register();
            Config.FreeCustomRoles6.Register();
            Config.FreeCustomRoles7.Register();
            Config.FreeCustomRoles8.Register();
            Config.FreeCustomRoles9.Register();
            Config.FreeCustomRoles10.Register();
            Config.FreeCustomRoles11.Register();
            Config.FreeCustomRoles12.Register();
            Config.FreeCustomRoles13.Register();
            Config.FreeCustomRoles14.Register();
            Config.FreeCustomRoles15.Register();
            Config.FreeCustomRoles16.Register();
            Config.FreeCustomRoles17.Register();
            Config.FreeCustomRoles18.Register();
            Config.FreeCustomRoles19.Register();
            Config.FreeCustomRoles20.Register();
            
            foreach (CustomRole role in CustomRole.Registered)
            {
                if (role is ICustomRole custom)
                {
                    Log.Debug($"Adding {role.Name} to dictionary..");
                    StartTeam team;
                    if (custom.StartTeam.HasFlag(StartTeam.Chaos))
                        team = StartTeam.Chaos;
                    else if (custom.StartTeam.HasFlag(StartTeam.Guard))
                        team = StartTeam.Guard;
                    else if (custom.StartTeam.HasFlag(StartTeam.Ntf))
                        team = StartTeam.Ntf;
                    else if (custom.StartTeam.HasFlag(StartTeam.Scientist))
                        team = StartTeam.Scientist;
                    else if (custom.StartTeam.HasFlag(StartTeam.ClassD))
                        team = StartTeam.ClassD;
                    else if (custom.StartTeam.HasFlag(StartTeam.Scp))
                        team = StartTeam.Scp;
                    else
                        team = StartTeam.Other;

                    if (!CustomRoles.Plugin.Instance.Roles.ContainsKey(team))
                        CustomRoles.Plugin.Instance.Roles.Add(team, new());

                    for (int i = 0; i < role.SpawnProperties.Limit; i++)
                        CustomRoles.Plugin.Instance.Roles[team].Add(custom);
                    Log.Debug($"Roles {team} now has {CustomRoles.Plugin.Instance.Roles[team].Count} elements.");
                }
            }
            Base.Plugin.Instance.VvupFcr = true;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            CustomRole.UnregisterRoles();
            Base.Plugin.Instance.VvupFcr = false;
            Instance = null;
            base.OnDisabled();
        }
    }
}