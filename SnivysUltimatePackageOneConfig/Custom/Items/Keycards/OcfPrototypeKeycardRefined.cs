using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using UnityEngine;
using YamlDotNet.Serialization;
using OperationCrossFire = SnivysUltimatePackageOneConfig.EventHandlers.ServerEventsEventHandlers.OperationCrossfireEventHandlers;

namespace SnivysUltimatePackageOneConfig.Custom.Items.Keycards
{
    [CustomItem(ItemType.KeycardCustomSite02)]
    public class OcfPrototypeKeycardRefined : CustomKeycard
    {
        [YamlIgnore]
        public override ItemType Type { get; set; } = ItemType.KeycardCustomSite02;
        public override uint Id { get; set; } = 45;
        public override string Name { get; set; } = "Prototype Keycard Refined";

        public override string Description { get; set; } =
            "This is the refined Prototype Keycard.";

        public override float Weight { get; set; } = 0.5f;
        public override SpawnProperties SpawnProperties { get; set; }
        public override string KeycardLabel { get; set; } = "Prototype Keycard Refined";
        public override Color32? TintColor { get; set; } = Color.red;
        public override Color32? KeycardLabelColor { get; set; } = Color.blue;
        public override Color32? KeycardPermissionsColor { get; set; } = Color.blue;

        protected override void OnAcquired(Player player, Item item, bool displayMessage)
        {
            if (OperationCrossFire.OcfStarted)
                OperationCrossFire._prototypeDeviceRefined = true;
            base.OnAcquired(player, item, displayMessage);
        }
    }
}