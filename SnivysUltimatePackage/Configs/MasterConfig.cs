using System.ComponentModel;
using System.IO;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using Exiled.Loader;
using SnivysUltimatePackage.Configs.CustomConfigs;
using SnivysUltimatePackage.Configs.ServerEventsConfigs;
using YamlDotNet.Serialization;

namespace SnivysUltimatePackage.Configs
{
    public class MasterConfig : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        
        [Description("There is a LOT of debug statements, turn this on if you really need top check something, otherwise keep it off to avoid flooding your server console")]
        public bool Debug { get; set; } = false;

        [YamlIgnore]
        public CustomItemsConfig CustomItemsConfig { get; set; } = null!;
        [YamlIgnore]
        public CustomRolesConfig CustomRolesConfig { get; set; } = null!;
        [YamlIgnore]
        public CustomRolesAbilitiesConfig CustomRolesAbilitiesConfig { get; set; } = null!;
        [YamlIgnore]
        public MicroDamageReductionConfig MicroDamageReductionConfig { get; set; } = null!;
        [YamlIgnore]
        public ServerEventsMasterConfig ServerEventsMasterConfig { get; set; } = null!;
        [YamlIgnore]
        public MicroEvaporateConfig MicroEvaporateConfig { get; set; } = null!;
        [YamlIgnore]
        public VoteConfig VoteConfig { get; set; } = null!;
        [YamlIgnore]
        public FlamingoAdjustmentsConfig FlamingoAdjustmentsConfig { get; set; } = null!;
        [YamlIgnore]
        public EscapeDoorOpenerConfig EscapeDoorOpenerConfig { get; set; } = null!;
        [YamlIgnore]
        public Scp1576SpectatorViewerConfig Scp1576SpectatorViewerConfig { get; set; } = null!;
        [YamlIgnore]
        public SsssConfig SsssConfig { get; set; } = null!;

        public string ConfigFolder { get; set; } =
            Path.Combine(Paths.Configs, "ViciousVikkisUltimatePluginPackage");
        
        public string CustomItemConfigFile { get; set; } = "vvCustomItems.yml";
        public string CustomRolesConfigFile { get; set; } = "vvCustomRoles.yml";
        public string CustomRolesAbilitiesConfigFile { get; set; } = "vvCustomAbilities.yml";
        public string MicroDamageReductionConfigFile { get; set; } = "vvMicroDamageReduction.yml";
        public string ServerEventsMasterConfigFile { get; set; } = "vvServerEvents.yml";
        public string MicroEvaporateConfigFile { get; set; } = "vvMicroEvaporate.yml";
        public string VoteConfigFile { get; set; } = "vvVote.yml";
        public string FlamingoAdjustmentsConfigFile { get; set; } = "vvFlamingoAdjustments.yml";
        public string EscapeDoorOpenerConfigFile { get; set; } = "vvEscapeDoorOpener.yml";
        public string Scp1576SpectatorViewerConfigFile { get; set; } = "vvScp1576SpectatorViewer.yml";
        public string SsssConfigFile { get; set; } = "vvSsss.yml";

