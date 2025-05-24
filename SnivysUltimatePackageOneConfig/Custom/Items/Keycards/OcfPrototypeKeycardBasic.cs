using System;
using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Pickups;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.EventArgs;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;
using Interactables.Interobjects.DoorUtils;
using InventorySystem;
using InventorySystem.Items.Keycards;
using JetBrains.Annotations;
using UnityEngine;
using YamlDotNet.Serialization;
using OperationCrossFire = SnivysUltimatePackageOneConfig.EventHandlers.ServerEventsEventHandlers.OperationCrossfireEventHandlers;

namespace SnivysUltimatePackageOneConfig.Custom.Items.Keycards
{
    [CustomItem(ItemType.KeycardCustomSite02)]
    public class OcfPrototypeKeycardBasic : CustomItem
    {
        [YamlIgnore] 
        public override ItemType Type { get; set; } = ItemType.KeycardCustomSite02;
        public override uint Id { get; set; } = 44;
        public override string Name { get; set; } = "Prototype Keycard Basic";

        public override string Description { get; set; } =
            "This is the basic Prototype Keycard needed to unlock SCP-914";

        public override float Weight { get; set; } = 0.5f;

        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            Limit = 1,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
            {
                new()
                {
                    Chance = 0,
                    Location = SpawnLocationType.InsideHidChamber,
                },
                new()
                {
                    Chance = 0,
                    Location = SpawnLocationType.InsideHczArmory,
                },
                new()
                {
                    Chance = 0,
                    Location = SpawnLocationType.Inside049Armory,
                },
                new()
                {
                    Chance = 0,
                    Location = SpawnLocationType.InsideGr18Glass,
                },
                new()
                {
                    Chance = 0,
                    Location = SpawnLocationType.Inside106Primary,
                },
                new()
                {
                    Chance = 0,
                    Location = SpawnLocationType.Inside330,
                },
                new()
                {
                    Chance = 0,
                    Location = SpawnLocationType.Inside173Gate,
                },
            }
        };

        [CanBeNull]
        public static string KeycardName { get; set; } = "Prototype Keycard Basic";
        public static KeycardLevels KeycardPermissions { get; set; } = new KeycardLevels(1, 0, 0);
        public static Color32 KeycardPermissionsColor { get; set; } = new Color32(0, 0, 0, 255);
        public static Color32 KeycardPrimaryColor { get; set; } = new Color32(255, 0, 255, 255);
        public static string KeycardLabel { get; set; } = "Prototype Keycard Basic";
        public static Color32 KeycardLabelColor { get; set; } = new Color32(255, 255, 255, 255);
        
        public uint RefinedKeycardId { get; set; } = 45;

        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();

            Exiled.Events.Handlers.Map.PickupAdded += OnPickupAdded;
            Exiled.Events.Handlers.Player.ItemAdded += OnItemAdded;
        }
        protected override void UnsubscribeEvents()
        {
            base.UnsubscribeEvents();

            Exiled.Events.Handlers.Map.PickupAdded -= OnPickupAdded;
            Exiled.Events.Handlers.Player.ItemAdded -= OnItemAdded;
        }

        private void OnPickupAdded(PickupAddedEventArgs ev) 
        {
            if (!Check(ev.Pickup))
                return;

            UpdateCard(ev.Pickup); 
        }

        private void OnItemAdded(ItemAddedEventArgs ev)
        {
            if (!Check(ev.Pickup))
                return;

            UpdateCard(ev.Pickup);
        }
        
        private void UpdateCard(Pickup pickup) //code taken from KeycardItem (kinda)
        {
            if (!Type.TryGetTemplate<KeycardItem>(out var item))
                throw new ArgumentException("Template for itemType not found");

            if (!item.Customizable)
                return;

            int num = 0;
            DetailBase[] details = item.Details;

            object[] args = new object[]
            {
                KeycardName,
                KeycardPermissions,
                KeycardPermissionsColor,
                KeycardPrimaryColor,
                KeycardLabel,
                KeycardLabelColor
            };

            for (int i = 0; i < details.Length; i++)
            {
                if (details[i] is ICustomizableDetail customizableDetail)
                {
                    customizableDetail.SetArguments(new ArraySegment<object>(args, num, customizableDetail.CustomizablePropertiesAmount));
                    num += customizableDetail.CustomizablePropertiesAmount;
                }
            }
        }
        protected override void OnUpgrading(UpgradingEventArgs ev)
        {
            ev.IsAllowed = false;
            ev.Item.DestroySelf();
            if (OperationCrossFire.OcfStarted)
                TrySpawn(RefinedKeycardId, ev.OutputPosition, out var pickup);
        }
    }
}