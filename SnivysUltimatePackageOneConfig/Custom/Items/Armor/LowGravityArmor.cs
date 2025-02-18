using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using UnityEngine;
using YamlDotNet.Serialization;
using PlayerLab = LabApi.Features.Wrappers.Player;
using PlayerAPI = Exiled.API.Features.Player;
using PlayerEvent = Exiled.Events.Handlers.Player;

namespace SnivysUltimatePackageOneConfig.Custom.Items.Armor
{
    [CustomItem(ItemType.ArmorLight)]
    public class LowGravityArmor : CustomArmor
    {
        [YamlIgnore]
        public override ItemType Type { get; set; } = ItemType.ArmorLight;
        public override uint Id { get; set; } = 38;
        public override string Name { get; set; } = "<color=#6600CC>Nebula Carapace</color>";

        public override string Description { get; set; } =
            "This armor is so light-weight that it feels like you're far lighter";

        public override float Weight { get; set; } = 0.75f;
        public Vector3 GravityChanges { get; set; } = new Vector3()
        {
            x = 1,
            y = 0.75f,
            z = 1
        };
        [YamlIgnore]
        private List<PlayerAPI> _playersWithArmorOn = new List<PlayerAPI>();
        public override SpawnProperties SpawnProperties { get; set; }
        
        protected override void SubscribeEvents()
        {
            PlayerEvent.Dying += OnDying;
            base.SubscribeEvents();
        }
        protected override void UnsubscribeEvents()
        {
            PlayerEvent.Dying -= OnDying;
            base.UnsubscribeEvents();
        }
        
        protected override void OnPickingUp(PickingUpItemEventArgs ev)
        {
            if (!_playersWithArmorOn.Contains(ev.Player))
            {
                Log.Warn($"VVUP Custom Items: Low Gravity Armor. Hi this is a WIP item using the new LabAPI stuff, this is effectively a debug statement without the debug flag on.\n{ev.Player.Nickname} has put on Low Gravity Armor, setting gravity to {GravityChanges}.\nFeedback is greatly appreciated, even if I say something snarky");
                Log.Warn($"VVUP Custom Items: Low Gravity Armor, {ev.Player.Nickname} gravity is currently {PlayerLab.Get(ev.Player.NetworkIdentity).Gravity.ToString()}");
                _playersWithArmorOn.Add(ev.Player);
                PlayerLab.Get(ev.Player.NetworkIdentity).Gravity = GravityChanges;
                Log.Warn($"VVUP Custom Items: Low Gravity Armor, {ev.Player.Nickname} gravity is currently {PlayerLab.Get(ev.Player.NetworkIdentity).Gravity.ToString()}");
            }
        }

        protected override void OnDroppingItem(DroppingItemEventArgs ev)
        {
            if (_playersWithArmorOn.Contains(ev.Player))
            {
                Log.Warn($"VVUP Custom Items: Low Gravity Armor, {ev.Player.Nickname} gravity is currently {PlayerLab.Get(ev.Player.NetworkIdentity).Gravity.ToString()}");
                PlayerLab.Get(ev.Player.NetworkIdentity).Gravity = new Vector3(1, 1, 1);
                Log.Warn($"VVUP Custom Items: Low Gravity Armor, {ev.Player.Nickname} gravity is currently {PlayerLab.Get(ev.Player.NetworkIdentity).Gravity.ToString()}");
                _playersWithArmorOn.Remove(ev.Player);
            }
        }

        private void OnDying(DyingEventArgs ev)
        {
            if (_playersWithArmorOn.Contains(ev.Player))
            {
                Log.Warn($"VVUP Custom Items: Low Gravity Armor, {ev.Player.Nickname} gravity is currently {PlayerLab.Get(ev.Player.NetworkIdentity).Gravity.ToString()}");
                PlayerLab.Get(ev.Player.NetworkIdentity).Gravity = new Vector3(1, 1, 1);
                Log.Warn($"VVUP Custom Items: Low Gravity Armor, {ev.Player.Nickname} gravity is currently {PlayerLab.Get(ev.Player.NetworkIdentity).Gravity.ToString()}");
                _playersWithArmorOn.Remove(ev.Player);
            }
        }
    }
}