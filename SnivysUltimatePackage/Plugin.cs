using System;
using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;
using SnivysUltimatePackage.API;
using SnivysUltimatePackage.Configs;
using SnivysUltimatePackage.EventHandlers;
using SnivysUltimatePackage.EventHandlers.Custom;
using SnivysUltimatePackage.EventHandlers.ServerEventsEventHandlers;

using Player = Exiled.Events.Handlers.Player;
using Scp049Events = Exiled.Events.Handlers.Scp049;
using Server = Exiled.Events.Handlers.Server;

namespace SnivysUltimatePackage
{
    public class Plugin : Plugin<MasterConfig>
    {
        public override PluginPriority Priority { get; } = PluginPriority.Higher;
        public static Plugin Instance;
        public override string Name { get; } = "Snivy's Ultimate Plugin Package";
        public override string Author { get; } = "Vicious Vikki";
        public override string Prefix { get; } = "VVUltimatePluginPackage";
        public override Version Version { get; } = new Version(1, 4, 0);
        public override Version RequiredExiledVersion { get; } = new Version(9, 0, 1);
        public static int ActiveEvent = 0;
        
        public Dictionary<StartTeam, List<ICustomRole>> Roles { get; } = new();
        
        public CustomRoleEventHandler CustomRoleEventHandler;
        public ServerEventsMainEventHandler ServerEventsMainEventHandler;
        public MicroDamageReductionEventHandler MicroDamageReductionEventHandler;
        public MicroEvaporateEventHandlers MicroEvaporateEventHandlers;
        public FlamingoAdjustmentEventHandlers FlamingoAdjustmentEventHandlers;
        public EscapeDoorOpenerEventHandlers EscapeDoorOpenerEventHandlers;

        public override void OnEnabled()
        {
            Instance = this;
            
            if(Instance.Config.CustomItemsConfig.IsEnabled)
                CustomItem.RegisterItems();

            if (Instance.Config.CustomRolesConfig.IsEnabled)
            {
                CustomRoleEventHandler = new CustomRoleEventHandler(this);
                
                Config.CustomRolesConfig.ContainmentScientists.Register();
                Config.CustomRolesConfig.LightGuards.Register();
                Config.CustomRolesConfig.Biochemists.Register();
                Config.CustomRolesConfig.ContainmentGuards.Register();
                Config.CustomRolesConfig.BorderPatrols.Register();
                Config.CustomRolesConfig.Nightfalls.Register();
                Config.CustomRolesConfig.A7Chaoss.Register();
                Config.CustomRolesConfig.Flippeds.Register();
                Config.CustomRolesConfig.TelepathicChaos.Register();
                Config.CustomRolesConfig.JuggernautChaos.Register();
                Config.CustomRolesConfig.CISpies.Register();
                Config.CustomRolesConfig.MtfWisps.Register();
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
            }

            if (Instance.Config.CustomRolesConfig.IsEnabled)
                CustomAbility.RegisterAbilities();

            if (Instance.Config.ServerEventsMasterConfig.IsEnabled)
            {
                ServerEventsMainEventHandler = new ServerEventsMainEventHandler(this);
                if(Instance.Config.ServerEventsMasterConfig.RandomlyStartingEvents)
                    Server.RoundStarted += ServerEventsMainEventHandler.OnRoundStart;
                Server.RoundEnded += ServerEventsMainEventHandler.OnEndingRound;
                Server.WaitingForPlayers += ServerEventsMainEventHandler.OnWaitingForPlayers;
            }

            if (Instance.Config.MicroDamageReductionConfig.IsEnabled)
            {
                MicroDamageReductionEventHandler = new MicroDamageReductionEventHandler(this);
                Player.Hurting += MicroDamageReductionEventHandler.OnPlayerHurting;
            }

            if (Instance.Config.MicroEvaporateConfig.IsEnabled)
            {
                MicroEvaporateEventHandlers = new MicroEvaporateEventHandlers(this);
                Player.Dying += MicroEvaporateEventHandlers.OnDying;
            }

            if (Instance.Config.FlamingoAdjustmentsConfig.IsEnabled)
            {
                FlamingoAdjustmentEventHandlers = new FlamingoAdjustmentEventHandlers(this);
                Player.Hurting += FlamingoAdjustmentEventHandlers.OnHurting;
            }

            if (Instance.Config.EscapeDoorOpenerConfig.IsEnabled)
            {
                EscapeDoorOpenerEventHandlers = new EscapeDoorOpenerEventHandlers(this);
            }
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            CustomItem.UnregisterItems();
            CustomRole.UnregisterRoles();
            CustomAbility.UnregisterAbilities();

            if (Instance.Config.CustomRolesConfig.IsEnabled)
            {
                Server.RoundStarted -= CustomRoleEventHandler.OnRoundStarted;
                Server.RespawningTeam -= CustomRoleEventHandler.OnRespawningTeam;
                Scp049Events.FinishingRecall -= CustomRoleEventHandler.FinishingRecall;

                CustomRoleEventHandler = null;
            }

            if (Instance.Config.ServerEventsMasterConfig.IsEnabled)
            {
                Server.RoundEnded -= ServerEventsMainEventHandler.OnEndingRound;
                Server.WaitingForPlayers -= ServerEventsMainEventHandler.OnWaitingForPlayers;
                ServerEventsMainEventHandler = null;
            }

            if (Instance.Config.MicroDamageReductionConfig.IsEnabled)
            {
                Player.Hurting -= MicroDamageReductionEventHandler.OnPlayerHurting;
                MicroDamageReductionEventHandler = null;
            }
            
            if (Instance.Config.MicroEvaporateConfig.IsEnabled)
            {
                Player.Dying -= MicroEvaporateEventHandlers.OnDying;
                MicroEvaporateEventHandlers = null;
            }
            
            if (Instance.Config.FlamingoAdjustmentsConfig.IsEnabled)
            {
                FlamingoAdjustmentEventHandlers = null;
            }
            
            if (Instance.Config.EscapeDoorOpenerConfig.IsEnabled)
            {
                EscapeDoorOpenerEventHandlers = null;
            }

            Instance = null;
            base.OnDisabled();
        }
    }
}