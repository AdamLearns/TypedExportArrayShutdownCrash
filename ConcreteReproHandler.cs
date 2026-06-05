using System.Threading;
using System.Threading.Tasks;
using Godot;
using YarnSpinnerGodot;

[GlobalClass]
public partial class ConcreteReproHandler : ActionMarkupHandler
{
    public override async Task RunReproAsync(CancellationToken cancellationToken)
    {
        var waitsRemaining = GD.RandRange(50, 100);
        // GD.Print("ConcreteReproHandler waitsRemaining: " + waitsRemaining);

        while (waitsRemaining > 0)
        {
            cancellationToken.ThrowIfCancellationRequested();
            double waitTime = GD.RandRange(0.08, 0.12);
            // GD.Print("ConcreteReproHandler waitTime: " + waitTime);
            await ToSignal(GetTree().CreateTimer(waitTime), SceneTreeTimer.SignalName.Timeout);
            waitsRemaining--;
        }
    }
}
