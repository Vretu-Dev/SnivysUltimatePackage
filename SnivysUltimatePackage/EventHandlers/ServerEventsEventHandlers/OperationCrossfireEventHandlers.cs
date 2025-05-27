using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using LightContainmentZoneDecontamination;
using MEC;
using PlayerRoles;
using PlayerAPI = Exiled.API.Features.Player;
using PlayerEvent = Exiled.Events.Handlers.Player;
using SnivysUltimatePackage.Configs.ServerEventsConfigs;
using Map = Exiled.API.Features.Map;
using Round = Exiled.API.Features.Round;
using Warhead = Exiled.API.Features.Warhead;
using Exiled.API.Extensions;

namespace SnivysUltimatePackage.EventHandlers.ServerEventsEventHandlers
{
    public class OperationCrossfireEventHandlers
    {
        private static OperationCrossfireConfig _config;
        public static bool OcfStarted;
        
        // Objectives
        public static bool _scp914LockdownOverridden = false;
        public static bool _scientistsEscorted = false;
        public static bool _prototypeDeviceRefined = false;
        
        // Player Tracking
        public static List<PlayerAPI> _mtfPlayers = new List<PlayerAPI>();
        public static List<PlayerAPI> _scientistPlayers = new List<PlayerAPI>();
        public static List<PlayerAPI> _classDPlayers = new List<PlayerAPI>();
        
        public static List<PlayerAPI> _playersSpectating = new List<PlayerAPI>();
        
        public static CoroutineHandle _ocfCoroutine;
        
