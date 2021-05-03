using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

[RequireComponent(typeof(ARRaycastManager))]

public class ObjectPlacement : MonoBehaviour
{
    [SerializeField]
    private GameObject textBox;

    public UIManager uIManager;
    public GameObject debugObject;
    public ARManager aRManager;
    

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private ARRaycastManager arRaycastManager;

    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;

        return false;
    }

    public void PlaceObject(Pose hitPose)
    {
        Debug.Log("Starting to place object");
        Vector3 position = hitPose.position;
        Vector3 rotation = hitPose.rotation.eulerAngles;
        rotation.x -= 90;
        GameObject placeText = Instantiate(textBox, position, Quaternion.Euler(rotation));
        Image textBackgroundImage = placeText.GetComponentInChildren<Image>();
        Text text = placeText.GetComponentInChildren<Text>();
        text.text = uIManager.GetStringText();
        text.color = uIManager.textColour;
        text.alignment = uIManager.textAlignment;
        textBackgroundImage.color = uIManager.backgroundColour;
        placeText.GetComponent<Canvas>().worldCamera = Camera.main;
        textBackgroundImage.gameObject.SetActive(uIManager.backgroundEnabled);
        TextObject textObject = placeText.GetComponent<TextObject>();
        aRManager.AddTextSavable(textObject.GetSaveable());
        enabled = false;
        Debug.Log("Finishing to place object");
    }

    public void PlaceObject ()
    {
        Vector3 position = GlobalUtility.CalculateUnityPosition(Input.location.lastData.latitude, Input.location.lastData.longitude, Input.location.lastData.altitude) - aRManager.transform.position;
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.x -= 90;
        GameObject placeText = Instantiate(textBox, position, Quaternion.Euler(rotation));
        Image textBackgroundImage = placeText.GetComponentInChildren<Image>();
        Text text = placeText.GetComponentInChildren<Text>();
        text.text = uIManager.GetStringText();
        text.color = uIManager.textColour;
        text.alignment = uIManager.textAlignment;
        textBackgroundImage.color = uIManager.backgroundColour;
        placeText.GetComponent<Canvas>().worldCamera = Camera.main;
        textBackgroundImage.gameObject.SetActive(uIManager.backgroundEnabled);
        TextObject textObject = placeText.GetComponent<TextObject>();
        aRManager.AddTextSavable(textObject.GetSaveable());
        enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        Debug.Log("Touch screen detected");
        if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            Debug.Log("Raycast hit");
            PlaceObject(hitPose);
            
        }

        if (!debugObject.activeSelf)
        {
            debugObject.SetActive(true);
        }
    }


 
}
