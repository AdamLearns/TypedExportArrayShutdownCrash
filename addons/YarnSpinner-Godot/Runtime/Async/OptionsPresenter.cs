using System.Threading;
using System.Threading.Tasks;
using Godot;

namespace YarnSpinnerGodot;

[GlobalClass]
public partial class OptionsPresenter : Node, IReproDialoguePresenter
{
    [Export] private DialogueRunner? dialogueRunner;

    public Task RunPresenterAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.CompletedTask;
    }
}
