using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Item;
using YamlDotNet.Serialization;
using UnityEngine;
using MEC;
using System.ComponentModel;
using Exiled.API.Features.Items;

namespace VVUP.CustomItems.Items.Other
{
    [CustomItem(ItemType.Jailbird)]
    public class Telewand : CustomItem
    {
        [YamlIgnore]
        public override ItemType Type { get; set; } = ItemType.Jailbird;
        public override uint Id { get; set; } = 50;
        public override string Name { get; set; } = "<color=#0096FF>TeleWand</color>";
        public override string Description { get; set; } = "<b>LMB</b> to save position, <b>RMB</b> to teleport";
        public override float Weight { get; set; } = 1;

        [Description("Time before the item can be used again after teleporting")]
        public float UseCooldown { get; set; } = 15f;

        [Description("Delay after using the item before the player is teleported")]
        public float TeleportCooldown { get; set; } = 5f;

        [Description("Maximum number of times the item can be used before breaking")]
        public int MaxUses { get; set; } = 3;

        [Description("Should a global flashbang sound be played after teleportation?")]
        public bool SoundEffect { get; set; } = false;

        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            Limit = 1,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
            {
                new()
                {
                    Chance = 10,
                    Location = SpawnLocationType.Inside079Armory,
                },
            },
        };

        private readonly Dictionary<Player, Vector3> savedPositions = new();
        private readonly Dictionary<ushort, CoroutineHandle> activeCountdowns = new();
        private readonly Dictionary<ushort, float> lastUseTimes = new();
        private readonly Dictionary<ushort, int> useCounts = new();
        private readonly Dictionary<Player, CoroutineHandle> teleportHints = new();

        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();
            Exiled.Events.Handlers.Item.ChargingJailbird += OnCharging;
            Exiled.Events.Handlers.Item.Swinging += OnSwinging;
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Item.ChargingJailbird -= OnCharging;
            Exiled.Events.Handlers.Item.Swinging -= OnSwinging;
            base.UnsubscribeEvents();
        }

        private void OnSwinging(SwingingEventArgs ev)
        {
            if (Check(ev.Player.CurrentItem))
            {
                ev.IsAllowed = false;

                savedPositions[ev.Player] = ev.Player.Position;
                ev.Player.ShowHint("<color=#808080>Position Saved</color>", 2f);
            }
        }

        private void OnCharging(ChargingJailbirdEventArgs ev)
        {
            if (Check(ev.Player.CurrentItem))
            {
                ev.IsAllowed = false;

                ushort serial = ev.Item.Serial;

                if (lastUseTimes.TryGetValue(serial, out float lastTime))
                {
                    float now = Time.realtimeSinceStartup;
                    if (now - lastTime < UseCooldown)
                    {
                        float left = UseCooldown - (now - lastTime);
                        ev.Player.ShowHint($"<color=#0096FF>TeleWand</color>: Wait {left:F1}s before next use", 2f);
                        return;
                    }
                }

                if (activeCountdowns.ContainsKey(serial))
                    return;

                CoroutineHandle handle = Timing.RunCoroutine(TeleportCountdown(ev.Player, serial));
                activeCountdowns[serial] = handle;

                ClearTeleportHint(ev.Player);

                CoroutineHandle hintHandle = Timing.RunCoroutine(ShowTeleportHint(ev.Player, TeleportCooldown));
                teleportHints[ev.Player] = hintHandle;
            }
        }

        private IEnumerator<float> ShowTeleportHint(Player player, float duration)
        {
            float end = Time.realtimeSinceStartup + duration;

            while (Time.realtimeSinceStartup < end)
            {
                float left = end - Time.realtimeSinceStartup;
                player.ShowHint($"Don't move for <color=yellow>{left:F1}s</color> to teleport!", 0.2f);

                yield return 0.1f;
            }
        }

        private IEnumerator<float> TeleportCountdown(Player player, ushort serial)
        {
            Vector3 startPos = player.Position;
            float startTime = Time.realtimeSinceStartup;
            float duration = TeleportCooldown;

            while (Time.realtimeSinceStartup - startTime < duration)
            {
                if (Vector3.Distance(player.Position, startPos) > 0.05f)
                {
                    player.ShowHint("<color=red>Teleportation Canceled!</color>", 2f);

                    activeCountdowns.Remove(serial);
                    ClearTeleportHint(player);

                    yield break;
                }

                yield return 0.1f;
            }

            if (savedPositions.TryGetValue(player, out Vector3 dest))
            {
                var destRoom = Room.Get(dest);

                if (destRoom != null)
                {
                    if (Warhead.IsDetonated && destRoom.Zone != ZoneType.Surface)
                    {
                        player.ShowHint("<color=red>Warhead detonated! You can only teleport to the surface.</color>", 3f);

                        activeCountdowns.Remove(serial);
                        ClearTeleportHint(player);

                        yield break;
                    }

                    if (Map.IsLczDecontaminated && destRoom.Zone == ZoneType.LightContainment)
                    {
                        player.ShowHint("<color=red>Cannot teleport to Light Containment - zone is decontaminated!</color>", 3f);

                        activeCountdowns.Remove(serial);
                        ClearTeleportHint(player);

                        yield break;
                    }

                    if (destRoom.Zone == ZoneType.Pocket)
                    {
                        player.ShowHint("<color=red>Cannot teleport to Pocket Dimension!</color>", 3f);

                        activeCountdowns.Remove(serial);
                        ClearTeleportHint(player);

                        yield break;
                    }
                }

                player.Position = dest;
                player.ShowHint("<color=green>Teleportation Succeeded!</color>", 2f);

                if (SoundEffect)
                {
                    FlashGrenade flash = (FlashGrenade)Item.Create(ItemType.GrenadeFlash, player);
                    flash.FuseTime = 1f;
                    flash.SpawnActive(player.Position);
                }
                
                useCounts[serial] = useCounts.TryGetValue(serial, out int count) ? count + 1 : 1;
                lastUseTimes[serial] = Time.realtimeSinceStartup;

                if (useCounts[serial] >= MaxUses)
                {
                    var item = player.Items.FirstOrDefault(i => i.Serial == serial);

                    if (item != null)
                    {
                        player.RemoveItem(item);
                        player.ShowHint("<color=#0096FF>TeleWand</color> has broken!", 3f);
                    }

                    activeCountdowns.Remove(serial);
                }
            }
            else
            {
                player.ShowHint("<color=#808080>No position saved!</color>", 2f);
            }

            ClearTeleportHint(player);
            activeCountdowns.Remove(serial);
        }

        private void ClearTeleportHint(Player player)
        {
            if (teleportHints.TryGetValue(player, out var handle))
            {
                Timing.KillCoroutines(handle);
                teleportHints.Remove(player);
            }
        }
    }
}