        public OperationCrossfireEventHandlers()
        {
            if (OcfStarted)
            {
                Log.Debug($"VVUP Custom Events: Operation Crossfire: Event is already running");
                return;
            }

            OcfStarted = true;
            Plugin.ActiveEvent += 1;
            
            Random random = new Random();

            _config = Plugin.Instance.Config.ServerEventsMasterConfig.OperationCrossfireConfig;

            foreach (PlayerAPI player in PlayerAPI.List)
            {
                Log.Debug($"VVUP Custom Events: Operation Crossfire: Killing {player.Nickname}");
                player.Role.Set(RoleTypeId.Spectator);
            }

            var allPlayers = PlayerAPI.List.Where(p => p.Role != RoleTypeId.Overwatch);

            var playerEnumerable = allPlayers as PlayerAPI[] ?? allPlayers.ToArray();
            int total = playerEnumerable.Count();
            int mtfCount = (int)Math.Round(total * _config.MtfRatio);
            int scientistCount = (int)Math.Round(total * _config.ScientistRatio);
            int classDCount = total - mtfCount - scientistCount;
            
            Log.Debug("VVUP Custom Events: Operation Crossfire: Locking Round");
            Round.IsLocked = true;
            Log.Debug("VVUP Custom Events: Operation Crossfire: Locking Warhead");
            Warhead.IsLocked = true;
            Timing.CallDelayed(_config.EventDuration - 90, () =>
            {
                Log.Debug("VVUP Custom Events: Operation Crossfire: Starting Warhead");
                Warhead.IsLocked = false;
                Warhead.Start();
                Warhead.IsLocked = true;
            });
            Map.IsDecontaminationEnabled = false;
            
            DecontaminationController.Singleton.DecontaminationOverride =
                DecontaminationController.DecontaminationStatus.Disabled;
            
            // Event Handlers Setup
            PlayerEvent.Verified += Plugin.Instance.ServerEventsMainEventHandler.OnPlayerJoinOcf;
            PlayerEvent.Died += Plugin.Instance.ServerEventsMainEventHandler.OnPlayerDiedOcf;
            PlayerEvent.Left += Plugin.Instance.ServerEventsMainEventHandler.OnPlayerLeaveOcf;
            PlayerEvent.InteractingDoor += Plugin.Instance.ServerEventsMainEventHandler.OnDoorInteractOcf;
            PlayerEvent.Hurting += Plugin.Instance.ServerEventsMainEventHandler.OnHurtingOcf;

            // Spawn Basic Keycard
            var customKeycardBasic = CustomItem.Get(_config.PrototypeKeycardBasicId);
            if (customKeycardBasic != null && customKeycardBasic.SpawnProperties != null)
            {
                var spawnPoints = customKeycardBasic.SpawnProperties.DynamicSpawnPoints;
                var selected = spawnPoints[random.Next(spawnPoints.Count)];
                var position = selected.Location.GetPosition();
                customKeycardBasic.Spawn(position);
                Log.Debug(
                    $"VVUP Custom Events: Operation Crossfire: Spawned {customKeycardBasic.Name} at {position}");
            }

            // Player Setup
            Timing.CallDelayed(0.5f, () =>
            {
                var shuffledPlayers = playerEnumerable.OrderBy(_ => Guid.NewGuid()).ToArray(); // Shuffle players to randomize assignments

                int assignedPlayers = 0;

                for (int i = 0; i < mtfCount && assignedPlayers < shuffledPlayers.Length; i++, assignedPlayers++)
                {
                    Log.Debug($"VVUP Custom Events: Operation Crossfire: Adding {shuffledPlayers[assignedPlayers].Nickname} to MTF side");
                    shuffledPlayers[assignedPlayers].Role.Set(RoleTypeId.NtfSergeant);
                    _mtfPlayers.Add(shuffledPlayers[assignedPlayers]);
                    string mtfObjective = $"{_config.MtfScientistObjective1}\n{_config.MtfScientistObjective2}\n{_config.MtfObjective3}";
                    shuffledPlayers[assignedPlayers].Broadcast((ushort)_config.StartingBroadcastTime, mtfObjective);
                }

                for (int i = 0; i < scientistCount && assignedPlayers < shuffledPlayers.Length; i++, assignedPlayers++)
                {
                    Log.Debug($"VVUP Custom Events: Operation Crossfire: Adding {shuffledPlayers[assignedPlayers].Nickname} to Scientist side");
                    shuffledPlayers[assignedPlayers].Role.Set(RoleTypeId.Scientist);
                    _scientistPlayers.Add(shuffledPlayers[assignedPlayers]);
                    string scientistObjective = $"{_config.MtfScientistObjective1}\n{_config.MtfScientistObjective2}\n{_config.ScientistObjective3}";
                    shuffledPlayers[assignedPlayers].Broadcast((ushort)_config.StartingBroadcastTime, scientistObjective);
                }

                for (int i = 0; i < classDCount && assignedPlayers < shuffledPlayers.Length; i++, assignedPlayers++)
                {
                    Log.Debug($"VVUP Custom Events: Operation Crossfire: Adding {shuffledPlayers[assignedPlayers].Nickname} to D-Class side");
                    shuffledPlayers[assignedPlayers].Role.Set(RoleTypeId.ClassD);
                    _classDPlayers.Add(shuffledPlayers[assignedPlayers]);
                    string dClassObjective = $"{_config.ClassDObjective1}\n{_config.ClassDObjective2}";
                    shuffledPlayers[assignedPlayers].Broadcast((ushort)_config.StartingBroadcastTime, dClassObjective);
                    shuffledPlayers[assignedPlayers].AddAmmo(AmmoType.Ammo12Gauge, 24);
                    shuffledPlayers[assignedPlayers].AddAmmo(AmmoType.Ammo44Cal, 24);
                    shuffledPlayers[assignedPlayers].AddAmmo(AmmoType.Nato9, 40);
                    shuffledPlayers[assignedPlayers].AddAmmo(AmmoType.Nato556, 40);
                    shuffledPlayers[assignedPlayers].AddAmmo(AmmoType.Nato762, 40);
                    shuffledPlayers[assignedPlayers].AddItem(_config.ClassDKeycard);
                    shuffledPlayers[assignedPlayers]
                        .AddItem(_config.ClassDFirearms[random.Next(_config.ClassDFirearms.Count)]);
                }
                
                // Start the Routine Proper
                _ocfCoroutine = Timing.RunCoroutine(OperationCrossfireTiming());
            });
        }

