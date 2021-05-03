using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class TextSaveable  
{
    public string text;
    public Color textColour;
    public Color textBackground;
    public Vector2 scale;
    public bool backgroundEnabled;
    public TextAnchor alignment;
    public Vector3 unityPos;
    public Quaternion rotation;
    

    public TextSaveable(string t, Color tc, Color tb, Vector2 s, bool be, TextAnchor a, Vector3 pos, Quaternion r)
    {
        text = t;
        textColour = tc;
        textBackground = tb;
        scale = s;
        backgroundEnabled = be;
        alignment = a;
        unityPos = pos;
        rotation = r;
    }
    public TextSaveable ()
    { }

}
