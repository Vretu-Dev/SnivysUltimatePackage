using System.ComponentModel;

namespace SnivysUltimatePackageOneConfig.Configs.CustomConfigs
{
    public class CustomRolesAbilitiesConfig
    {
        [Description("Enables Custom Role Abilities")]
        public bool IsEnabled { get; set; } = true;
    }
}