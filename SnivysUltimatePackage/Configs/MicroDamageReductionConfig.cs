using System.ComponentModel;
using PlayerRoles;

namespace SnivysUltimatePackage.Configs;

public class MicroDamageReductionConfig
{
    public bool IsEnabled { get; set; } = true;
    
    [Description("SCPs that get the damage reduction from Micro")]
    public RoleTypeId[] ScpDamageReduction { get; set; } =
    {
        RoleTypeId.Scp096,
    };

    [Description("The reduced damage that the Micro does (divided by, so 2 is by half)")]
    public float ScpDamageReductionValue { get; set; } = 2.0f;
}