        public void LoadConfigs()
        {
            if(!Directory.Exists(ConfigFolder))
                Directory.CreateDirectory(ConfigFolder);
            
            string ciFilePath = Path.Combine(ConfigFolder, CustomItemConfigFile);
            if (!File.Exists(ciFilePath))
            {
                CustomItemsConfig = new CustomItemsConfig();
                File.WriteAllText(ciFilePath, Loader.Serializer.Serialize(CustomItemsConfig));
            }
            else
            {
                CustomItemsConfig = Loader.Deserializer.Deserialize<CustomItemsConfig>(File.ReadAllText(ciFilePath));
                File.WriteAllText(ciFilePath, Loader.Serializer.Serialize(CustomItemsConfig));
            }
            
            string crFilePath = Path.Combine(ConfigFolder, CustomRolesConfigFile);
            if (!File.Exists(crFilePath))
            {
                CustomRolesConfig = new CustomRolesConfig();
                File.WriteAllText(crFilePath, Loader.Serializer.Serialize(CustomRolesConfig));
            }
            else
            {
                CustomRolesConfig = Loader.Deserializer.Deserialize<CustomRolesConfig>(File.ReadAllText(crFilePath));
                File.WriteAllText(crFilePath, Loader.Serializer.Serialize(CustomRolesConfig));
            }
            
            string caFilePath = Path.Combine(ConfigFolder, CustomRolesAbilitiesConfigFile);
            if (!File.Exists(caFilePath))
            {
                CustomRolesAbilitiesConfig = new CustomRolesAbilitiesConfig();
                File.WriteAllText(caFilePath, Loader.Serializer.Serialize(CustomRolesAbilitiesConfig));
            }
            else
            {
                CustomRolesAbilitiesConfig = Loader.Deserializer.Deserialize<CustomRolesAbilitiesConfig>(File.ReadAllText(caFilePath));
                File.WriteAllText(caFilePath, Loader.Serializer.Serialize(CustomRolesAbilitiesConfig));
            }
            
            string mdrFilePath = Path.Combine(ConfigFolder, MicroDamageReductionConfigFile);
            if (!File.Exists(mdrFilePath))
            {
                MicroDamageReductionConfig = new MicroDamageReductionConfig();
                File.WriteAllText(mdrFilePath, Loader.Serializer.Serialize(MicroDamageReductionConfig));
            }
            else
            {
                MicroDamageReductionConfig = Loader.Deserializer.Deserialize<MicroDamageReductionConfig>(File.ReadAllText(mdrFilePath));
                File.WriteAllText(mdrFilePath, Loader.Serializer.Serialize(MicroDamageReductionConfig));
            }
            
            string serverEventsFilePath = Path.Combine(ConfigFolder, ServerEventsMasterConfigFile);
            if (!File.Exists(serverEventsFilePath))
            {
                ServerEventsMasterConfig = new ServerEventsMasterConfig();
                File.WriteAllText(serverEventsFilePath, Loader.Serializer.Serialize(ServerEventsMasterConfig));
            }
            else
            {
                ServerEventsMasterConfig = Loader.Deserializer.Deserialize<ServerEventsMasterConfig>(File.ReadAllText(serverEventsFilePath));
                File.WriteAllText(serverEventsFilePath, Loader.Serializer.Serialize(ServerEventsMasterConfig));
            }
            
            string meFilePath = Path.Combine(ConfigFolder, MicroEvaporateConfigFile);
            if (!File.Exists(meFilePath))
            {
                MicroEvaporateConfig = new MicroEvaporateConfig();
                File.WriteAllText(meFilePath, Loader.Serializer.Serialize(MicroEvaporateConfig));
            }
            else
            {
                MicroEvaporateConfig = Loader.Deserializer.Deserialize<MicroEvaporateConfig>(File.ReadAllText(meFilePath));
                File.WriteAllText(meFilePath, Loader.Serializer.Serialize(MicroEvaporateConfig));
            }
            
            string vFilePath = Path.Combine(ConfigFolder, VoteConfigFile);
            if (!File.Exists(vFilePath))
            {
                VoteConfig = new VoteConfig();
                File.WriteAllText(vFilePath, Loader.Serializer.Serialize(VoteConfig));
            }
            else
            {
                VoteConfig = Loader.Deserializer.Deserialize<VoteConfig>(File.ReadAllText(vFilePath));
                File.WriteAllText(vFilePath, Loader.Serializer.Serialize(VoteConfig));
            }
            
            string fFilePath = Path.Combine(ConfigFolder, FlamingoAdjustmentsConfigFile);
            if (!File.Exists(fFilePath))
            {
                FlamingoAdjustmentsConfig = new FlamingoAdjustmentsConfig();
                File.WriteAllText(fFilePath, Loader.Serializer.Serialize(FlamingoAdjustmentsConfig));
            }
            else
            {
                FlamingoAdjustmentsConfig = Loader.Deserializer.Deserialize<FlamingoAdjustmentsConfig>(File.ReadAllText(fFilePath));
                File.WriteAllText(fFilePath, Loader.Serializer.Serialize(FlamingoAdjustmentsConfig));
            }
            
            string edoFilePath = Path.Combine(ConfigFolder, EscapeDoorOpenerConfigFile);
            if (!File.Exists(edoFilePath))
            {
                EscapeDoorOpenerConfig = new EscapeDoorOpenerConfig();
                File.WriteAllText(edoFilePath, Loader.Serializer.Serialize(EscapeDoorOpenerConfig));
            }
            else
            {
                EscapeDoorOpenerConfig = Loader.Deserializer.Deserialize<EscapeDoorOpenerConfig>(File.ReadAllText(edoFilePath));
                File.WriteAllText(edoFilePath, Loader.Serializer.Serialize(EscapeDoorOpenerConfig));
            }
            
            string s1576svFilePath = Path.Combine(ConfigFolder, Scp1576SpectatorViewerConfigFile);
            if (!File.Exists(s1576svFilePath))
            {
                Scp1576SpectatorViewerConfig = new Scp1576SpectatorViewerConfig();
                File.WriteAllText(s1576svFilePath, Loader.Serializer.Serialize(Scp1576SpectatorViewerConfig));
            }
            else
            {
                Scp1576SpectatorViewerConfig = Loader.Deserializer.Deserialize<Scp1576SpectatorViewerConfig>(File.ReadAllText(s1576svFilePath));
                File.WriteAllText(s1576svFilePath, Loader.Serializer.Serialize(Scp1576SpectatorViewerConfig));
            }

            string ssssFilePath = Path.Combine(ConfigFolder, SsssConfigFile);
            if (!File.Exists(ssssFilePath))
            {
                SsssConfig = new SsssConfig();
                File.WriteAllText(ssssFilePath, Loader.Serializer.Serialize(SsssConfig));
            }
            else
            {
                SsssConfig = Loader.Deserializer.Deserialize<SsssConfig>(File.ReadAllText(ssssFilePath));
                File.WriteAllText(ssssFilePath, Loader.Serializer.Serialize(SsssConfig));
            }
        }
    }
}