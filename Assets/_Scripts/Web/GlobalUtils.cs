using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalUtils {

    public static string Precise(this Vector3 vec) => ((Vector2) vec).Precise();

    public static string Precise(this Vector2 vec) {
        return StrFloat(vec.x) + StrFloat(vec.y);
    }

    private static string StrFloat(float val) => (Mathf.Round(val * Mathf.Pow(10, 5)) / Mathf.Pow(10, 5)).ToString();
}
