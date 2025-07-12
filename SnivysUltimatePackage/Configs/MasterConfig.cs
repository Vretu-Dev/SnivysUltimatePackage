using System;
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
        /*[YamlIgnore]
        public FlamingoAdjustmentsConfig FlamingoAdjustmentsConfig { get; set; } = null!;*/
        [YamlIgnore]
        public RoundStartConfig RoundStartConfig { get; set; } = null!;
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
        public string RoundStartConfigFile { get; set; } = "vvRoundStart.yml";
        public string Scp1576SpectatorViewerConfigFile { get; set; } = "vvScp1576SpectatorViewer.yml";
        public string SsssConfigFile { get; set; } = "vvSsss.yml";

        public void LoadConfigs()
        {
            if(!Directory.Exists(ConfigFolder))
                Directory.CreateDirectory(ConfigFolder);
            string filePath = String.Empty;
            filePath = Path.Combine(ConfigFolder, CustomItemConfigFile);
            // Custom Items
            if (!File.Exists(filePath))
            {
                CustomItemsConfig = new CustomItemsConfig();
                File.WriteAllText(filePath, Loader.Serializer.Serialize(CustomItemsConfig));
            }
            else
            {
                CustomItemsConfig = Loader.Deserializer.Deserialize<CustomItemsConfig>(File.ReadAllText(filePath));
                File.WriteAllText(filePath, Loader.Serializer.Serialize(CustomItemsConfig));
            }
            // Custom Roles
            filePath = Path.Combine(ConfigFolder, CustomRolesConfigFile);
            if (!File.Exists(filePath))
            {
                CustomRolesConfig = new CustomRolesConfig();
                File.WriteAllText(filePath, Loader.Serializer.Serialize(CustomRolesConfig));
            }
            else
            {
                CustomRolesConfig = Loader.Deserializer.Deserialize<CustomRolesConfig>(File.ReadAllText(filePath));
                File.WriteAllText(filePath, Loader.Serializer.Serialize(CustomRolesConfig));
            }
            // Custom Role Abilities
            filePath = Path.Combine(ConfigFolder, CustomRolesAbilitiesConfigFile);
            if (!File.Exists(filePath))
            {
                CustomRolesAbilitiesConfig = new CustomRolesAbilitiesConfig();
                File.WriteAllText(filePath, Loader.Serializer.Serialize(CustomRolesAbilitiesConfig));
            }
            else
            {
                CustomRolesAbilitiesConfig = Loader.Deserializer.Deserialize<CustomRolesAbilitiesConfig>(File.ReadAllText(filePath));
                File.WriteAllText(filePath, Loader.Serializer.Serialize(CustomRolesAbilitiesConfig));
            }
            // Micro Damage Reduction
            filePath = Path.Combine(ConfigFolder, MicroDamageReductionConfigFile);
            if (!File.Exists(filePath))
            {
                MicroDamageReductionConfig = new MicroDamageReductionConfig();
                File.WriteAllText(filePath, Loader.Serializer.Serialize(MicroDamageReductionConfig));
            }
            else
            {
                MicroDamageReductionConfig = Loader.Deserializer.Deserialize<MicroDamageReductionConfig>(File.ReadAllText(filePath));
                File.WriteAllText(filePath, Loader.Serializer.Serialize(MicroDamageReductionConfig));
            }
            // Server Events
            filePath = Path.Combine(ConfigFolder, ServerEventsMasterConfigFile);
            if (!File.Exists(filePath))
            {
                ServerEventsMasterConfig = new ServerEventsMasterConfig();
                File.WriteAllText(filePath, Loader.Serializer.Serialize(ServerEventsMasterConfig));
            }
            else
            {
                ServerEventsMasterConfig = Loader.Deserializer.Deserialize<ServerEventsMasterConfig>(File.ReadAllText(filePath));
                File.WriteAllText(filePath, Loader.Serializer.Serialize(ServerEventsMasterConfig));
            }
            // Micro Evaporate
            filePath = Path.Combine(ConfigFolder, MicroEvaporateConfigFile);
            if (!File.Exists(filePath))
            {
                MicroEvaporateConfig = new MicroEvaporateConfig();
                File.WriteAllText(filePath, Loader.Serializer.Serialize(MicroEvaporateConfig));
            }
            else
            {
                MicroEvaporateConfig = Loader.Deserializer.Deserialize<MicroEvaporateConfig>(File.ReadAllText(filePath));
                File.WriteAllText(filePath, Loader.Serializer.Serialize(MicroEvaporateConfig));
            }
            // Vote Config
            filePath = Path.Combine(ConfigFolder, VoteConfigFile);
            if (!File.Exists(filePath))
            {
                VoteConfig = new VoteConfig();
                File.WriteAllText(filePath, Loader.Serializer.Serialize(VoteConfig));
            }
            else
            {
                VoteConfig = Loader.Deserializer.Deserialize<VoteConfig>(File.ReadAllText(filePath));
                File.WriteAllText(filePath, Loader.Serializer.Serialize(VoteConfig));
            }
            // Flamingo Adjustments
            /*FilePath = Path.Combine(ConfigFolder, FlamingoAdjustmentsConfigFile);
            if (!File.Exists(FilePath))
            {
                FlamingoAdjustmentsConfig = new FlamingoAdjustmentsConfig();
                File.WriteAllText(FilePath, Loader.Serializer.Serialize(FlamingoAdjustmentsConfig));
            }
            else
            {
                FlamingoAdjustmentsConfig = Loader.Deserializer.Deserialize<FlamingoAdjustmentsConfig>(File.ReadAllText(FilePath));
                File.WriteAllText(FilePath, Loader.Serializer.Serialize(FlamingoAdjustmentsConfig));
            }*/
            // Round Start
            filePath = Path.Combine(ConfigFolder, RoundStartConfigFile);
            if (!File.Exists(filePath))
            {
                RoundStartConfig = new RoundStartConfig();
                File.WriteAllText(filePath, Loader.Serializer.Serialize(RoundStartConfig));
            }
            else
            {
                RoundStartConfig = Loader.Deserializer.Deserialize<RoundStartConfig>(File.ReadAllText(filePath));
                File.WriteAllText(filePath, Loader.Serializer.Serialize(RoundStartConfig));
            }
            // SCP 1576 Spectator Viewer
            filePath = Path.Combine(ConfigFolder, Scp1576SpectatorViewerConfigFile);
            if (!File.Exists(filePath))
            {
                Scp1576SpectatorViewerConfig = new Scp1576SpectatorViewerConfig();
                File.WriteAllText(filePath, Loader.Serializer.Serialize(Scp1576SpectatorViewerConfig));
            }
            else
            {
                Scp1576SpectatorViewerConfig = Loader.Deserializer.Deserialize<Scp1576SpectatorViewerConfig>(File.ReadAllText(filePath));
                File.WriteAllText(filePath, Loader.Serializer.Serialize(Scp1576SpectatorViewerConfig));
            }
            // SSSS
            filePath = Path.Combine(ConfigFolder, SsssConfigFile);
            if (!File.Exists(filePath))
            {
                SsssConfig = new SsssConfig();
                File.WriteAllText(filePath, Loader.Serializer.Serialize(SsssConfig));
            }
            else
            {
                SsssConfig = Loader.Deserializer.Deserialize<SsssConfig>(File.ReadAllText(filePath));
                File.WriteAllText(filePath, Loader.Serializer.Serialize(SsssConfig));
            }
        }
    }
}