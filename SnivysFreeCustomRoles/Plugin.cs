using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;
using Exiled.Loader;

namespace SnivysFreeCustomRoles
{
    public class Plugin : Plugin<MasterConfig>
    {
        public override PluginPriority Priority { get; } = PluginPriority.Lower;
        public static Plugin Instance;
        public override string Name { get; } = "Snivy's Free Custom Roles";
        public override string Author { get; } = "Vicious Vikki";
        public override string Prefix { get; } = "VVFreeCustomRoles";
        public override Version Version { get; } = new Version(2, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(9, 6, 1);

        public bool SplitConfig  = false;
        public bool OneConfig = false;

        public override void OnEnabled()
        {
            Instance = this;
            var versionDecider = new VersionDecider();

            bool initialized = false;

            // Try to initialize with available package
            if (Loader.Plugins.Any(plugin => plugin.Prefix == "VVUltimatePluginPackage"))
            {
                Log.Info("Found VVUltimatePluginPackage, attempting to initialize...");
                if (versionDecider.Initialize("VVUltimatePluginPackage"))
                {
                    SplitConfig = true;
                    initialized = true;
                    Log.Info("Successfully initialized with VVUltimatePluginPackage.");
                }
                else
                {
                    Log.Error("Failed to initialize with VVUltimatePluginPackage. Attempting fallback...");
                }
            }

            // Try alternate package if first one failed
            if (!initialized && Loader.Plugins.Any(plugin => plugin.Prefix == "VVUltimatePluginPackageOneConfig"))
            {
                Log.Info("Found VVUltimatePluginPackageOneConfig, attempting to initialize...");
                if (versionDecider.Initialize("VVUltimatePluginPackageOneConfig"))
                {
                    OneConfig = true;
                    initialized = true;
                    Log.Info("Successfully initialized with VVUltimatePluginPackageOneConfig.");
                }
                else
                {
                    Log.Error("Failed to initialize with VVUltimatePluginPackageOneConfig.");
                }
            }

            if (!initialized)
            {
                Log.Error("Could not initialize with either package. Disabling plugin.");
                base.OnDisabled();
                return;
            }
            
            Config.FreeCustomRoles1.Register();
            Config.FreeCustomRoles2.Register();
            Config.FreeCustomRoles3.Register();
            Config.FreeCustomRoles4.Register();
            Config.FreeCustomRoles5.Register();
            Config.FreeCustomRoles6.Register();
            Config.FreeCustomRoles7.Register();
            Config.FreeCustomRoles8.Register();
            Config.FreeCustomRoles9.Register();
            Config.FreeCustomRoles10.Register();
            Config.FreeCustomRoles11.Register();
            Config.FreeCustomRoles12.Register();
            Config.FreeCustomRoles13.Register();
            Config.FreeCustomRoles14.Register();
            Config.FreeCustomRoles15.Register();
            Config.FreeCustomRoles16.Register();
            Config.FreeCustomRoles17.Register();
            Config.FreeCustomRoles18.Register();
            Config.FreeCustomRoles19.Register();
            Config.FreeCustomRoles20.Register();
            
            foreach (CustomRole role in CustomRole.Registered)
            {
                if (role is ICustomRole custom)
                {
                    Log.Debug($"Adding {role.Name} to dictionary..");
                    StartTeam localTeam;
            
                    if (custom.StartTeam.HasFlag(StartTeam.Chaos))
                        localTeam = StartTeam.Chaos;
                    else if (custom.StartTeam.HasFlag(StartTeam.Guard))
                        localTeam = StartTeam.Guard;
                    else if (custom.StartTeam.HasFlag(StartTeam.Ntf))
                        localTeam = StartTeam.Ntf;
                    else if (custom.StartTeam.HasFlag(StartTeam.Scientist))
                        localTeam = StartTeam.Scientist;
                    else if (custom.StartTeam.HasFlag(StartTeam.ClassD))
                        localTeam = StartTeam.ClassD;
                    else if (custom.StartTeam.HasFlag(StartTeam.Scp))
                        localTeam = StartTeam.Scp;
                    else
                        localTeam = StartTeam.Other;

                    try
                    {
                        // Convert the local team to the package's enum type
                        object packageTeam = versionDecider.ConvertStartTeam(localTeam);
                
                        // Use the new method instead of directly manipulating the dictionary
                        versionDecider.AddRoleToTeam(packageTeam, role, role.SpawnProperties.Limit);
                
                        // Get the dictionary for logging purposes
                        var roles = versionDecider.GetRolesDict();
                        if (roles != null && roles.ContainsKey(packageTeam))
                        {
                            Log.Debug($"Roles {localTeam} now has {roles[packageTeam].Count} elements.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"Error processing role {role.Name}: {ex.Message}");
                    }
                }
            }

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            CustomRole.UnregisterRoles();
            SplitConfig = false;
            OneConfig = false;
            base.OnDisabled();
        }
    }
}