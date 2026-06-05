using System.Threading;
using System.Threading.Tasks;
using Godot;

namespace YarnSpinnerGodot;

public abstract partial class ActionMarkupHandler : Node, IActionMarkupHandler
{
    public abstract Task RunReproAsync(CancellationToken cancellationToken);
}
