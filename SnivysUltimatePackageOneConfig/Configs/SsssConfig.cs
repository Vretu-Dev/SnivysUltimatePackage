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
        public int TeleportId { get; set; } = 10008;
        public string Header { get; set; } = "VV Ultimate Package.";
        public int CustomRoleTextId { get; set; } = 1;
        public int CustomItemTextId { get; set; } = 2;
        public string ActiveCamoHint { get; set; } = "Press the keybind to activate Active Camo, you will become invisible for a short time (Custom Ability).";
        public string ChargeHint { get; set; } = "Press the keybind to activate Charge, you will be able to charge at a target (Custom Ability).";
        public string DetectHint { get; set; } = "Press the keybind to activate Detect, you will be able to detect nearby players (Custom Ability).";
        public string DoorPickingHint { get; set; } = "Press the keybind to activate Door Picking, you will be able to pick doors (Custom Ability).";
        public string HealingMistHint { get; set; } = "Press the keybind to activate Healing Mist, you will be able to heal yourself and nearby players (Custom Ability).";
        public string RemoveDisguiseHint { get; set; } = "Press the keybind to activate Remove Disguise, you will be able to remove your disguise (Custom Ability).";
        public string DetonateC4Hint { get; set; } = "Press the keybind to activate Detonate C4, you will be able to detonate your C4 (Custom Item).";
        public string ReviveMistHint { get; set; } = "Press the keybind to activate Revive Mist, you will be able to revive nearby players (Custom Ability).";
        public string TeleportHint { get; set; } = "Press the keybind to activate Teleport, you will be able to teleport to a target location (Custom Ability).";
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
        public string TeleportSsssText { get; set; } = "Teleport";
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
        public string SsssTeleportActivationMessage { get; set; } = "Activated Teleport";
    }
}