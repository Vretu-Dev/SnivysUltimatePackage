using System.ComponentModel;
using UnityEngine;

namespace SnivysUltimatePackageOneConfig.Configs.ServerEventsConfigs
{
    public class GravityConfig
    {
        public string StartEventCassieMessage { get; set; } = "Low Gravity";
        public string StartEventCassieText { get; set; } = "Low Gravity Event";

        [Description("What should be the gravity for everyone")]
        public Vector3 GravityChanges { get; set; } = new Vector3()
        {
            x = 1,
            y = 0.5f,
            z = 1,
        };
    }
}