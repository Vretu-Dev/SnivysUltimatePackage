using System.ComponentModel;
using Exiled.API.Interfaces;

namespace VVUP.Base
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        [Description("There's basically no debug statements for this Module, as its more of the base for everything else.")]
        public bool Debug { get; set; } = false;
    }
}