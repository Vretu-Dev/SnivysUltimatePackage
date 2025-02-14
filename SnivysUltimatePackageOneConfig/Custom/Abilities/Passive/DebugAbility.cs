using System.Collections;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.CustomRoles.API.Features;
using MEC;
using UnityEngine;

namespace SnivysUltimatePackageOneConfig.Custom.Abilities.Passive
{
    [CustomAbility]
    public class DebugAbility : PassiveAbility
    {
        private static CoroutineHandle _debugCoroutine;
        
        public override string Name { get; set; } = "Ability Remover";

        public override string Description { get; set; } =
            "Removes any random abilities a user might get for whatever reason";
        
        protected override void AbilityAdded(Player player)
        {
            _debugCoroutine = Timing.RunCoroutine(DebugTracking(player));
        }

        protected override void AbilityRemoved(Player player)
        {
            Timing.KillCoroutines(_debugCoroutine);
        }

        private static IEnumerator<float> DebugTracking(Player player)
        {
            for (;;)
            {
                Log.Warn($"VVUP: {player.Position}");
                yield return Timing.WaitForSeconds(0.5f);
            }
        }
    }
}