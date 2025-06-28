using System.ComponentModel;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Components;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups.Projectiles;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using JetBrains.Annotations;
using UnityEngine;
using YamlDotNet.Serialization;
using Firearm = Exiled.API.Features.Items.Firearm;

namespace SnivysUltimatePackageOneConfig.Custom.Items.Firearms
{
    [CustomItem(ItemType.GunLogicer)]
    public class GrenadeLauncherImpact : CustomWeapon
    {
        [YamlIgnore]
        public override ItemType Type { get; set; } = ItemType.GunLogicer;
        public override uint Id { get; set; } = 46;
        public override string Name { get; set; } = "<color=#FF0000>ADATS-I</color>";
        public override string Description { get; set; } = "A grenade launcher that explodes on impact.";
        public override float Weight { get; set; } = 3;
        public override SpawnProperties SpawnProperties { get; set; }
        public override byte ClipSize { get; set; } = 1;
        [Description("Set to false if you want to include Custom Grenades (such as Cluster Grenades) in the grenade launcher. NOTE: This will not make the grenade launcher launch a Cluster Grenade")]
        public bool IgnoreCustomGrenades { get; set; } = true;

        public float GrenadeFuseTime { get; set; } = 1.5f;
        public bool UseGrenadesToReload { get; set; } = true;
        [Description("If true, players can hold down fire and it will become a grenade firehose")]
        public bool AllowFiringDuringReload { get; set; } = false;
        
        private ProjectileType GrenadeType { get; set; } = ProjectileType.FragGrenade;
        [CanBeNull] 
        private CustomGrenade loadedCustomGrenade;
        private bool grenadeLauncherEmpty = false;
        private bool fakeAmmoGiven = false;
        private bool isReloading = false;

        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();
            Exiled.Events.Handlers.Player.DryfiringWeapon += OnDryfiringWeapon;
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.DryfiringWeapon += OnDryfiringWeapon;
            base.UnsubscribeEvents();
        }

        protected override void OnShooting(ShootingEventArgs ev)
        {
            ev.IsAllowed = false;

            if (ev.Player.CurrentItem is Firearm firearm)
            {
                firearm.MagazineAmmo -= 1;
                if (firearm.MagazineAmmo == 0 && UseGrenadesToReload)
                {
                    ev.Player.AddAmmo(AmmoType.Nato762, 1);
                    grenadeLauncherEmpty = true;
                }

                if (!AllowFiringDuringReload && isReloading)
                    ev.IsAllowed = false;
            }

            Vector3 position = ev.Player.CameraTransform.TransformPoint(new Vector3(0.0715f, 0.0225f, 0.45f));
            Projectile projectile;
            Log.Debug($"VVUP Custom Items: Grenade Launcher Impact: {ev.Player.Nickname} fired, firing a {GrenadeType}");
            switch (GrenadeType)
            {
                case ProjectileType.Flashbang:
                    projectile = ev.Player.ThrowGrenade(GrenadeType).Projectile;
                    break;
                case ProjectileType.Scp018:
                    projectile = ev.Player.ThrowGrenade(GrenadeType).Projectile;
                    break;
                case ProjectileType.Scp2176:
                    projectile = ev.Player.ThrowGrenade(GrenadeType).Projectile;
                    break;
                // Remind me to put in the Snowball and Coals during the winter event, would be funny.
                default:
                    projectile = ev.Player.ThrowGrenade(GrenadeType).Projectile;
                    break;
            }

            projectile.GameObject.AddComponent<CollisionHandler>().Init(ev.Player.GameObject, projectile.Base);
        }

