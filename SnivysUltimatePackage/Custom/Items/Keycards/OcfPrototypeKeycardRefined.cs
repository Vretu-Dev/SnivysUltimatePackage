using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using UnityEngine;
using YamlDotNet.Serialization;
using OperationCrossFire = SnivysUltimatePackage.EventHandlers.ServerEventsEventHandlers.OperationCrossfireEventHandlers;

namespace SnivysUltimatePackage.Custom.Items.Keycards
{
    [CustomItem(ItemType.KeycardFacilityManager)]
    public class OcfPrototypeKeycardRefined : CustomItem
    {
        [YamlIgnore] 
        public override ItemType Type { get; set; } = ItemType.KeycardFacilityManager;
        public override uint Id { get; set; } = 45;
        public override string Name { get; set; } = "Prototype Keycard Refined";

        public override string Description { get; set; } =
            "This is the refined Prototype Keycard.";

        public override float Weight { get; set; } = 0.5f;
        public override SpawnProperties SpawnProperties { get; set; }

        protected override void OnAcquired(Player player, Item item, bool displayMessage)
        {
            if (OperationCrossFire.OcfStarted)
                OperationCrossFire._prototypeDeviceRefined = true;
            base.OnAcquired(player, item, displayMessage);
        }
    }
}