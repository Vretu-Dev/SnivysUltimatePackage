using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;

namespace SnivysUltimatePackage.Custom.Abilities.Passive
{
    [CustomAbility]
    public class Disguised : PassiveAbility
    {
        public override string Name { get; set; } = "Disguised";

        public override string Description { get; set; } =
            "Handles everything with being disguised";

        [Description(
            "The text displayed to any attacker who is trying to hurt someone who is disguised, but is on their team")]
        public string DisguisedFriendlyFireText { get; set; } = "This player is on your side";
        
        [Description("The true team of the disguised player. NTF or MTF = MTF/Scientists, CI = Chaos Insurgency/Class-D, SCP = SCPs")]
        public TrueTeamEnum DisguisedTrueTeam { get; set; } = TrueTeamEnum.Ntf;

        [Description("Should the disguised notifcation be a hint? (True = Hint, False = Broadcast)")]
        public bool DisguisedHintDisplay { get; set; } = true;
        
        [Description("How long should the text displayed to the player should last")]
        public float DisguisedTextDisplayTime { get; set; } = 5f;
        
        public Dictionary<Player, TrueTeamEnum> PlayersWithDisguisedEffect = new Dictionary<Player, TrueTeamEnum>();

        public enum TrueTeamEnum
        {
            Ntf,
            Mtf,
            Ci,
            Scp
        }
        protected override void AbilityAdded(Player player)
        {
            Log.Debug($"VVUP Custom Abilities: Disguised, Adding Disguised Ability to {player.Nickname}");
            PlayersWithDisguisedEffect.Add(player, DisguisedTrueTeam);
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
            //Exiled.Events.Handlers.Player.Shot += OnShot;
        }
        protected override void AbilityRemoved(Player player)
        {
            Log.Debug($"VVUP Custom Abilities: Disguised, Removing Disguised Ability from {player.Nickname}");
            PlayersWithDisguisedEffect.Remove(player);
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
            //Exiled.Events.Handlers.Player.Shot -= OnShot;
        }

        private void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker == null)
                return;
            if (!PlayersWithDisguisedEffect.ContainsKey(ev.Player)) 
                return;
            /*if (ev.Player.IsNTF && (ev.Attacker.IsCHI || ev.Attacker.Role.Type == RoleTypeId.ClassD))
            {
                Log.Debug("VVUP Custom Abilities: Preventing accidental friendly fire with disguised");
                if (DisguisedHintDisplay)
                    ev.Attacker.ShowHint(DisguisedCi, DisguisedTextDisplayTime);
                else
                    ev.Attacker.Broadcast(new Exiled.API.Features.Broadcast(DisguisedCi, (ushort)DisguisedTextDisplayTime));
                ev.IsAllowed = false;
            }
            else if (ev.Player.IsCHI && (ev.Attacker.IsNTF || ev.Attacker.Role.Type == RoleTypeId.Scientist))
            {
                Log.Debug("VVUP Custom Abilities: Preventing accidental friendly fire with disguised");
                if (DisguisedHintDisplay)
                    ev.Attacker.ShowHint(DisguisedMtf, DisguisedTextDisplayTime);
                else
                    ev.Attacker.Broadcast(new Exiled.API.Features.Broadcast(DisguisedMtf, (ushort)DisguisedTextDisplayTime));
                ev.IsAllowed = false;
            }*/
            
            if ((PlayersWithDisguisedEffect[ev.Player] == TrueTeamEnum.Ntf || PlayersWithDisguisedEffect[ev.Player] == TrueTeamEnum.Mtf) && ev.Attacker.Role.Side == Side.Mtf || 
                (PlayersWithDisguisedEffect[ev.Player] == TrueTeamEnum.Ci && ev.Attacker.Role.Side == Side.ChaosInsurgency) ||
                (PlayersWithDisguisedEffect[ev.Player] == TrueTeamEnum.Scp && ev.Attacker.Role.Side == Side.Scp))
            {
                Log.Debug($"VVUP Custom Abilities, Disguised: Preventing accidental friendly fire with disguised from {ev.Attacker.Nickname} (Attacker) and {ev.Player.Nickname} (Target)");
                if (DisguisedHintDisplay)
                    ev.Attacker.ShowHint(DisguisedFriendlyFireText, DisguisedTextDisplayTime);
                else
                    ev.Attacker.Broadcast((ushort)DisguisedTextDisplayTime, DisguisedFriendlyFireText, shouldClearPrevious: true);
                ev.IsAllowed = false;
            }
        }

        /*private void OnShot(ShotEventArgs ev)
        {
            if (PlayersWithDisguisedEffect.ContainsKey(ev.Player))
            {
                Log.Debug("VVUP Custom Abilities: Preventing accidental friendly fire with disguised");
                if (ev.Player.IsNTF)
                {
                    if (ev.Target != null && Check(ev.Player) && (ev.Target.Role == RoleTypeId.ClassD || ev.Target.IsCHI))
                        ev.CanHurt = false;
                }
                else if (ev.Player.IsCHI)
                {
                    if (ev.Target != null && Check(ev.Player) && (ev.Target.Role == RoleTypeId.Scientist || ev.Target.IsNTF))
                        ev.CanHurt = false;
                }
            }
        }*/
    }
}