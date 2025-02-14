using System.ComponentModel;
using Exiled.API.Interfaces;
using SnivysUltimatePackageOneConfig.Configs.CustomConfigs;
using SnivysUltimatePackageOneConfig.Configs.ServerEventsConfigs;

namespace SnivysUltimatePackageOneConfig.Configs
{
    public class MasterConfig : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description(
            "There is a LOT of debug statements, turn this on if you really need top check something, otherwise keep it off to avoid flooding your server console")]
        public bool Debug { get; set; } = false;

        public CustomItemsConfig CustomItemsConfig { get; set; } = new();
        public CustomRolesConfig CustomRolesConfig { get; set; } = new();
        public CustomRolesAbilitiesConfig CustomRolesAbilitiesConfig { get; set; } = new();
        public MicroDamageReductionConfig MicroDamageReductionConfig { get; set; } = new();
        public ServerEventsMasterConfig ServerEventsMasterConfig { get; set; } = new();
        public MicroEvaporateConfig MicroEvaporateConfig { get; set; } = new();
        public VoteConfig VoteConfig { get; set; } = new();
        //public FlamingoAdjustmentsConfig FlamingoAdjustmentsConfig { get; set; } = new();
        public EscapeDoorOpenerConfig EscapeDoorOpenerConfig { get; set; } = new();
        public Scp1576SpectatorViewerConfig Scp1576SpectatorViewerConfig { get; set; } = new();
        public SsssConfig SsssConfig { get; set; } = new();
    }
}