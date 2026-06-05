using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Godot;

namespace YarnSpinnerGodot;

[GlobalClass]
public partial class LinePresenter : Node, IReproDialoguePresenter
{
    private const int CallsPerHandler = 4;

    private readonly List<IActionMarkupHandler> actionMarkupHandlers = [];
    private Task? presenterKeepAliveTask;

    [Export]
    private Godot.Collections.Array<ActionMarkupHandler>? eventHandlers = [];

    [Export]
    private DialogueRunner? dialogueRunner;

    [Export]
    private MarkupPalette? palette;

    public override void _Ready()
    {
        ShutdownArrayRetainer.Retain(eventHandlers);

        if (eventHandlers == null)
        {
            return;
        }

        foreach (var eventHandler in eventHandlers)
        {
            if (eventHandler == null)
            {
                continue;
            }

            actionMarkupHandlers.Add(eventHandler);
        }

        if (palette != null)
        {
            _ = palette.BasicMarkerList.Count;
            _ = palette.CustomMarkerList.Count;
        }
    }

    public async Task RunPresenterAsync(CancellationToken cancellationToken)
    {
        presenterKeepAliveTask ??= KeepPresenterAliveAsync();

        foreach (var eventHandler in actionMarkupHandlers)
        {
            for (var i = 0; i < CallsPerHandler; i++)
            {
                await eventHandler.RunReproAsync(cancellationToken);
            }
        }
    }

    private async Task KeepPresenterAliveAsync()
    {
        await Task.Delay(TimeSpan.FromMinutes(10));
    }
}