        protected override void OnReloading(ReloadingWeaponEventArgs ev)
        {
            if (!Check(ev.Player.CurrentItem))
                return;
            isReloading = true;
            if (UseGrenadesToReload)
            {
                if (!(ev.Player.CurrentItem is Firearm firearm) || firearm.MagazineAmmo >= ClipSize)
                {
                    Log.Debug($"VVUP Custom Items: Grenade Launcher Impact: {ev.Player.Nickname} tried to reload the Grenade Launcher Impact, but it is already full.");
                    return;
                }

                Log.Debug($"VVUP Custom Items: Grenade Launcher Impact: {ev.Player.Nickname} is reloading the Grenade Launcher Impact with grenades.");
                foreach (Item item in ev.Player.Items.ToList())
                {
                    Log.Debug($"VVUP Custom Items: Grenade Launcher Impact: {ev.Player.Nickname} has {item.Type}");
                    if (item.Type != ItemType.GrenadeHE && item.Type != ItemType.GrenadeFlash &&
                        item.Type != ItemType.SCP018 && item.Type != ItemType.SCP2176)
                    {
                        Log.Debug($"VVUP Custom Items: Grenade Launcher Impact: {ev.Player.Nickname} has a {item.Type}, not a grenade, skipping.");
                        continue;
                    }

                    if (TryGet(item, out CustomItem? customItem))
                    {
                        if (IgnoreCustomGrenades)
                        {
                            Log.Debug($"VVUP Custom Items: Grenade Launcher Impact: {ev.Player.Nickname} has a {item.Type}, but it's a custom grenade, skipping.");
                            continue;
                        }

                        if (customItem is CustomGrenade customGrenade)
                        {
                            loadedCustomGrenade = customGrenade;
                            Log.Debug($"VVUP Custom Items: Grenade Launcher Impact: {ev.Player.Nickname} has a {item.Type}, it's a custom grenade, setting it to {loadedCustomGrenade.Name}");
                        }
                    }
                    ev.Player.DisableEffect(EffectType.Invisible);
                    GrenadeType = item.Type switch
                    {
                        ItemType.GrenadeFlash => ProjectileType.Flashbang,
                        ItemType.SCP018 => ProjectileType.Scp018,
                        ItemType.SCP2176 => ProjectileType.Scp2176,
                        // Remind me to put in the Snowball and Coals during the winter event, would be funny.
                        _ => ProjectileType.FragGrenade
                    };
                    ev.Player.RemoveItem(item);
                    Log.Debug($"VVUP Custom Items: Grenade Launcher Impact: {ev.Player.Nickname} reloaded the Grenade Launcher Impact with a {GrenadeType} grenade.");
                    return;
                }

                ev.IsAllowed = false;
                Log.Debug($"VVUP Custom Items: Grenade Launcher Impact: {ev.Player.Nickname} had no grenades to reload with.");
                return;
            }
            Log.Debug($"VVUP Custom Items: Grenade Launcher Impact: {ev.Player.Nickname} reloaded the Grenade Launcher Impact with regular ammo.");
        }

        protected override void OnReloaded(ReloadedWeaponEventArgs ev)
        {
            if (ev.Player.CurrentItem is Firearm firearm)
            {
                firearm.MagazineAmmo = ClipSize;
                grenadeLauncherEmpty = false;
                fakeAmmoGiven = false;
                isReloading = false;
            }
        }

        protected override void OnDroppingAmmo(DroppingAmmoEventArgs ev)
        {
            if (ev.Player.Ammo[(ItemType)AmmoType.Nato762] == 1 && UseGrenadesToReload && grenadeLauncherEmpty && fakeAmmoGiven)
            {
                ev.IsAllowed = false;
                return;
            }
            base.OnDroppingAmmo(ev);
        }

        protected override void OnDroppingItem(DroppingItemEventArgs ev)
        {
            if (ev.Player.Ammo[(ItemType)AmmoType.Nato762] == 1 && UseGrenadesToReload && grenadeLauncherEmpty && fakeAmmoGiven)
            {
                ev.Player.SetAmmo(AmmoType.Nato762, 0);
                fakeAmmoGiven = false;
            }
            base.OnDroppingItem(ev);
        }

        protected override void OnAcquired(Player player, Item item, bool displayMessage)
        {
            if (grenadeLauncherEmpty && UseGrenadesToReload)
            {
                player.AddAmmo(AmmoType.Nato762, 1);
                fakeAmmoGiven = true;
            }
            base.OnAcquired(player, item, displayMessage);
        }

        private void OnDryfiringWeapon(DryfiringWeaponEventArgs ev)
        {
            if (Check(ev.Player.CurrentItem) && ev.Player.CurrentItem is Firearm firearm && firearm.MagazineAmmo == 0 && UseGrenadesToReload && grenadeLauncherEmpty && !fakeAmmoGiven)
            {
                ev.Player.AddAmmo(AmmoType.Nato762, 1);
                fakeAmmoGiven = true;
            }
        }
    }
}