using System.ComponentModel;
using UnityEngine;

namespace SnivysUltimatePackage.Configs.ServerEventsConfigs
{
    public class GravityConfig
    {
        public string StartEventCassieMessage { get; set; } = "Low Gravity";
        public string StartEventCassieText { get; set; } = "Low Gravity Event";

        [Description("What should be the gravity for everyone")]
        public Vector3 GravityChanges { get; set; } = new Vector3()
        {
            x = 0,
            y = -12.60f,
            z = 0
        };
    }
}