        public IEnumerator<float> OperationCrossfireTiming()
        {
            for (;;)
            {
                int totalScientists = _scientistPlayers.Count;
                if (totalScientists > 0)
                {
                    int escapedScientists = _scientistPlayers.Count(p =>
                        p.Role == RoleTypeId.Scientist && p.Zone == ZoneType.Surface);

                    float percentEscaped = (float)escapedScientists / totalScientists;
                    if (percentEscaped >= _config.ScientistPercentageRequiredToWin)
                    {
                        _scientistsEscorted = true;
                    }
                    int mtfAlive = _mtfPlayers.Count(p => p.Role.Team == Team.FoundationForces);
                    float percentMtfAlive = (float)mtfAlive / _mtfPlayers.Count;
                    if (_prototypeDeviceRefined && _scientistsEscorted && _scp914LockdownOverridden &&
                        percentMtfAlive >= _config.MtfPercentageRequiredToWin)
                    {
                        Map.Broadcast((ushort)_config.EndOfRoundTime, _config.MtfScientistWinMessage);
                        yield return Timing.WaitForSeconds(_config.EndOfRoundTime);
                        EndEvent();
                        yield break;
                    }
                    if (totalScientists - escapedScientists <= 0 && percentMtfAlive < _config.MtfPercentageRequiredToWin)
                    {
                        Map.Broadcast((ushort)_config.EndOfRoundTime, _config.ClassDWinMessage);
                        yield return Timing.WaitForSeconds(_config.EndOfRoundTime);
                        EndEvent();
                        yield break;
                    }
                    if (Warhead.IsDetonated)
                    {
                        Map.Broadcast((ushort)_config.EndOfRoundTime, _config.TieMessage);
                        yield return Timing.WaitForSeconds(_config.EndOfRoundTime);
                        EndEvent();
                        yield break;
                    }
                }
                yield return Timing.WaitForSeconds(_config.CheckForEventsInterval);
            }
        }

        public static void EndEvent()
        {
            if (!OcfStarted) return;
            Log.Debug("VVUP Custom Events: Operation Crossfire: Ending event");
            OcfStarted = false;
            Timing.KillCoroutines(_ocfCoroutine);
            PlayerEvent.Verified -= Plugin.Instance.ServerEventsMainEventHandler.OnPlayerJoinOcf;
            PlayerEvent.Died -= Plugin.Instance.ServerEventsMainEventHandler.OnPlayerDiedOcf;
            PlayerEvent.Left -= Plugin.Instance.ServerEventsMainEventHandler.OnPlayerLeaveOcf;
            PlayerEvent.InteractingDoor -= Plugin.Instance.ServerEventsMainEventHandler.OnDoorInteractOcf;
            foreach (PlayerAPI player in PlayerAPI.List)
            {
                Log.Debug($"VVUP Custom Events: Operation Crossfire: Killing {player.Nickname}");
                if (player.Role == RoleTypeId.Overwatch && _playersSpectating!.Contains(player))
                    continue;
                player.Role.Set(RoleTypeId.Tutorial);
            }
            foreach (PlayerAPI player in _playersSpectating.ToList())
                _playersSpectating.Remove(player);
            foreach (PlayerAPI player in _classDPlayers.ToList())
                _classDPlayers.Remove(player);
            foreach (PlayerAPI player in _mtfPlayers.ToList())
                _mtfPlayers.Remove(player);
            foreach (PlayerAPI player in _scientistPlayers.ToList())
                _scientistPlayers.Remove(player);

            Plugin.ActiveEvent -= 1;
            if (!Warhead.IsDetonated)
            {
                Warhead.DetonationTimer = 90;
                Warhead.IsLocked = false;
                Warhead.Stop();
            }
        }
    }
}