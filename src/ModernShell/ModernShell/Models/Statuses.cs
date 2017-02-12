using System.ComponentModel;

namespace ModernShell.Models
{
    public enum Statuses
    {
        [Description("Online")]
        Online,

        [Description("Away")]
        Away,

        [Description("Do not disturb")]
        DoNotDisturb,

        [Description("Offline")]
        Offline,

        [Description("Invisible")]
        Invisible
    }
}