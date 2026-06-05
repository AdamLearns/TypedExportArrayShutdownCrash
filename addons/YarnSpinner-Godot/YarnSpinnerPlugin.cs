using Godot;

namespace YarnSpinnerGodot;

[Tool]
public partial class YarnSpinnerPlugin : EditorPlugin
{
    public override void _EnterTree()
    {
        RegisterCustomType<DialogueRunner>("DialogueRunner", "Node", "res://addons/YarnSpinner-Godot/Runtime/DialogueRunner.cs");
        RegisterCustomType<LinePresenter>("LinePresenter", "Node", "res://addons/YarnSpinner-Godot/Runtime/Async/LinePresenter.cs");
        RegisterCustomType<OptionsPresenter>("OptionsPresenter", "Node", "res://addons/YarnSpinner-Godot/Runtime/Async/OptionsPresenter.cs");
        RegisterCustomType<BasicMarker>("BasicMarker", "Resource", "res://addons/YarnSpinner-Godot/Runtime/BasicMarker.cs");
        RegisterCustomType<CustomMarker>("CustomMarker", "Resource", "res://addons/YarnSpinner-Godot/Runtime/CustomMarker.cs");
        RegisterCustomType<MarkupPalette>("MarkupPalette", "Resource", "res://addons/YarnSpinner-Godot/Runtime/Views/MarkupPalette.cs");
    }

    public override void _ExitTree()
    {
        RemoveCustomType(nameof(DialogueRunner));
        RemoveCustomType(nameof(LinePresenter));
        RemoveCustomType(nameof(OptionsPresenter));
        RemoveCustomType(nameof(BasicMarker));
        RemoveCustomType(nameof(CustomMarker));
        RemoveCustomType(nameof(MarkupPalette));
    }

    private void RegisterCustomType<T>(string typeName, string baseType, string scriptPath)
        where T : GodotObject
    {
        var script = ResourceLoader.Load<CSharpScript>(scriptPath);
        AddCustomType(typeName, baseType, script, null);
    }
}
