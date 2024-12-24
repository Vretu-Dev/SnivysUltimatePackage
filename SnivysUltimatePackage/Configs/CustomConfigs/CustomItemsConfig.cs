using System.Collections.Generic;
using System.ComponentModel;
using SnivysUltimatePackage.Custom.Items.Armor;
using SnivysUltimatePackage.Custom.Items.Firearms;
using SnivysUltimatePackage.Custom.Items.Grenades;
using SnivysUltimatePackage.Custom.Items.Injections;
using SnivysUltimatePackage.Custom.Items.Other;

namespace SnivysUltimatePackage.Configs.CustomConfigs
{
    public class CustomItemsConfig
    {
        [Description("Enables Custom Items")]
        public bool IsEnabled { get; set; } = true;
        
        public List<SmokeGrenade> SmokeGrenades { get; private set; } = new()
        {
            new SmokeGrenade()
        };

        public List<ExplosiveRoundRevolver> ExplosiveRoundRevolvers { get; private set; } = new()
        {
            new ExplosiveRoundRevolver()
        };
        
        public List<NerveAgentGrenade> NerveAgentGrenades { get; set; } = new()
        {
            new NerveAgentGrenade()
        };

        public List<DeadringerSyringe> DeadringerSyringes { get; private set; } = new()
        {
            new DeadringerSyringe()
        };

        public List<PhantomLantern> PhantomLanterns { get; private set; } = new()
        {
            new PhantomLantern()
        };

        public List<ExplosiveResistantArmor> ExplosiveResistantArmor { get; private set; } = new()
        {
            new ExplosiveResistantArmor()
        };

        public List<KySyringe> KySyringes { get; private set; } = new()
        {
            new KySyringe()
        };
    }
}