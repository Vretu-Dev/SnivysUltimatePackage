using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Loader;

namespace SnivysFreeCustomRoles
{
    public class VersionDecider
    {
        public Type PackageCustomRoleType { get; private set; }
        public Type PackageStartTeamType { get; private set; }
        private object _pluginInstance;
        private PropertyInfo _rolesProperty;
        private object _rolesDict;
        private string _baseNamespace;

        public bool Initialize(string prefix)
        {
            try
            {
                var plugin = Loader.Plugins.FirstOrDefault(p => p.Prefix == prefix);
                if (plugin == null)
                {
                    Log.Error($"Could not find plugin with prefix: {prefix}");
                    return false;
                }

                _baseNamespace = prefix == "VVUltimatePluginPackage"
                    ? "SnivysUltimatePackage"
                    : "SnivysUltimatePackageOneConfig";

                Log.Debug($"Using base namespace: {_baseNamespace}");

                var assembly = plugin.Assembly;

                PackageCustomRoleType = assembly.GetType($"{_baseNamespace}.API.ICustomRole");
                PackageStartTeamType = assembly.GetType($"{_baseNamespace}.API.StartTeam");

                _pluginInstance = plugin;
                Log.Debug($"Found plugin instance directly from Loader.Plugins registry");

                foreach (var prop in _pluginInstance.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    Log.Debug($"Examining property: {prop.Name} of type {prop.PropertyType}");
            
                    if (prop.Name == "Roles")
                    {
                        _rolesProperty = prop;
                        var value = prop.GetValue(_pluginInstance);
                
                        if (value != null)
                        {
                            Log.Debug($"Found potential roles dictionary via property: {prop.Name}");
                            _rolesDict = value;
                        }
                    }
                }

                if (_rolesDict == null)
                {
                    Log.Error("Could not find Roles dictionary on plugin instance");
                    return false;
                }

                return PackageCustomRoleType != null && PackageStartTeamType != null;
            }
            catch (Exception ex)
            {
                Log.Error($"Exception in Initialize: {ex.Message}\n{ex.StackTrace}");
                return false;
            }
        }

        public object ConvertStartTeam(StartTeam team)
        {
            return Enum.ToObject(PackageStartTeamType, (int)team);
        }

        public void AddRoleToTeam(object team, CustomRole role, uint count)
        {
            try
            {
                if (_rolesDict == null)
                {
                    Log.Error("Roles dictionary is null");
                    return;
                }

                Type dictType = _rolesDict.GetType();

                bool containsKey = (bool)dictType.GetMethod("ContainsKey").Invoke(_rolesDict, new[] { team });
                if (!containsKey)
                {
                    Log.Debug($"Creating new list for team {team}");
                    Type valueType = dictType.GetGenericArguments()[1];
                    object newList = Activator.CreateInstance(valueType);

                    dictType.GetMethod("Add").Invoke(_rolesDict, new[] { team, newList });
                }

                object rolesList = dictType.GetProperty("Item").GetValue(_rolesDict, new[] { team });

                uint actualCount = Math.Min(count, role.SpawnProperties.Limit);
                Log.Debug($"Adding {actualCount} of role {role.Name} to team {team} (limit: {role.SpawnProperties.Limit})");

                Type listType = rolesList.GetType();
                MethodInfo addMethod = listType.GetMethod("Add");
                Type targetInterfaceType = addMethod.GetParameters()[0].ParameterType;

                for (uint i = 0; i < actualCount; i++)
                {
                    try {
                        Log.Debug($"Adding role {role.Name} to team {team}");
                
                        object targetRole = null;
                
                        Type concreteType = targetInterfaceType.Assembly.GetTypes()
                            .FirstOrDefault(t => !t.IsInterface && !t.IsAbstract && targetInterfaceType.IsAssignableFrom(t));
                
                        if (concreteType != null)
                        {
                            targetRole = Activator.CreateInstance(concreteType);
                    
                            foreach (var prop in targetInterfaceType.GetProperties())
                            {
                                if (!prop.CanWrite) continue;
                        
                                var sourceProp = role.GetType().GetProperty(prop.Name);
                                if (sourceProp != null)
                                {
                                    try
                                    {
                                        var value = sourceProp.GetValue(role);
                                        prop.SetValue(targetRole, value);
                                    }
                                    catch {}
                                }
                            }
                        }
                
                        if (targetRole != null)
                        {
                            addMethod.Invoke(rolesList, new[] { targetRole });
                        }
                    }
                    catch (Exception ex) {
                        Log.Error($"Error processing role {role.Name}: {ex.Message}");
                    }
                }

                PropertyInfo countProperty = listType.GetProperty("Count");
                int elementsCount = (int)countProperty.GetValue(rolesList);
                Log.Debug($"Roles {team} now has {elementsCount} elements.");
            }
            catch (Exception ex)
            {
                Log.Error($"Error adding role to team: {ex.Message}\n{ex.StackTrace}");
            }
        }

        public Dictionary<object, List<object>> GetRolesDict()
        {
            try
            {
                if (_rolesDict == null)
                {
                    Log.Error("Roles dictionary is null");
                    return new Dictionary<object, List<object>>();
                }

                return _rolesDict as Dictionary<object, List<object>>;
            }
            catch (Exception ex)
            {
                Log.Error($"Error getting roles dictionary: {ex.Message}");
                return new Dictionary<object, List<object>>();
            }
        }
    }
}