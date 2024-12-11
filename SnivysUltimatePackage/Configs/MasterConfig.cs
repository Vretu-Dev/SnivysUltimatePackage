using Exiled.API.Interfaces;
using SnivysUltimatePackage.Configs.CustomConfigs;
using SnivysUltimatePackage.Configs.ServerEventsConfigs;

namespace SnivysUltimatePackage.Configs
{
    public class MasterConfig : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        public CustomItemsConfig CustomItemsConfig { get; set; } = new();
        
        public CustomRolesConfig CustomRolesConfig { get; set; } = new();

        public CustomRolesAbilitiesConfig CustomRolesAbilitiesConfig { get; set; } = new();
        
        public MicroDamageReductionConfig MicroDamageReductionConfig { get; set; } = new();
        
        public ServerEventsMasterConfig ServerEventsMasterConfig { get; set; } = new();
    }
}