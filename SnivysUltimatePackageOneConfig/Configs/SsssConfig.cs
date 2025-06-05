namespace SnivysUltimatePackageOneConfig.Configs
{
    public class SsssConfig
    {
        public bool IsEnabled { get; set; } = false;

        public int ActiveCamoId { get; set; } = 10000;
        public int ChargeId { get; set; } = 10001;
        public int DetectId { get; set; } = 10002;
        public int DoorPickingId { get; set; } = 10003;
        public int HealingMistId { get; set; } = 10004;
        public int RemoveDisguiseId { get; set; } = 10005;
        public int DetonateC4Id { get; set; } = 10006;
        public int ReviveMistId { get; set; } = 10007;
        public string CustomRoleHeader { get; set; } = "VV Custom Roles";
        public string CustomItemHeader { get; set; } = "VV Custom Items";
        public string CustomAbilityActivatorHeader { get; set; } = "VV Custom Role Ability Activators";
        public string CustomItemActivators { get; set; } = "VV Custom Item Activators";
        public string ActiveCamoSsssText { get; set; } = "Active Camo";
        public string ChargeSsssText { get; set; } = "Charge";
        public string DetectSsssText { get; set; } = "Detect";
        public string DoorPickingSsssText { get; set; } = "Door Picking";
        public string HealingMistSsssText { get; set; } = "Healing Mist";
        public string RemoveDisguiseSsssText { get; set; } = "Remove Disguise";
        public string DetonateC4SsssText { get; set; } = "Detonate C4";
        public string ReviveMistSsssText { get; set; } = "Reviving Mist";
        public string SsssActiveCamoActivationMessage { get; set; } = "Activated Active Camo";
        public string SsssChargeActivationMessage { get; set; } = "Activated Charge";
        public string SsssDoorPickingActivationMessage { get; set; } = "Activated Door Picking, Interact with the door you want to pick.";
        public string SsssHealingMistActivationMessage { get; set; } = "Activated Healing Mist";
        public string SsssRemoveDisguiseActivationMessage { get; set; } = "Removing Disguise";
        public string SsssC4NoC4Deployed { get; set; } = "You haven't placed any C4";
        public string SsssC4DetonatorNeeded { get; set; } = "You need to have your detonator equipped";
        public string SsssC4TooFarAway { get; set; } = "You are far away from your C4, consider getting closer";
        public string SsssDetonateC4ActivationMessage { get; set; } = "Detonating C4";
        public string SsssReviveMistActivationMessage { get; set; } = "Activated Revive Mist";
    }
}