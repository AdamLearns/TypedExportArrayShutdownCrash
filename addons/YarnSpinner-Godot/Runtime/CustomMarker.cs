using Godot;

namespace YarnSpinnerGodot;

[GlobalClass]
public partial class CustomMarker : Resource
{
    [Export] public string? Marker;
    [Export] public string? Start;
    [Export] public string? End;
    [Export] public int MarkerOffset;
    [Export] public int TotalVisibleCharacterCount;
}
