using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using UnityEngine;
using YamlDotNet.Serialization;

namespace SnivysUltimatePackage.Custom.Items.Other
{
    [CustomItem(ItemType.Painkillers)]
    public class InfinitePills : CustomItem
    {
        public override uint Id { get; set; } = 34;
        public override string Name { get; set; } = "<color=#6600CC>Infinite Pills</color>";
        public override string Description { get; set; } = "This pill bottle seems endless\nUnfortunately it seems to be out of date and wont heal you";
        public override float Weight { get; set; } = 0.5f;
        [YamlIgnore]
        public ItemType ItemType { get; set; } = ItemType.Painkillers;
        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            Limit = 1,
            LockerSpawnPoints = new()
            {
                new LockerSpawnPoint()
                {
                    Chance = 0,
                    Type = LockerType.Misc,
                    UseChamber = true,
                    Offset = Vector3.zero,
                }
            }
        };

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsingItemCompleted += OnUsingItemCompleted;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsingItemCompleted -= OnUsingItemCompleted;
            base.UnsubscribeEvents();
        }

        private void OnUsingItemCompleted(UsingItemCompletedEventArgs ev)
        {
            if (!Check(ev.Player.CurrentItem))
                return;

            ev.IsAllowed = false;
        }
    }
}