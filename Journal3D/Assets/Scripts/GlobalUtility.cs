using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalUtility
{
    public static float sealevel = 6371146;


    public static Vector3 CalculateUnityPosition(float latitude, float longitude, float altitude)
    {
        Vector3 unityPos = Quaternion.AngleAxis(longitude, -Vector3.up) * Quaternion.AngleAxis(latitude, -Vector3.right) * new Vector3(0, 0, sealevel);
        return unityPos;
    }

    public static Vector3 CalculateWorldPosition(float currentAltitude, float userUnityY, Vector3 objectUnityPosition)
    {
        float altitude = currentAltitude + (objectUnityPosition.y - userUnityY);
        float r = altitude + sealevel;
        float latitude = Mathf.Acos(objectUnityPosition.z / r);
        float longitude = Mathf.Acos(objectUnityPosition.x / (r * Mathf.Sin(latitude)));
        return new Vector3(latitude, longitude, altitude);
    }

}
