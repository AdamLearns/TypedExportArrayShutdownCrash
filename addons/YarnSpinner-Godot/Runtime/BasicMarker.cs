using Godot;

namespace YarnSpinnerGodot;

[GlobalClass]
public partial class BasicMarker : Resource
{
    [Export] public string? Marker;
    [Export] public bool CustomColor;
    [Export] public Color Color;
    [Export] public bool Boldened;
    [Export] public bool Italicised;
    [Export] public bool Underlined;
    [Export] public bool Strikedthrough;
}
