using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static TextObject textObject;

    public static void SetTextObject (TextObject to)
    {
        textObject = to;
    }

    public static TextObject GetTextObject ()
    {
        return textObject;
    }

}
