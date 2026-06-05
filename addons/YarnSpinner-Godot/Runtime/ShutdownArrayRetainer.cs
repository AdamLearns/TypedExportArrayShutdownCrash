using System.Collections.Generic;

namespace YarnSpinnerGodot;

public static class ShutdownArrayRetainer
{
    private static readonly List<object> RetainedArrays = [];

    public static void Retain(object? array)
    {
        if (array != null)
        {
            RetainedArrays.Add(array);
        }
    }
}
