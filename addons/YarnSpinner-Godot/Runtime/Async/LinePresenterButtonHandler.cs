using System.Threading;
using System.Threading.Tasks;
using Godot;

namespace YarnSpinnerGodot;

public partial class LinePresenterButtonHandler : ActionMarkupHandler
{
    [Export]
    private DialogueRunner? dialogueRunner;

    private bool displayComplete;

    public override void _Ready()
    {
        displayComplete = false;
    }

    public override async Task RunReproAsync(CancellationToken cancellationToken)
    {
        displayComplete = false;

        var waitsRemaining = GD.RandRange(50, 100);

        while (waitsRemaining > 0)
        {
            cancellationToken.ThrowIfCancellationRequested();
            double waitTime = GD.RandRange(0.08, 0.12);
            await ToSignal(GetTree().CreateTimer(waitTime), SceneTreeTimer.SignalName.Timeout);
            waitsRemaining--;
        }

        displayComplete = true;
    }

    private void OnClick()
    {
        if (dialogueRunner == null)
        {
            return;
        }

        if (displayComplete)
        {
            dialogueRunner.RequestNextLine();
        }
        else
        {
            dialogueRunner.RequestHurryUpLine();
        }
    }
}
