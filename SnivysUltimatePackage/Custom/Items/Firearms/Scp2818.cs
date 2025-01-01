using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using PlayerStatsSystem;
using UnityEngine;

namespace SnivysUltimatePackage.Custom.Items.Firearms
{
    [CustomItem(ItemType.GunE11SR)]
    public class Scp2818 : CustomWeapon
    {
        public override uint Id { get; set; } = 33;
        public override string Name { get; set; } = "<color=#FF0000>SCP-2818</color>";

        public override string Description { get; set; } =
            "When this weapon is fired, it uses the biomass of the shooter as the bullet.";

        public override float Weight { get; set; } = 4;
        public override float Damage { get; set; } = 1000;
        public override byte ClipSize { get; set; } = 1;

        [Description("How frequently the shooter will be moved towards his target.\n# Note, a lower tick frequency, and lower MaxDistance will make the travel smoother, but be more stressful on your server.")]
        public float TickFrequency { get; set; } = 0.00025f;
        [Description("The max distance towards the target location the shooter can be moved each tick.")]
        public float MaxDistancePerTick { get; set; } = 0.50f;
        [Description("Whether or not the weapon should despawn itself after it's been used.")]
        public bool DespawnAfterUse { get; set; } = false;

        public string DeathReasonUser { get; set; } = "Vaporized by becoming a bullet";
        public string DeathReasonTarget { get; set; } = "Vaporized by a human bullet";
        
        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            Limit = 1,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
            {
                new()
                {
                    Chance = 10,
                    Location = SpawnLocationType.InsideHid,
                },
            },
        };
        
        protected override void OnShooting(ShootingEventArgs ev)
        { 
            try
            {
                foreach (Item item in ev.Player.Items.ToList())
                    if (Check(item))
                    {
                        Log.Debug($"VVUP Custom Items: SCP-2818, Found a 2818 in inventory of {ev.Player.Nickname}, removing.");
                        ev.Player.RemoveItem(item);
                    }

                Player target = ev.ClaimedTarget;
                if (ev.Direction == Vector3.zero || (ev.Player.Position - ev.Direction).sqrMagnitude > 1000f)
                {
                    ev.Player.Kill(DeathReasonUser);
                    ev.IsAllowed = false;
                    return;
                }

                Timing.RunCoroutine(ShooterProjectile(ev.Player, ev.Direction, target));
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private IEnumerator<float> ShooterProjectile(Player player, Vector3 targetPos, Player? target = null)
        {
            RoleTypeId playerRole = player.Role;

            // This is the camera transform used to make grenades appear like they are coming from the player's head instead of their stomach. We move them here so they aren't skidding across the floor.
            player.Position = player.CameraTransform.TransformPoint(new Vector3(0.0715f, 0.0225f, 0.45f));
            player.Scale = new Vector3(0.15f, 0.15f, 0.15f);
            if (target != null)
            {
                while (Vector3.Distance(player.Position, target.Position) > (MaxDistancePerTick + 0.15f))
                {
                    if (player.Role != playerRole)
                        break;

                    player.Position = Vector3.MoveTowards(player.Position, target.Position, MaxDistancePerTick);

                    yield return Timing.WaitForSeconds(TickFrequency);
                }
            }
            else
            {
                while (Vector3.Distance(player.Position, targetPos) > 0.5f)
                {
                    if (player.Role != playerRole)
                        break;

                    player.Position = Vector3.MoveTowards(player.Position, targetPos, MaxDistancePerTick);

                    yield return Timing.WaitForSeconds(TickFrequency);
                }
            }

            player.Scale = Vector3.one;

            // Make sure the scale is reset properly *before* killing them. That's important.
            yield return Timing.WaitForSeconds(0.01f);

            if (DespawnAfterUse)
            {
                Log.Debug($"VVUP Custom Items: SCP-2818, inv count: {player.Items.Count}");
                foreach (Item item in player.Items)
                {
                    if (Check(item))
                    {
                        Log.Debug("VVUP Custom Items: SCP-2818, found 2818 in inventory, removing");
                        player.RemoveItem(item);
                    }
                }
            }

            if (player.Role != RoleTypeId.Spectator)
                player.Kill(DeathReasonUser);
            if (target?.Role != RoleTypeId.Spectator)
                target?.Kill(DeathReasonTarget);
        }
    }
}