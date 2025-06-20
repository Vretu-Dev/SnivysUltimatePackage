using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;

namespace SnivysUltimatePackage.Custom.Abilities.Passive
{
    public class ApplyEffectOnHit : PassiveAbility
    {
        public override string Name { get; set; } = "Apply Effect On Hit";
        public override string Description { get; set; } = "Enables Effects to whoever you hit";
        
        public List<Player> PlayersWithApplyEffectOnHit = new List<Player>();
        [Description("First is the effect type, then the intensity, then the duration in seconds.")]
        public Dictionary<EffectType, Dictionary<byte, float>> EffectsToApply { get; set; } = new Dictionary<EffectType, Dictionary<byte, float>>()
        {
            {EffectType.Invigorated, new Dictionary<byte, float> {{1, 5f}}},
        };
        
        protected override void AbilityAdded(Player player)
        {
            Log.Debug($"VVUP Custom Abilities: ApplyEffectOnHit, Adding ApplyEffectOnHit Ability to {player.Nickname}");
            PlayersWithApplyEffectOnHit.Add(player);
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
        }

        protected override void AbilityRemoved(Player player)
        {
            Log.Debug($"VVUP Custom Abilities: ApplyEffectOnHit, Removing ApplyEffectOnHit Ability from {player.Nickname}");
            PlayersWithApplyEffectOnHit.Remove(player);
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
        }

        private void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker == null || ev.Player == null)
                return;
            if (PlayersWithApplyEffectOnHit.Contains(ev.Attacker))
            {
                foreach (var effect in EffectsToApply)
                {
                    foreach (var intensityDuration in effect.Value)
                    {
                        Log.Debug(
                            $"VVUP Custom Abilities: ApplyEffectOnHit, {effect.Key} with intensity {intensityDuration.Key} and duration {intensityDuration.Value} to {ev.Player.Nickname} from {ev.Attacker.Nickname}");
                        ev.Player.EnableEffect(effect.Key, intensityDuration.Key, intensityDuration.Value);
                    }
                }
            }
        }
    }
}