using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class LineSaveable
{
    public Color lineColour;
    public int lineWidth;
    //public float latitude;
    //public float longitude;
    //public float altitude;
    


public LineSaveable (Color lc, int lw)
    {
        lineColour = lc;
        lineWidth = lw;

    }
}
