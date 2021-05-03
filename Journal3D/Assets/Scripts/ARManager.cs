using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARManager : MonoBehaviour
{

    private Saveable saveable = new Saveable();
    public Vector3 userUnityPosition;
    public Camera aRCamera;
    

    [SerializeField]
    private GameObject textPrefab;

    private List<TextSaveable> localText = new List<TextSaveable>();
    private List<TextSaveable> distantText = new List<TextSaveable>();
    private List<float> textDistances = new List<float>();
    private List<TextSaveable> trackedText = new List<TextSaveable>();
    private List<GameObject> spawnedTexts = new List<GameObject>();


    public void AddTextSavable ()
    {
        TextSaveable ts = ServiceLocator.GetTextObject().GetSaveable();
        saveable.textSaveables.Add(ts);
        SaveSystem.Save(saveable);
    }
    public void AddTextSavable(TextSaveable ts)
    {
        saveable.textSaveables.Add(ts);
        SaveSystem.Save(saveable);
    }

    public void DeleteTextSavable (TextSaveable ts)
    {
        saveable.textSaveables.Remove(ts);
        SaveSystem.Save(saveable);
    }

    private void Awake()
    {
        StartCoroutine(startLocationService());
        saveable = SaveSystem.Load();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            foreach (var t in saveable.textSaveables)
            {
                Debug.Log(t.scale);
                float distance = Vector3.Distance(aRCamera.transform.position, t.unityPos);
                if (distance < 10)
                {
                    localText.Add(t);
                    GameObject spawnedObject = Instantiate(textPrefab, transform.position, transform.rotation);
                    spawnedTexts.Add(spawnedObject);
                    spawnedObject.GetComponent<TextObject>().Initialise(t);
                    float latitude = Input.location.lastData.latitude;
                    float longitude = Input.location.lastData.longitude;
                    float altitude = Input.location.lastData.altitude;
                    Vector3 direction = spawnedObject.GetComponent<TextObject>().worldPos.unityPosition - GlobalUtility.CalculateUnityPosition(latitude, longitude, altitude);
                    spawnedObject.transform.position = aRCamera.transform.position + direction;
                }
                else
                {
                    distantText.Add(t);
                    textDistances.Add(distance);
                }

            }
            while (distantText.Count < 4 && textDistances.Count > 0)
            {
                float shortestDistance = Mathf.Infinity;
                TextSaveable closestText = new TextSaveable();
                int closestIndex = 0;

                for (int i = 0; i < textDistances.Count; i++)
                {
                    if (textDistances[i] < shortestDistance)
                    {
                        shortestDistance = textDistances[i];
                        closestText = distantText[i];
                        closestIndex = i;
                    }
                }
                trackedText.Add(closestText);
                distantText.RemoveAt(closestIndex);
                textDistances.RemoveAt(closestIndex);
            }

            for (int i = 0; i < localText.Count; i++)
            {
                if (Vector3.Distance(transform.position, localText[i].unityPos) > 10)
                {
                    localText.RemoveAt(i);
                    GameObject gText = spawnedTexts[i];
                    spawnedTexts.RemoveAt(i);
                    Destroy(gText);
                    break;
                }

            }
        }
        else
        {
            Debug.Log($"location status is {Input.location.status}");
        }

        

    }

    IEnumerator startLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            print("location not enabled");
            yield break;
        }
        if (Input.location.status == LocationServiceStatus.Running)
        {
            Input.location.Stop();
        }
        Input.location.Start(0.5f, 0.5f);
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }
    }
}
