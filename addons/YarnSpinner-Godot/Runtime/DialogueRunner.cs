using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Godot;

namespace YarnSpinnerGodot;

[GlobalClass]
public partial class DialogueRunner : Node
{
    private readonly List<IReproDialoguePresenter> dialoguePresenterList = [];
    private CancellationTokenSource? dialogueCancellationTokenSource;
    private Task? runningDialogueTask;

    [Export]
    public Godot.Collections.Array<Node?>? dialoguePresenters;

    public override void _Ready()
    {
        ShutdownArrayRetainer.Retain(dialoguePresenters);

        if (dialoguePresenters == null)
        {
            return;
        }

        foreach (var dialoguePresenter in dialoguePresenters)
        {
            if (dialoguePresenter is IReproDialoguePresenter presenter)
            {
                dialoguePresenterList.Add(presenter);
            }
        }

        runningDialogueTask = RunDialogueAfterNextFrameAsync();
    }

    public async Task Stop()
    {
        dialogueCancellationTokenSource?.Cancel();

        if (runningDialogueTask != null)
        {
            try
            {
                await runningDialogueTask;
            }
            catch (OperationCanceledException)
            {
            }
        }

        dialogueCancellationTokenSource?.Dispose();
        dialogueCancellationTokenSource = null;
        runningDialogueTask = null;
    }

    public void RequestNextLine()
    {
    }

    public void RequestHurryUpLine()
    {
    }

    private async Task RunDialogueAsync()
    {
        dialogueCancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = dialogueCancellationTokenSource.Token;

        foreach (var dialoguePresenter in dialoguePresenterList)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await dialoguePresenter.RunPresenterAsync(cancellationToken);
        }
    }

    private async Task RunDialogueAfterNextFrameAsync()
    {
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        await RunDialogueAsync();
    }
}
