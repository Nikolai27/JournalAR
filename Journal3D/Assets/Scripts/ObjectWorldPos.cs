using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWorldPos
{
    public float latitude;
    public float longitude;
    public float altitude;
    public Vector3 unityPosition;
    public float sealevel = 6371146;

    public void CalculateUnityPosition()
    {
        float r = altitude + sealevel;
        float x = r * Mathf.Sin(latitude) * Mathf.Cos(longitude);
        float y = r * Mathf.Sin(latitude) * Mathf.Sin(longitude);
        float z = r * Mathf.Cos(latitude);
        unityPosition = new Vector3(x, y, z);
    }

    public void CalculateWorldPosition(float currentAltitude, float userUnityY)
    {
        altitude = currentAltitude + (unityPosition.y - userUnityY);
        float r = altitude + sealevel;
        latitude = Mathf.Acos(unityPosition.z / r);
        longitude = Mathf.Acos(unityPosition.x / (r * Mathf.Sin(latitude)));
        
    }
}
