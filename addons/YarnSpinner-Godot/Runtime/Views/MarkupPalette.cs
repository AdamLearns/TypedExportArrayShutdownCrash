using System.Collections.Generic;
using Godot;

namespace YarnSpinnerGodot;

[GlobalClass]
[Tool]
public partial class MarkupPalette : Resource
{
    private readonly List<BasicMarker> basicMarkerList = [];
    private readonly List<CustomMarker> customMarkerList = [];
    private bool markersImported;

    [Export] public Godot.Collections.Array<BasicMarker> BasicMarkers = null!;
    [Export] public Godot.Collections.Array<CustomMarker> CustomMarkers = null!;

    public IReadOnlyList<BasicMarker> BasicMarkerList
    {
        get
        {
            ImportMarkers();
            return basicMarkerList;
        }
    }

    public IReadOnlyList<CustomMarker> CustomMarkerList
    {
        get
        {
            ImportMarkers();
            return customMarkerList;
        }
    }

    private void ImportMarkers()
    {
        if (markersImported)
        {
            return;
        }

        markersImported = true;

        ShutdownArrayRetainer.Retain(BasicMarkers);
        ShutdownArrayRetainer.Retain(CustomMarkers);

        foreach (var marker in BasicMarkers)
        {
            if (marker != null)
            {
                basicMarkerList.Add(marker);
            }
        }

        foreach (var marker in CustomMarkers)
        {
            if (marker != null)
            {
                customMarkerList.Add(marker);
            }
        }
    }
}
