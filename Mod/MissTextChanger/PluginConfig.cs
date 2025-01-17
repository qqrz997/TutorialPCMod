using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace MissTextChanger;

internal class PluginConfig
{
    public virtual bool Enabled { get; set; } = true;
    public virtual string MissText { get; set; } = "MISS";
}
