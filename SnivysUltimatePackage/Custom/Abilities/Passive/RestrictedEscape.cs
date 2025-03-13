using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;

namespace SnivysUltimatePackage.Custom.Abilities.Passive
{
    [CustomAbility]
    public class RestrictedEscape : PassiveAbility
    {
        public override string Name { get; set; } = "Restricted Escape";

        public override string Description { get; set; } =
            "Prevents players from escaping regularly (can still escape while detained)";
        public List<Player> PlayersWithRestrictedEscapeEffect = new List<Player>();

        public string EscapeText { get; set; } = "You're unable to escape unless you're detained";
        public float EscapeTextTime { get; set; } = 5;
        public bool UseHints { get; set; } = true;
        public bool AllowedUncuffedEscape { get; set; } = false;
        public bool AllowedCuffedEscape { get; set; } = false;
        
        protected override void AbilityAdded(Player player)
        {
            PlayersWithRestrictedEscapeEffect.Add(player);
            Exiled.Events.Handlers.Player.Escaping += OnEscaping;
        }
        protected override void AbilityRemoved(Player player)
        {
            PlayersWithRestrictedEscapeEffect.Remove(player);
            Exiled.Events.Handlers.Player.Escaping -= OnEscaping;
        }

        private void OnEscaping(EscapingEventArgs ev)
        {
            if (PlayersWithRestrictedEscapeEffect.Contains(ev.Player) && !ev.Player.IsCuffed)
            {
                ev.IsAllowed = false;
                Log.Debug($"VVUP Custom Abilities: Restricting Escape of {ev.Player.Nickname}");
                if (UseHints)
                    ev.Player.ShowHint(EscapeText, EscapeTextTime);
                else
                    ev.Player.Broadcast(new Exiled.API.Features.Broadcast(EscapeText, (ushort)EscapeTextTime));
            }
        }
    }
}