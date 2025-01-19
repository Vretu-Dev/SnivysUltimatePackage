using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        public bool AllowWhen207OrAnti207IsActive { get; set; } = false;

        public string TextToShowToPlayerOnFailCola { get; set; } =
            "I dont think mixing this along with a cola effect is the best idea";
        [Description("If UseHints is true, it will show a hint to the player, otherwise it will be a broadcast")]
        public bool UseHints { get; set; } = false;
        [Description("If this is false, users can just drink a lot of additional health 207")]
        public bool CapMaxHealth { get; set; } = true;
        public float MaxUserHpLimit { get; set; } = 130;
        public string TextToShowToPlayerOnFailHpLimit { get; set; } = "I had too much, I don't need any more";
        public ushort TextDisplayDuration { get; set; } = 5;

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
            Exiled.Events.Handlers.Player.UsedItem += OnUsingItemCompleted;
            Exiled.Events.Handlers.Player.ChangingItem += OnChangingItem;
            base.SubscribeEvents();
        }
        
        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsingItem -= OnUsingItem;
            Exiled.Events.Handlers.Player.UsedItem -= OnUsingItemCompleted;
            Exiled.Events.Handlers.Player.ChangingItem -= OnChangingItem;
            base.UnsubscribeEvents();
        }

        private void OnUsingItem(UsingItemEventArgs ev)
        {
            if (!Check(ev.Player.CurrentItem))
                return;
            
            if ((ev.Player.GetEffect(EffectType.Scp207) || (ev.Player.GetEffect(EffectType.AntiScp207)) && !AllowWhen207OrAnti207IsActive))
            {
                Log.Debug(
                    $"VVUP Custom Items: Additional Health 207, {ev.Player.Nickname} tried using Additional Health 207, but has Anti-207 or 207 active, but allowing to use this is disabled with either effect active");
                
                if (UseHints)
                {
                    Log.Debug($"VVUP Custom Items: Additional Health 207, displaying use fail hint to {ev.Player.Nickname}");
                    ev.Player.ShowHint(TextToShowToPlayerOnFailCola, TextDisplayDuration);
                }
                else
                {
                    Log.Debug($"VVUP Custom Items: Additional Health 207, displaying use fail broadcast to {ev.Player.Nickname}");
                    ev.Player.Broadcast(new Exiled.API.Features.Broadcast(TextToShowToPlayerOnFailHpLimit, TextDisplayDuration));
                }
                ev.IsAllowed = false;
                return;
            }

            if (ev.Player.MaxHealth >= MaxUserHpLimit && CapMaxHealth)
            {
                Log.Debug(
                    $"VVUP Custom Items: Additional Health 207, {ev.Player.Nickname} tried using Additional Health 207, " +
                    $"but has the max health cap, {ev.Player.Nickname} has {ev.Player.MaxHealth} max health, " +
                    $"and the limit is set to {MaxUserHpLimit}");
                
                if (UseHints)
                {
                    Log.Debug($"VVUP Custom Items: Additional Health 207, displaying use fail hint to {ev.Player.Nickname}");
                    ev.Player.ShowHint(TextToShowToPlayerOnFailHpLimit, TextDisplayDuration);
                }
                else
                {
                    Log.Debug($"VVUP Custom Items: Additional Health 207, displaying use fail broadcast to {ev.Player.Nickname}");
                    ev.Player.Broadcast(new Exiled.API.Features.Broadcast(TextToShowToPlayerOnFailHpLimit, TextDisplayDuration));
                }
                ev.IsAllowed = false;
                return;
            }
            
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

        private void OnUsingItemCompleted(UsedItemEventArgs ev)
        {
            if (!Check(ev.Player.CurrentItem))
                return;
            Log.Debug($"VVUP Custom Items: Additional Health 207, {ev.Player.Nickname} finished drinking Additional Health 207, Unlocking inventory. Applying additional health");
            _consuming207 = false;
            ev.Player.MaxHealth += HealthToBeAdded;
            ev.Player.Heal(HealthToBeAdded);
            Timing.CallDelayed(0.25f, () => ev.Player.DisableEffect(EffectType.AntiScp207));
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