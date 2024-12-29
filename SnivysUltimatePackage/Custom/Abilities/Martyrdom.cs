using System.ComponentModel;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using Item = Exiled.API.Features.Items.Item;
using Player = Exiled.Events.Handlers.Player;

namespace SnivysUltimatePackage.Custom.Abilities
{
    [CustomAbility]
    public class Martyrdom : PassiveAbility
    {
        public override string Name { get; set; } = "Martyrdom";

        public override string Description { get; set; } = "Causes the player to explode upon death.";

        [Description("How long should the fuse be?")]
        public float ExplosiveFuse { get; set; } = 3f;

        protected override void SubscribeEvents()
        {
            Player.Dying += OnDying;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Player.Dying -= OnDying;
            base.UnsubscribeEvents();
        }

        private void OnDying(DyingEventArgs ev)
        {
            if (Check(ev.Player))
            {
                Log.Debug($"VVUP Custom Abilities: Spawning Grenade at {ev.Player.Nickname} death location");
                ExplosiveGrenade grenade = (ExplosiveGrenade)Item.Create(ItemType.GrenadeHE);
                grenade.FuseTime = ExplosiveFuse;
                grenade.SpawnActive(ev.Player.Position);
            }
        }
    }
}