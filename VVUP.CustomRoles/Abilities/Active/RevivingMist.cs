using System;
using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using UnityEngine;

namespace VVUP.CustomRoles.Abilities.Active
{
    [CustomAbility]
    public class RevivingMist : ActiveAbility
    {
        private readonly List<CoroutineHandle> coroutines = new();
        private readonly Dictionary<Player, DeathInfo> recentlyDead = new();
        
        public override string Name { get; set; } = "Reviving Mist";
        public override string Description { get; set; } = "Creates a mist that heals allies and revives recently fallen teammates";
        public override float Duration { get; set; } = 1f;
        public override float Cooldown { get; set; } = 180f;
        public float ReviveRadius { get; set; } = 5f;
        public float ReviveTimeWindow { get; set; } = 30f;
        public float ReviveHealthPercent { get; set; } = 30f;
        public string ReviveMessage { get; set; } = "You have been revived by a Paramedic!";
        public ushort ReviveMessageTime { get; set; } = 5;
        public bool ReviveTeammatesOnly { get; set; } = true;
        public bool GrantLoadoutOnRevive { get; set; } = false;
        [Description("If true, the player will be revived to a set role, if false, it will revive to their original role.")]
        public bool ReviveToSetRole { get; set; } = false;
        [Description("If ReviveToSetRole is true, this will determine if they revive to a custom role. Otherwise it will be a regular role.")]
        public bool ReviveToCustomRole { get; set; } = false;
        public RoleTypeId ReviveBaseRole { get; set; } = RoleTypeId.Tutorial;
        public uint ReviveCustomRoleId { get; set; } = 25;

        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();
            Exiled.Events.Handlers.Player.Died += OnPlayerDied;
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Died -= OnPlayerDied;
            foreach (CoroutineHandle handle in coroutines)
                Timing.KillCoroutines(handle);
            base.UnsubscribeEvents();
        }

        private void OnPlayerDied(DiedEventArgs ev)
        {
            // Store info about dead players
            recentlyDead[ev.Player] = new DeathInfo
            {
                DeathTime = DateTime.Now,
                Role = ev.Player.Role.Type,
                Position = ev.Player.Position,
                Side = ev.Player.Role.Side
            };
        }

        protected override void AbilityUsed(Player player)
        {
            CheckForDeadTeammates(player);
        }
        
        private void CheckForDeadTeammates(Player activator)
        {
            List<Player> toRemove = new List<Player>();
            
            foreach (var deadPlayer in recentlyDead)
            {
                if (ReviveTeammatesOnly && deadPlayer.Value.Side != activator.Role.Side)
                    continue;
                
                TimeSpan timeSinceDeath = DateTime.Now - deadPlayer.Value.DeathTime;
                if (timeSinceDeath.TotalSeconds > ReviveTimeWindow)
                {
                    toRemove.Add(deadPlayer.Key);
                    continue;
                }
                
                float distanceSquared = (deadPlayer.Value.Position - activator.Position).sqrMagnitude;
                if (distanceSquared > ReviveRadius * ReviveRadius)
                    continue;
                
                Player player = deadPlayer.Key;
                if (ReviveToSetRole)
                {
                    if (ReviveToCustomRole)
                    {
                        CustomRole.Get(ReviveCustomRoleId)?.AddRole(player);
                        player.Position = deadPlayer.Value.Position;
                        if (!GrantLoadoutOnRevive)
                            player.ClearInventory();
                        Log.Debug($"VVUP Custom Abilities: Reviving Mist: {player.Nickname} has been revived to custom role id {ReviveCustomRoleId} at position {deadPlayer.Value.Position}");
                    }
                    else
                    {
                        player.Role.Set(ReviveBaseRole, GrantLoadoutOnRevive ? RoleSpawnFlags.AssignInventory : RoleSpawnFlags.None);
                        Log.Debug($"VVUP Custom Abilities: Reviving Mist: {player.Nickname} has been revived to role {ReviveBaseRole} at position {deadPlayer.Value.Position}");
                    }
                }
                else
                {
                    player.Role.Set(deadPlayer.Value.Role, GrantLoadoutOnRevive ? RoleSpawnFlags.AssignInventory : RoleSpawnFlags.None);
                    Log.Debug($"VVUP Custom Abilities: Reviving Mist: {player.Nickname} has been revived to their original role {deadPlayer.Value.Role} at position {deadPlayer.Value.Position}");
                }
                player.Health = player.MaxHealth * (ReviveHealthPercent / 100f);
                player.Broadcast(ReviveMessageTime, ReviveMessage);
                Log.Debug($"VVUP Custom Abilities: Reviving Mist: Showing revive message to {player.Nickname}: {ReviveMessage}");
                toRemove.Add(player);
            }
            
            // Clean up the dictionary
            foreach (var player in toRemove)
                recentlyDead.Remove(player);
        }
        
        private class DeathInfo
        {
            public DateTime DeathTime { get; set; }
            public RoleTypeId Role { get; set; }
            public Vector3 Position { get; set; }
            public Side Side { get; set; }
        }
    }
}