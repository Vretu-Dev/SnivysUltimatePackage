using System.ComponentModel;
using Exiled.API.Interfaces;

namespace VVUP.CreditTags
{
    public class Config : IConfig
    {
        [Description("This is really just to be a thanks for the people who have contributed to the plugin, if thats by testing or contributing code.")]
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        [Description("Overrides badges for the player who has a tag, if they have another one")]
        public bool BadgeOverride { get; set; } = false;
        [Description("Overrides Custom Info for the player who has a tag, if they have another one (best to keep this off, since stuff like Custom Roles uses it")]
        public bool CustomInfoOverride { get; set; } = false;
        [Description("Should this ignore a player's DNT flag? Turing this on will always check a player, even if they have DNT on")]
        public bool IgnoreDntFlag { get; set; } = false;
    }
}