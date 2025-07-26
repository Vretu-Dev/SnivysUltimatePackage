using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;
using Exiled.Loader;
using VVUP.CustomRoles.API;
using VVUP.CustomRoles.EventHandlers;
using Server = Exiled.Events.Handlers.Server;
using Scp049Events = Exiled.Events.Handlers.Scp049;

namespace VVUP.CustomRoles
{
    public class Plugin : Plugin<CustomRolesConfig>
    {
        public override PluginPriority Priority { get; } = PluginPriority.Low;
        public static Plugin Instance;
        public override string Name { get; } = "VVUP: Custom Roles";
        public override string Author { get; } = "Vicious Vikki";
        public override string Prefix { get; } = "VVUP.CR";
        public override Version Version { get; } = new Version(3, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(9, 6, 1);
        
        public Dictionary<StartTeam, List<ICustomRole>> Roles { get; } = new();
        public CustomRoleEventHandler CustomRoleEventHandler;

        public override void OnEnabled()
        {
            Instance = this;
            if (!Loader.Plugins.Any(plugin => plugin.Prefix == "VVUP.Base"))
            {
                Log.Error("VVUP Base Plugin is not present, disabling module");
                base.OnDisabled();
                return;
            }
            
            CustomRoleEventHandler = new CustomRoleEventHandler(this);
            CustomRolesConfig.ContainmentScientists.Register();
            CustomRolesConfig.LightGuards.Register();
            CustomRolesConfig.Biochemists.Register();
            CustomRolesConfig.ContainmentGuards.Register();
            CustomRolesConfig.BorderPatrols.Register();
            CustomRolesConfig.Nightfalls.Register();
            CustomRolesConfig.A7Chaoss.Register();
            CustomRolesConfig.Flippeds.Register();
            CustomRolesConfig.TelepathicChaos.Register();
            CustomRolesConfig.JuggernautChaos.Register();
            CustomRolesConfig.CISpies.Register();
            CustomRolesConfig.MtfWisps.Register();
            CustomRolesConfig.DwarfZombies.Register();
            CustomRolesConfig.ExplosiveZombies.Register();
            CustomRolesConfig.CiPhantoms.Register();
            CustomRolesConfig.MedicZombies.Register();
            CustomRolesConfig.LockpickingClassDs.Register();
            CustomRolesConfig.Demolitionists.Register();
            CustomRolesConfig.Vanguards.Register();
            CustomRolesConfig.TheoreticalPhysicistScientists.Register();
            CustomRolesConfig.MtfParamedics.Register();
            CustomRolesConfig.ClassDAnalysts.Register();
            CustomRolesConfig.ClassDTanks.Register();
            CustomRolesConfig.InfectedZombies.Register();
            CustomRolesConfig.PoisonousZombies.Register();
            CustomRolesConfig.SpeedsterZombies.Register();
            CustomRolesConfig.TeleportZombies.Register();

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

                    if (!Roles.ContainsKey(team))
                        Roles.Add(team, new());

                    for (int i = 0; i < role.SpawnProperties.Limit; i++)
                        Roles[team].Add(custom);
                    Log.Debug($"Roles {team} now has {Roles[team].Count} elements.");
                }
            }

            Server.RoundStarted += CustomRoleEventHandler.OnRoundStarted;
            Server.RespawningTeam += CustomRoleEventHandler.OnRespawningTeam;
            Scp049Events.FinishingRecall += CustomRoleEventHandler.FinishingRecall;
            Base.Plugin.Instance.VvupCr = true;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            CustomRole.UnregisterRoles();
            Server.RoundStarted -= CustomRoleEventHandler.OnRoundStarted;
            Server.RespawningTeam -= CustomRoleEventHandler.OnRespawningTeam;
            Scp049Events.FinishingRecall -= CustomRoleEventHandler.FinishingRecall;
            Base.Plugin.Instance.VvupCr = false;
            CustomRoleEventHandler = null;
            Instance = null;
            base.OnDisabled();
        }
    }
}