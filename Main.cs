using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using YarnSpinnerGodot;

public partial class Main : Control
{
    private const int SceneCount = 500;
    private static readonly StringName AerialDashLeft = "aerial_dash_left";
    private static readonly StringName AerialDashRight = "aerial_dash_right";

    private readonly List<Node> _reproScenes = [];
    private readonly List<DialogueRunner> _dialogueRunners = [];
    private Node2D _subGameNode = null!;

    public override void _Ready()
    {
        _subGameNode = GetNode<Node2D>("%SubGameNode");

        GD.Print($"Loading {SceneCount} repro scene instance(s)");
        var packedScene = ResourceLoader.Load<PackedScene>("res://ExportArrayScene.tscn");

        for (var i = 0; i < SceneCount; i++)
        {
            var reproScene = packedScene.Instantiate();
            _subGameNode.AddChild(reproScene);
            _reproScenes.Add(reproScene);
            _dialogueRunners.Add(reproScene.GetNode<DialogueRunner>("DialogueRunner"));
        }

        _ = QuitAfterDelay();
    }

    private async Task QuitAfterDelay()
    {
        double quitDelay = GD.RandRange(1.7, 1.8);
        GD.Print("quitDelay: " + quitDelay);
        await ToSignal(GetTree().CreateTimer(quitDelay), SceneTreeTimer.SignalName.Timeout);

        GD.Print("Stopping dialogue runners");
        foreach (var dialogueRunner in _dialogueRunners)
        {
            _ = dialogueRunner.Stop();
        }

        _dialogueRunners.Clear();

        GD.Print("Keeping repro scenes alive through Quit");
        _reproScenes.Clear();

        GD.Print("Quitting with repro scenes still alive");
        GetTree().Quit();
    }
}
