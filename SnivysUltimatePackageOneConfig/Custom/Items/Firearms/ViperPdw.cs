using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Item;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using InventorySystem.Items.Firearms.Attachments;
using JetBrains.Annotations;
using MEC;
using UnityEngine;
using YamlDotNet.Serialization;

namespace SnivysUltimatePackageOneConfig.Custom.Items.Firearms
{
    [CustomItem(ItemType.GunCrossvec)]
    public class ViperPdw : CustomWeapon
    {
        [YamlIgnore] 
        public override ItemType Type { get; set; } = ItemType.GunCrossvec;
        public override uint Id { get; set; } = 39;
        public override string Name { get; set; } = "<color=#FF0000>Viper</color>";
        public override string Description { get; set; } = "A compact PDW that does damage based on range to target";
        public override float Weight { get; set; } = 1.75f;

        [Description(
            "Dont use Damage, instead use DamageShortRange, DamageMediumRange, and DamageLongRange to determine the damage")]
        public override float Damage { get; set; } = 0;

        public float DamageShortRange { get; set; } = 10;
        public float DamageMediumRange { get; set; } = 5;
        public float DamageLongRange { get; set; } = 2;

        [Description(
            "The range between the attacker and target, if its below the Medium Range Threshold it will do Short Range Damage")]
        public float MediumRangeThreshold { get; set; } = 5;

        [Description(
            "The range between the attacker and target, if its above the Medium Range Threshold but below the Long Range Threshold it will do Medium Range Damage")]
        public float LongRangeThreshold { get; set; } = 15;

        public override byte ClipSize { get; set; } = 10;
        public bool AllowAttachmentChanging { get; set; } = false;
        public string RestrictedAttachmentChangingMessage { get; set; } =
            "You're not allowed to swap attachments on the Viper";
        public bool UseHints { get; set; } = false;
        public float RestrictedAttachmentChangeMessageTimeDuration { get; set; } = 5f;
        
        [CanBeNull]
        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            Limit = 1,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
            {
                new()
                {
                    Chance = 25,
                    Location = SpawnLocationType.InsideHczArmory,
                },
                new()
                {
                    Chance = 25,
                    Location = SpawnLocationType.Inside049Armory,
                },
                new()
                {
                    Chance = 25,
                    Location = SpawnLocationType.Inside096,
                },
                new ()
                {
                    Chance = 25,
                    Location = SpawnLocationType.Inside079Armory,
                },
            }
        };
        
        public override AttachmentName[] Attachments { get; set; } = new[]
        {
            AttachmentName.None,
            AttachmentName.IronSights,
            AttachmentName.StandardBarrel,
            AttachmentName.RetractedStock,
            AttachmentName.StandardMagJHP,
        };
        private List<ushort> droppedVipers = new List<ushort>();

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Item.ChangingAttachments += OnChangingAttachments;
            Exiled.Events.Handlers.Server.RoundEnded += OnRoundEnd;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Item.ChangingAttachments -= OnChangingAttachments;
            Exiled.Events.Handlers.Server.RoundEnded -= OnRoundEnd;
            base.UnsubscribeEvents();
        }

        protected override void OnPickingUp(PickingUpItemEventArgs ev)
        {
            if (Check(ev.Pickup) && !droppedVipers.Contains(ev.Pickup.Serial))
            {
                Timing.CallDelayed(0.25f, () =>
                {
                    ev.Player.RemoveItem(ev.Pickup.Serial);
                    TryGive(ev.Player, Id, true);
                });
            }
        }
        protected override void OnDroppingItem(DroppingItemEventArgs ev)
        {
            if (!droppedVipers.Contains(ev.Item.Serial))
                droppedVipers.Add(ev.Item.Serial);
        }
        private void OnRoundEnd(RoundEndedEventArgs ev)
        {
            droppedVipers.Clear();
        }

        protected override void OnWaitingForPlayers()
        {
            droppedVipers.Clear();
            base.OnWaitingForPlayers();
        }

        private void OnChangingAttachments(ChangingAttachmentsEventArgs ev)
        {
            if (Check(ev.Player.CurrentItem) && !AllowAttachmentChanging)
            {
                Log.Debug(
                    $"VVUP Custom Items: ViperPDW, {ev.Player.Nickname} tried changing attachments, but it's disallowed");
                ev.IsAllowed = false;
                if (UseHints)
                {
                    Log.Debug($"VVUP Custom Items: ViperPDW, showing Restricted Attachment Changing Message Hint to {ev.Player.Nickname} for {RestrictedAttachmentChangeMessageTimeDuration} seconds");
                    ev.Player.ShowHint(RestrictedAttachmentChangingMessage, RestrictedAttachmentChangeMessageTimeDuration);
                }
                else
                {
                    Log.Debug($"VVUP Custom Items: ViperPDW, showing Restricted Attachment Changing Message Broadcast to {ev.Player.Nickname} for {RestrictedAttachmentChangeMessageTimeDuration} seconds");
                    ev.Player.Broadcast(new Exiled.API.Features.Broadcast(RestrictedAttachmentChangingMessage, (ushort)RestrictedAttachmentChangeMessageTimeDuration));
                }
            }
        }

        protected override void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Player == ev.Attacker)
                return;

            float distance = Vector3.Distance(ev.Player.Position, ev.Attacker.Position);
            float damageToApply;

            if (distance < MediumRangeThreshold)
            {
                Log.Debug($"VVUP Custom Items: ViperPDW, {ev.Attacker.Nickname} is in short range of {ev.Player.Nickname}, dealing short range damage. Distance: {distance}");
                damageToApply = DamageShortRange;
            }
            else if (distance >= MediumRangeThreshold && distance < LongRangeThreshold)
            {
                Log.Debug($"VVUP Custom Items: ViperPDW, {ev.Attacker.Nickname} is in medium range of {ev.Player.Nickname}, dealing medium range damage. Distance: {distance}");
                damageToApply = DamageMediumRange;
            }
            else
            {
                Log.Debug($"VVUP Custom Items: ViperPDW, {ev.Attacker.Nickname} is in long range of {ev.Player.Nickname}, dealing long range damage. Distance: {distance}");
                damageToApply = DamageLongRange;
            }

            if (ev.Player.HasItem(ItemType.ArmorLight))
                damageToApply *= 0.6f;
            else if (ev.Player.HasItem(ItemType.ArmorCombat))
                damageToApply *= 0.4f;
            else if (ev.Player.HasItem(ItemType.ArmorHeavy))
                damageToApply *= 0.2f;

            ev.Amount = damageToApply;
        }
    }
}