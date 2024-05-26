using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHelper : MonoBehaviour
{
    public static Color HexToColor(string hex)
    {
        hex = hex.Replace("#", "");
        int r = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        int g = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        int b = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color(r / 255f, g / 255f, b / 255f);
    }
}
