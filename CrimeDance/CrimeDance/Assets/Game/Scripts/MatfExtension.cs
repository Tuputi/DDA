using UnityEngine;
using System.Collections;

public static class MatfExtension
{
    public static bool Between(this double f, double start, double end)
    {
        return (f >= start && f < end);
    }
}

