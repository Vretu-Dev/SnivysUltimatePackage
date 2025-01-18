using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using YamlDotNet.Serialization;

namespace SnivysUltimatePackage.Custom.Items.Other
{
    [CustomItem(ItemType.AntiSCP207)]
    public class AdditionalHealth207 : CustomItem
    {
        [YamlIgnore]
        public override ItemType Type { get; set; } = ItemType.AntiSCP207;
        [YamlIgnore]
        private bool _consuming207 = false;
        public override uint Id { get; set; } = 37;
        public override string Name { get; set; } = "<color=#6600CC>Additional Health SCP-207</color>";
        public override string Description { get; set; } = "Adds additional health on consumption";
        public override float Weight { get; set; } = 1;
        public float HealthToBeAdded { get; set; } = 15;

        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            Limit = 1,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
            {
                new DynamicSpawnPoint()
                {
                    Chance = 25,
                    Location = SpawnLocationType.InsideHidChamber,
                },
            },
        };
        
        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsingItem += OnUsingItem;
            Exiled.Events.Handlers.Player.UsingItemCompleted += OnUsingItemCompleted;
            Exiled.Events.Handlers.Player.ChangingItem += OnChangingItem;
            base.SubscribeEvents();
        }
        
        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsingItem -= OnUsingItem;
            Exiled.Events.Handlers.Player.UsingItemCompleted -= OnUsingItemCompleted;
            Exiled.Events.Handlers.Player.ChangingItem -= OnChangingItem;
            base.UnsubscribeEvents();
        }

        private void OnUsingItem(UsingItemEventArgs ev)
        {
            if (!Check(ev.Player.CurrentItem))
                return;
            Log.Debug(
                $"VVUP Custom Items: Additional Health 207, {ev.Player.Nickname} is consuming Addition Health 207, locking inventory for a moment");
            _consuming207 = true;
            Timing.CallDelayed(10f, () =>
            {
                if (_consuming207)
                {
                    _consuming207 = false;
                    Log.Debug($"VVUP Custom Items: Additional Health 207, {ev.Player.Nickname} was not able to finish drinking the item. Activating fail safe to allow the player to use inventory again");
                }
            });
        }

        private void OnUsingItemCompleted(UsingItemCompletedEventArgs ev)
        {
            if (!Check(ev.Player.CurrentItem))
                return;
            ev.IsAllowed = false;
            Log.Debug($"VVUP Custom Items: Additional Health 207, {ev.Player.Nickname} finished drinking Additional Health 207, Unlocking inventory. Applying additional health");
            _consuming207 = false;
            ev.Player.MaxHealth += HealthToBeAdded;
            ev.Player.Heal(HealthToBeAdded);
        }

        private void OnChangingItem(ChangingItemEventArgs ev)
        {
            if (!Check(ev.Player.CurrentItem))
                return;
            
            if (_consuming207)
            {
                Log.Debug($"VVUP Custom Items: Additional Health 207, {ev.Player.Nickname} tried swapping items while drinking additional health 207, restricting.");
                ev.IsAllowed = false;
            }
        }
    }
}