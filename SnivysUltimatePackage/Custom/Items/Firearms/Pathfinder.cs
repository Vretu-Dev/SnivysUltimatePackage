using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerAPI = Exiled.API.Features.Player;
using YamlDotNet.Serialization;

namespace SnivysUltimatePackage.Custom.Items.Firearms
{
    [CustomItem(ItemType.GunCOM18)]
    public class Pathfinder : CustomWeapon
    {
        [YamlIgnore] public override ItemType Type { get; set; } = ItemType.GunCOM18;
        public override uint Id { get; set; } = 40;
        public override string Name { get; set; } = "<color=#0096FF>Pathfinder</color>";

        public override string Description { get; set; } =
            "When it hits a hostile, they get marked for a set amount of time, causing increased damage";

        public override float Weight { get; set; } = 1.5f;
        public override SpawnProperties SpawnProperties { get; set; }
        public override float Damage { get; set; } = 0;
        public override byte ClipSize { get; set; } = 1;

        [Description("Handles how long a target should be marked for")]
        public float MarkedDuration { get; set; } = 10f;
        [Description("Handles the damage multipler the tracked target(s) should take (1.10 = 10% more, 1.50 = 50% more, etc")]
        public float DamageMultiplier { get; set; } = 1.10f;
        [Description("Handles how much time needs to pass in seconds before allowing a reload. (To prevent being able to spam it at multiple targets)")]
        public float TimeNeededToPassBeforeReload { get; set; } = 5f;
        [Description("If true on a friendly fire on server. Can the Pathfinder mark teammates. If set to true on a friendly fire off server, does nothing.")]
        public bool MarkTeammates { get; set; } = false;

        public string ReloadingMessage { get; set; } = "Reloading, it takes 5 seconds";
        public float ReloadingMessageTime { get; set; } = 5f;
        public bool UseHints { get; set; } = false;
        
        private List<PlayerAPI> _activeMarkedPlayers = new();
        private List<PlayerAPI> _playersReloading = new();
        
        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
            Exiled.Events.Handlers.Player.ChangingItem += OnChangingItem;
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
            Exiled.Events.Handlers.Player.ChangingItem -= OnChangingItem;
        }

        protected override void OnShot(ShotEventArgs ev)
        {
            if (ev.Target == null)
                return;
            if (ev.Player.Role.Side == Side.Mtf && (ev.Target.Role.Side != Side.Mtf || 
                (ev.Target.Role.Side == Side.Mtf && MarkTeammates)))
            {
                if (_activeMarkedPlayers.Contains(ev.Target))
                    return;
                _activeMarkedPlayers.Add(ev.Target);
            }
            else if (ev.Player.Role.Side == Side.ChaosInsurgency && (ev.Target.Role.Side != Side.ChaosInsurgency ||
                     (ev.Target.Role.Side == Side.ChaosInsurgency && MarkTeammates)))
            {
                if (_activeMarkedPlayers.Contains(ev.Target))
                    return;
                _activeMarkedPlayers.Add(ev.Target);
            }
            else if (ev.Player.Role.Side is Side.Tutorial or Side.Scp && (ev.Target.Role.Side is not Side.Tutorial or Side.Scp ||
                     (ev.Target.Role.Side is Side.Tutorial or Side.Scp && MarkTeammates)))
            {
                if (_activeMarkedPlayers.Contains(ev.Target))
                    return;
                _activeMarkedPlayers.Add(ev.Target);
            }
            Timing.CallDelayed(MarkedDuration, () =>
            {
                _activeMarkedPlayers.Remove(ev.Target);
            });
        }

        private new void OnHurting(HurtingEventArgs ev)
        {
            if (!_activeMarkedPlayers.Contains(ev.Player))
                return;
            ev.Amount *= DamageMultiplier;
        }

        protected override void OnReloading(ReloadingWeaponEventArgs ev)
        {
            if (!Check(ev.Player.CurrentItem))
                return;
            ev.IsAllowed = false;
            if (_playersReloading.Contains(ev.Player))
                return;
            _playersReloading.Add(ev.Player);
            if (UseHints)
            {
                Log.Debug($"VVUP Custom Items: Pathfinder, showing Restricted Attachment Changing Message Hint to {ev.Player.Nickname} for {ReloadingMessageTime} seconds");
                ev.Player.ShowHint(ReloadingMessage, ReloadingMessageTime);
            }
            else
            {
                Log.Debug($"VVUP Custom Items: Pathfinder, showing Restricted Attachment Changing Message Broadcast to {ev.Player.Nickname} for {ReloadingMessageTime} seconds");
                ev.Player.Broadcast(new Exiled.API.Features.Broadcast(ReloadingMessage, (ushort)ReloadingMessageTime));
            }
            Timing.CallDelayed(TimeNeededToPassBeforeReload, () =>
            {
                Log.Debug($"VVUP Custom Items: Pathfinder, {ev.Player.Nickname} is now reloading Pathfinder.");
                ev.Firearm.Reload();
                _playersReloading.Remove(ev.Player);
            });
        }

        private void OnChangingItem(ChangingItemEventArgs ev)
        {
            if (!Check(ev.Player.CurrentItem))
                return;
            if (!_playersReloading.Contains(ev.Player))
                return;
            Log.Debug($"VVUP Custom Items: Pathfinder, {ev.Player.Nickname} tried swapping weapons during reloading Pathfinder, restricting.");
            ev.IsAllowed = false;
        }
    }
}