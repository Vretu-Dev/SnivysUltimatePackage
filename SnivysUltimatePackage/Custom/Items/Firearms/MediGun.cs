using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using JetBrains.Annotations;
using MEC;
using PlayerRoles;
using YamlDotNet.Serialization;
using PlayerEvent = Exiled.Events.Handlers.Player;

namespace SnivysUltimatePackage.Custom.Items.Firearms
{
    [CustomItem(ItemType.GunFSP9)]
    public class MediGun : CustomWeapon
    {
        [YamlIgnore]
        public override ItemType Type { get; set; } = ItemType.GunFSP9;
        
        public override uint Id { get; set; } = 27;
        public override string Name { get; set; } = "<color=#0096FF>Phantom Pulse</color>";

        public override string Description { get; set; } =
            "A specialized weapon which applies healing to friendlies and revives zombies into humans";

        public override float Weight { get; set; } = 1.5f;
        
        public override float Damage { get; set; } = 0;

        public override byte ClipSize { get; set; } = 10;

        [Description("Does the medigun friendly fire? (Only an option for FF on servers)")]
        public override bool FriendlyFire { get; set; } = false;
        
        public bool HealZombies { get; set; } = true;

        [Description("1 = 100%, 0.5 = 50%, 2 = 200% healing rate")]
        public float AhpRequiredForZombieHeal { get; set; } = 200f;
        [Description("Determines if Serpents Hand can revive zombies to their side")]
        public bool ZombieHealingBySerpents { get; set; } = false;

        [Description("What is the CustomRole ID for Serpents Hand to set the revive zombie to")]
        public uint SerpentsHandCustomRoleId { get; set; } = 27;
        public float HealingModifer { get; set; } = 1f;
        [Description("Determines how much AHP human players can get")]
        public float MaxAhpAmount { get; set; } = 30f;
        [Description("Deterimines if AHP drains")]
        public bool AhpDecay { get; set; } = false;
        

        [CanBeNull]
        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            Limit = 1,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>()
            {
                new()
                {
                    Chance = 25,
                    Location = SpawnLocationType.InsideGr18,
                },
                new()
                {
                    Chance = 25,
                    Location = SpawnLocationType.InsideGateA,
                },
                new()
                {
                    Chance = 25,
                    Location = SpawnLocationType.InsideGateB,
                },
            }
        };
        
        protected override void SubscribeEvents()
        {
            PlayerEvent.ReloadingWeapon += OnReloading;
        }

        protected override void UnsubscribeEvents()
        {
            PlayerEvent.ReloadingWeapon -= OnReloading;
        }
        
        private void OnReloading(ReloadingWeaponEventArgs ev)
        {
            if (!Check(ev.Player.CurrentItem))
                return;
            Timing.CallDelayed(4f, () =>
            {
                ev.Firearm.MagazineAmmo = ClipSize - 1;
            });
        }

        protected override void OnHurting(HurtingEventArgs ev)
        {
            if (Check(ev.Attacker.CurrentItem) && ev.Attacker != ev.Player && ev.DamageHandler.Type == DamageType.Fsp9)
            {
                if (Damage != 0)
                    ev.Amount = Damage;

                if (ev.Player.Role.Side == ev.Attacker.Role.Side)
                {
                    float amount = ev.Amount * HealingModifer;
                    ev.Player.Heal(amount);
                    Log.Debug($"VVUP Custom Items: Medigun healing {ev.Player.Nickname} for {amount}");
                    if (ev.Player.Health >= 100 && ev.Player.ArtificialHealth < MaxAhpAmount)
                    {
                        float decay = 1.2f;
                        if (AhpDecay)
                            decay = 0f;
                        ev.Player.AddAhp(amount, MaxAhpAmount, decay);
                        Log.Debug($"VVUP Custom Items: Medigun adding {amount} AHP to {ev.Player.Nickname}");
                    }

                    ev.IsAllowed = false;
                }
                else if (ev.Player.Role == RoleTypeId.Scp0492 && HealZombies)
                {
                    if (!ev.Player.ActiveArtificialHealthProcesses.Any())
                        ev.Player.AddAhp(0, AhpRequiredForZombieHeal, persistant: true);
                    ev.Player.ArtificialHealth += ev.Amount;

                    if (ev.Player.ArtificialHealth >= ev.Player.MaxArtificialHealth)
                    {
                        switch (ev.Attacker.Role.Side)
                        {
                            case Side.Mtf:
                                ev.Player.Role.Set(RoleTypeId.NtfPrivate, SpawnReason.None);
                                break;
                            case Side.ChaosInsurgency:
                                ev.Player.Role.Set(RoleTypeId.ChaosConscript, SpawnReason.None);
                                break;
                            case Side.Tutorial when ZombieHealingBySerpents:
                                CustomRole.Get(SerpentsHandCustomRoleId).AddRole(ev.Player);
                                break;
                        }
                    }

                    ev.IsAllowed = false;
                }
            }
        }
    }
}