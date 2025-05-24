using System;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using Exiled.API.Features.Spawn;
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
using Random = System.Random;

namespace SnivysUltimatePackageOneConfig.Custom.Items.Keycards
{
    [CustomItem(ItemType.KeycardCustomManagement)]
    public class OcfPrototypeKeycardRefined : CustomItem
    {
        [YamlIgnore] 
        public override ItemType Type { get; set; } = ItemType.KeycardCustomManagement;
        public override uint Id { get; set; } = 45;
        public override string Name { get; set; } = "Prototype Keycard Refined";

        public override string Description { get; set; } =
            "This is the refined Prototype Keycard.";

        public override float Weight { get; set; } = 0.5f;
        public override SpawnProperties SpawnProperties { get; set; }
        
        [CanBeNull]
        public static List<string> KeycardNames { get; set; } = new List<string>
        {
            "Dr. Vicious Vikki",
        };
        public static KeycardLevels KeycardPermissions { get; set; } = new KeycardLevels(1, 2, 3);
        public static Color32 KeycardPermissionsColor { get; set; } = new Color32(0, 0, 0, 255);
        public static Color32 KeycardPrimaryColor { get; set; } = new Color32(255, 0, 255, 255);
        public static string KeycardLabel { get; set; } = "Prototype Keycard Refined";
        public static Color32 KeycardLabelColor { get; set; } = new Color32(255, 255, 255, 255);

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

        public static string KeycardName => GetRandomKeycardName();
        private static string GetRandomKeycardName()
        {
            Random random = new Random();
            if (KeycardNames == null || KeycardNames.Count == 0)
            {
                return "Someone decided to remove the keycard names.";
            }
            return KeycardNames[random.Next(KeycardNames.Count)];
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

        protected override void OnAcquired(Player player, Item item, bool displayMessage)
        {
            if (OperationCrossFire.OcfStarted)
                OperationCrossFire._prototypeDeviceRefined = true;
            base.OnAcquired(player, item, displayMessage);
        }
    }
}