using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Saveable
{
    public List <TextSaveable> textSaveables = new List<TextSaveable>();

    public Saveable(List<TextSaveable> ts)
    {
        textSaveables = ts;

    }
    public Saveable()
    {
        textSaveables = new List<TextSaveable>();

    }

}
