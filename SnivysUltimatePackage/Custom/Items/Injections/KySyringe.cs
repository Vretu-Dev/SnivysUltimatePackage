using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Player = Exiled.Events.Handlers.Player;

namespace SnivysUltimatePackage.Custom.Items.Injections
{
    [CustomItem(ItemType.Adrenaline)]
    public class KySyringe : CustomItem
    {
        private bool KillAfterAnimation { get; set; } = true;
        private string KillReason { get; set; } = "Intentional Fatal Injection";
        public override uint Id { get; set; } = 42;
        public override string Name { get; set; } = "<color=#0000CC>LJ-429</color>";
        public override string Description { get; set; } = "When injected, the user has a quick death.";
        public override float Weight { get; set; } = 1.15f;

        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties()
        {
            Limit = 1,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
            {
                new DynamicSpawnPoint()
                {
                    Chance = 100,
                    Location = SpawnLocationType.Inside096,
                }
            }
        };
        protected override void SubscribeEvents()
        {
            if (KillAfterAnimation)
                Player.UsingItemCompleted += OnUsingLJAnimation;
            else
                Player.UsingItem += OnUsingLJ;

            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            if (KillAfterAnimation)
                Player.UsingItemCompleted -= OnUsingLJAnimation;
            else
                Player.UsingItem -= OnUsingLJ;

            base.UnsubscribeEvents();
        }
        private void OnUsingLJ(UsingItemEventArgs ev)
        {
            if (!Check(ev.Player.CurrentItem))
                return;
            ev.Player.Kill(KillReason);
            ev.Player.Health = 1f;
            ev.Player.EnableEffect(EffectType.Bleeding, 500f);
            ev.Player.EnableEffect(EffectType.Corroding, 500f);
        }

        private void OnUsingLJAnimation(UsingItemCompletedEventArgs ev)
        {
            if (!Check(ev.Player.CurrentItem))
                return;
            ev.Player.Kill(KillReason);
            ev.Player.Health = 1f;
            ev.Player.EnableEffect(EffectType.Bleeding, 500f);
            ev.Player.EnableEffect(EffectType.Corroding, 500f);
        }
    }
}