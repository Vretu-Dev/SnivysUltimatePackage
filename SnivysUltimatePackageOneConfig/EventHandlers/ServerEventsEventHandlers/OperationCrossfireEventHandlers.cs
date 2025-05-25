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
using SnivysUltimatePackageOneConfig.Configs.ServerEventsConfigs;
using Map = Exiled.API.Features.Map;
using Round = Exiled.API.Features.Round;
using Warhead = Exiled.API.Features.Warhead;

namespace SnivysUltimatePackageOneConfig.EventHandlers.ServerEventsEventHandlers
{
    public class OperationCrossfireEventHandlers
    {
        public static OperationCrossfireEventHandlers Instance { get; private set; }
        
        private static OperationCrossfireConfig _config;
        public static bool OcfStarted = false;
        
        // Objectives
        public static bool _scp914LockdownOverridden = false;
        public static bool _scientistsEscorted = false;
        public static bool _prototypeDeviceRefined = false;
        
        // Player Tracking
        private static List<PlayerAPI> _mtfPlayers = new List<PlayerAPI>();
        private static List<PlayerAPI> _scientistPlayers = new List<PlayerAPI>();
        private List<PlayerAPI> _classDPlayers = new List<PlayerAPI>();
        
        private List<PlayerAPI> _playersSpectating = new List<PlayerAPI>();
        
        private CoroutineHandle _ocfCoroutine;
        
        public OperationCrossfireEventHandlers()
        {
            if (OcfStarted) return;
            
            OcfStarted = true;
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
            Log.Debug(
                $"VVUP Custom Events: Operation Crossfire: Starting Warhead and setting its time to {_config.EventDuration} seconds");
            Warhead.Start();
            Warhead.DetonationTimer = _config.EventDuration;
            Warhead.IsLocked = true;

            DecontaminationController.Singleton.DecontaminationOverride =
                DecontaminationController.DecontaminationStatus.Disabled;

            Instance = this;

            // Player Setup
            Timing.CallDelayed(0.5f, () =>
            {
                for (int i = 0; i < mtfCount && i < playerEnumerable.Length; i++)
                {
                    Log.Debug(
                        $"VVUP Custom Events: Operation Crossfire: Adding {playerEnumerable[i].Nickname} to MTF side");
                    playerEnumerable[i].Role.Set(RoleTypeId.NtfSergeant);
                    _mtfPlayers.Add(playerEnumerable[i]);
                    string mtfObjective =
                        $"{_config.MtfScientistObjective1}\n{_config.MtfScientistObjective2}\n{_config.MtfObjective3}";
                    _mtfPlayers[i].Broadcast((ushort)_config.StartingBroadcastTime, mtfObjective);
                    Log.Debug(
                        $"VVUP Custom Events: Operation Crossfire: There are now {_mtfPlayers.Count} MTF players");
                }

                for (int i = mtfCount; i < mtfCount + scientistCount && i < playerEnumerable.Length; i++)
                {
                    Log.Debug(
                        $"VVUP Custom Events: Operation Crossfire: Adding {playerEnumerable[i].Nickname} to Scientist side");
                    playerEnumerable[i].Role.Set(RoleTypeId.Scientist);
                    _scientistPlayers.Add(playerEnumerable[i]);
                    string scientistObjective =
                        $"{_config.MtfScientistObjective1}\n{_config.MtfScientistObjective2}\n{_config.ScientistObjective3}";
                    _mtfPlayers[i].Broadcast((ushort)_config.StartingBroadcastTime, scientistObjective);
                    Log.Debug(
                        $"VVUP Custom Events: Operation Crossfire: There are now {_scientistPlayers.Count} Scientist Players");
                }

                for (int i = mtfCount + scientistCount; i < playerEnumerable.Length; i++)
                {
                    Log.Debug(
                        $"VVUP Custom Events: Operation Crossfire: Adding {playerEnumerable[i].Nickname} to D-Class side");
                    playerEnumerable[i].Role.Set(RoleTypeId.ClassD);
                    _classDPlayers.Add(playerEnumerable[i]);
                    string dClassObjective =
                        $"{_config.ClassDObjective1}\n{_config.ClassDObjective2}";
                    _mtfPlayers[i].Broadcast((ushort)_config.StartingBroadcastTime, dClassObjective);
                    Log.Debug(
                        $"VVUP Custom Events: Operation Crossfire: There are now {_classDPlayers.Count} D-Class Players");
                }

                // Event Handlers Setup
                PlayerEvent.Verified += OnPlayerJoin;
                PlayerEvent.Died += OnPlayerDied;
                PlayerEvent.Left += OnPlayerLeave;
                PlayerEvent.InteractingDoor += OnDoorInteract;

                // Spawn Basic Keycard
                var customKeycardBasic = CustomItem.Get(_config.PrototypeKeycardBasicId);
                if (customKeycardBasic != null && customKeycardBasic.SpawnProperties != null)
                {
                    var spawnPoints = customKeycardBasic.SpawnProperties.DynamicSpawnPoints;
                    var random = new Random();
                    var selected = spawnPoints[random.Next(spawnPoints.Count)];
                    var position = selected.Position;
                    customKeycardBasic.Spawn(position);
                    Log.Debug(
                        $"VVUP Custom Events: Operation Crossfire: Spawned {customKeycardBasic.Name} at {position}");
                }
                
                // Start the Routine Proper
                _ocfCoroutine = Timing.RunCoroutine(OperationCrossfireTiming());
            });
        }

