using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using UnityEngine;

namespace SnivysUltimatePackageOneConfig.Custom.Items.Keycards
{
    [CustomItem(ItemType.KeycardCustomSite02)]
    public class ContainmentScientistKeycard : CustomKeycard
    {
        public override uint Id { get; set; } = 35;
        public override string Name { get; set; } = "<color=#FFFF7C>Containment Engineer Scientist Keycard</color>";
        public override string Description { get; set; } = "A keycard that can be used to open doors with a Containment Engineer Scientist clearance level.";
        public override float Weight { get; set; } = 1;
        public override SpawnProperties SpawnProperties { get; set; }

        public override KeycardPermissions Permissions { get; set; } = KeycardPermissions.Checkpoints |
                                                                       KeycardPermissions.ContainmentLevelOne |
                                                                       KeycardPermissions.ContainmentLevelTwo |
                                                                       KeycardPermissions.ContainmentLevelThree |
                                                                       KeycardPermissions.AlphaWarhead |
                                                                       KeycardPermissions.Intercom;

        public override string KeycardLabel { get; set; } = "Containment Engineer Keycard";
        public override Color32? TintColor { get; set; } = new Color32(15, 143, 1, 255);
        public override Color32? KeycardLabelColor { get; set; } = new Color32(207, 210, 217, 255);
        public override Color32? KeycardPermissionsColor { get; set; } = new Color32(0, 0, 0, 255);
    }
}