using System.Threading;
using System.Threading.Tasks;
using Godot;

namespace YarnSpinnerGodot;

public partial class LinePresenterButtonHandler : ActionMarkupHandler
{
    [Export]
    private Button? continueButton;

    [Export]
    private DialogueRunner? dialogueRunner;

    private bool displayComplete;

    public override void _Ready()
    {
        displayComplete = false;

        if (continueButton != null)
        {
            continueButton.Disabled = true;
            continueButton.Pressed += OnClick;
        }
    }

    public override async Task RunReproAsync(CancellationToken cancellationToken)
    {
        displayComplete = false;

        if (continueButton != null)
        {
            continueButton.Disabled = false;
        }

        var waitsRemaining = (int)GD.RandRange(50, 100);
        // GD.Print("LinePresenterButtonHandler waitsRemaining: " + waitsRemaining);

        while (waitsRemaining > 0)
        {
            cancellationToken.ThrowIfCancellationRequested();
            double waitTime = GD.RandRange(0.08, 0.12);
            // GD.Print("LinePresenterButtonHandler waitTime: " + waitTime);
            await ToSignal(GetTree().CreateTimer(waitTime), SceneTreeTimer.SignalName.Timeout);
            waitsRemaining--;
        }

        displayComplete = true;

        if (continueButton != null)
        {
            continueButton.Disabled = true;
        }
    }

    public override void _ExitTree()
    {
        if (continueButton != null)
        {
            continueButton.Pressed -= OnClick;
        }
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