        public static IEnumerator<float> OperationCrossfireTiming()
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
                        yield break;
                    }
                    if (totalScientists - escapedScientists <= 0 && percentMtfAlive < _config.MtfPercentageRequiredToWin)
                    {
                        Map.Broadcast((ushort)_config.EndOfRoundTime, _config.ClassDWinMessage);
                        yield return Timing.WaitForSeconds(_config.EndOfRoundTime);
                        yield break;
                    }
                    if (Warhead.IsDetonated)
                    {
                        Map.Broadcast((ushort)_config.EndOfRoundTime, _config.TieMessage);
                        yield return Timing.WaitForSeconds(_config.EndOfRoundTime);
                        yield break;
                    }
                }
                yield return Timing.WaitForSeconds(_config.CheckForEventsInterval);
            }
        }
        
        public void OnPlayerJoin(VerifiedEventArgs ev)
        {
            if (!OcfStarted) return;
            Log.Debug($"VVUP Custom Events: Operation Crossfire: Player {ev.Player.Nickname} has joined the server, setting them to Overwatch");
            ev.Player.Role.Set(RoleTypeId.Overwatch);
            ev.Player.Broadcast((ushort)_config.PlayerConnectDuringEventMessageDisplayDuration, _config.PlayerConnectDuringEventMessage);
            _playersSpectating.Add(ev.Player);
        }

        public void OnPlayerDied(DiedEventArgs ev)
        {
            if (!OcfStarted) return;
            Log.Debug($"VVUP Custom Events: Operation Crossfire: Player {ev.Player.Nickname} has died, setting them to Overwatch");
            Timing.CallDelayed(0.5f, () => ev.Player.Role.Set(RoleTypeId.Overwatch));
            _playersSpectating.Add(ev.Player);
        }
        
        public void OnPlayerLeave(LeftEventArgs ev)
        {
            if (!OcfStarted) return;
            Log.Debug($"VVUP Custom Events: Operation Crossfire: Player {ev.Player.Nickname} has left the server, removing them from the list");
            if (_playersSpectating.Contains(ev.Player))
                _playersSpectating.Remove(ev.Player);
        }

        public void OnDoorInteract(InteractingDoorEventArgs ev)
        {
            if (!OcfStarted) return;
            if (ev.Door.Type == DoorType.Scp914Gate
                && ev.Player.CurrentItem != null
                && CustomItem.TryGet(ev.Player.CurrentItem, out var customItem)
                && customItem != null
                && customItem.Id == _config.PrototypeKeycardBasicId
                && (_scientistPlayers.Contains(ev.Player) || _mtfPlayers.Contains(ev.Player))
                && !_scp914LockdownOverridden)
            {
                ev.Door.IsOpen = true;
                _scp914LockdownOverridden = true;
                Log.Debug($"VVUP Custom Events: Operation Crossfire: Player {ev.Player.Nickname} has opened SCP-914, overriding the lockdown of SCP-914");
            }
        }

        public void EndEvent()
        {
            if (!OcfStarted) return;
            Log.Debug("VVUP Custom Events: Operation Crossfire: Ending event");
            OcfStarted = false;
            Timing.KillCoroutines(_ocfCoroutine);
            PlayerEvent.Verified -= OnPlayerJoin;
            PlayerEvent.Died -= OnPlayerDied;
            PlayerEvent.Left -= OnPlayerLeave;
            PlayerEvent.InteractingDoor -= OnDoorInteract;
            foreach (PlayerAPI player in PlayerAPI.List)
            {
                Log.Debug($"VVUP Custom Events: Operation Crossfire: Killing {player.Nickname}");
                player.Role.Set(RoleTypeId.Spectator);
            }
            foreach (PlayerAPI player in _playersSpectating.ToList())
            {
                Log.Debug($"VVUP Custom Events: Operation Crossfire: Setting {player.Nickname} back to Spectator");
                player.Role.Set(RoleTypeId.Spectator);
                _playersSpectating.Remove(player);
            }
        }
    }
}