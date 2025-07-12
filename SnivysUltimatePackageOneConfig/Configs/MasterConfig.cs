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

        public CustomItemsConfig CustomItemsConfig { get; set; }
        public CustomRolesConfig CustomRolesConfig { get; set; }
        public CustomRolesAbilitiesConfig CustomRolesAbilitiesConfig { get; set; }
        public MicroDamageReductionConfig MicroDamageReductionConfig { get; set; }
        public ServerEventsMasterConfig ServerEventsMasterConfig { get; set; }
        public MicroEvaporateConfig MicroEvaporateConfig { get; set; }
        public VoteConfig VoteConfig { get; set; }
        //public FlamingoAdjustmentsConfig FlamingoAdjustmentsConfig { get; set; }
        public RoundStartConfig RoundStartConfig { get; set; }
        public Scp1576SpectatorViewerConfig Scp1576SpectatorViewerConfig { get; set; }
        public SsssConfig SsssConfig { get; set; }
        
        public void LoadConfigs()
        {
            // Reload all configuration objects
            CustomItemsConfig = new CustomItemsConfig();
            CustomRolesConfig = new CustomRolesConfig();
            CustomRolesAbilitiesConfig = new CustomRolesAbilitiesConfig();
            MicroDamageReductionConfig = new MicroDamageReductionConfig();
            ServerEventsMasterConfig = new ServerEventsMasterConfig();
            MicroEvaporateConfig = new MicroEvaporateConfig();
            VoteConfig = new VoteConfig();
            //FlamingoAdjustmentsConfig = new FlamingoAdjustmentsConfig();
            RoundStartConfig = new RoundStartConfig();
            Scp1576SpectatorViewerConfig = new Scp1576SpectatorViewerConfig();
            SsssConfig = new SsssConfig();
        }
    }
}