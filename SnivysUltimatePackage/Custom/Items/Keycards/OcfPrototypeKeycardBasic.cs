using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Pickups;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.EventArgs;
using Exiled.CustomItems.API.Features;
using UnityEngine;
using YamlDotNet.Serialization;
using OperationCrossFire = SnivysUltimatePackage.EventHandlers.ServerEventsEventHandlers.OperationCrossfireEventHandlers;

namespace SnivysUltimatePackage.Custom.Items.Keycards
{
    [CustomItem(ItemType.KeycardJanitor)]
    public class OcfPrototypeKeycardBasic : CustomItem
    {
        [YamlIgnore] 
        public override ItemType Type { get; set; } = ItemType.KeycardJanitor;
        public override uint Id { get; set; } = 44;
        public override string Name { get; set; } = "Prototype Keycard Basic";

        public override string Description { get; set; } =
            "This is the basic Prototype Keycard needed to unlock SCP-914";

        public override float Weight { get; set; } = 0.5f;

        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            Limit = 1,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
            {
                new()
                {
                    Chance = 0,
                    Location = SpawnLocationType.InsideHidChamber,
                },
                new()
                {
                    Chance = 0,
                    Location = SpawnLocationType.InsideHczArmory,
                },
                new()
                {
                    Chance = 0,
                    Location = SpawnLocationType.Inside049Armory,
                },
                new()
                {
                    Chance = 0,
                    Location = SpawnLocationType.InsideGr18Glass,
                },
                new()
                {
                    Chance = 0,
                    Location = SpawnLocationType.Inside106Primary,
                },
                new()
                {
                    Chance = 0,
                    Location = SpawnLocationType.Inside330,
                },
                new()
                {
                    Chance = 0,
                    Location = SpawnLocationType.Inside173Gate,
                },
            }
        };
        
        public uint RefinedKeycardId { get; set; } = 45;

        protected override void OnUpgrading(UpgradingEventArgs ev)
        {
            ev.IsAllowed = false;
            ev.Item.DestroySelf();
            if (OperationCrossFire.OcfStarted)
                TrySpawn(RefinedKeycardId, ev.OutputPosition, out var pickup);
        }
    }
}