namespace SnivysUltimatePackage.Configs
{
    public class SsssConfig
    {
        public bool IsEnabled { get; set; } = true;

        public int ActiveCamoId { get; set; } = 10000;
        public int ChargeId { get; set; } = 10001;
        public int DetectId { get; set; } = 10002;
        public int DoorPickingId { get; set; } = 10003;
        public int HealingMistId { get; set; } = 10004;
        public int RemoveDisguiseId { get; set; } = 10005;
        public int DetonateC4Id { get; set; } = 10006;
        
        public bool SsssCustomRoleInfo { get; set; } = true;
        public bool SsssCustomItemInfo { get; set; } = true;
        public bool SsssCustomRoleAbilityToggle { get; set; } = true;
        public bool SsssC4DetonateToggle { get; set; } = true;
        
        public string SsssActiveCamoActivationMessage { get; set; } = "Activated Active Camo";
        public string SsssChargeActivationMessage { get; set; } = "Activated Charge";
        public string SsssDoorPickingActivationMessage { get; set; } = "Activated Door Picking, Interact with the door you want to pick.";
        public string SsssHealingMistActivationMessage { get; set; } = "Activated Healing Mist";
        public string SsssRemoveDisguiseActivationMessage { get; set; } = "Removing Disguise";

        public string SsssC4NoC4Deployed { get; set; } = "You haven't placed any C4";
        public string SsssC4DetonatorNeeded { get; set; } = "You need to have your detonator equipped";
        public string SsssC4TooFarAway { get; set; } = "You are far away from your C4, consider getting closer";
        public string SsssDetonateC4ActivationMessage { get; set; } = "Detonating C4";
    }
}