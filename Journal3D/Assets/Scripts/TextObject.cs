using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextObject : MonoBehaviour
{
    private TextSaveable dataText;
    private string text;
    private Color textColour;
    private Color textBackground;
    private Vector2 scale;
    private bool backgroundEnabled;
    private TextAnchor alignment;
    private Quaternion rotation;

    public ObjectWorldPos worldPos;


    public void Initialise()
    {
        text = GetComponentInChildren<Text>().text;
        textColour = GetComponentInChildren<Text>().color;
        textBackground = GetComponentInChildren<Image>().color;
        scale = transform.localScale;

        if (GetComponentInChildren<Image>() != null)
        {
            backgroundEnabled = true;
        }
        else
        {
            backgroundEnabled = false;
        }

        alignment = GetComponentInChildren<Text>().alignment;
        Vector3 realPosition = (transform.position - Camera.main.transform.position) + (GlobalUtility.CalculateUnityPosition(Input.location.lastData.latitude, Input.location.lastData.longitude, Input.location.lastData.altitude));
        worldPos.unityPosition = realPosition;
        rotation = transform.rotation;
        dataText = new TextSaveable(text, textColour, textBackground, scale, backgroundEnabled, alignment, realPosition, rotation);
    }

    public void Initialise(TextSaveable textSaveable)
    {
        GetComponentInChildren<Text>().text = textSaveable.text;
        GetComponentInChildren<Text>().color = textSaveable.textColour;
        GetComponentInChildren<Image>().color = textSaveable.textBackground;
        transform.localScale = textSaveable.scale;
        GetComponentInChildren<Image>().enabled = textSaveable.backgroundEnabled;
        GetComponentInChildren<Text>().alignment = textSaveable.alignment;
        transform.position = textSaveable.unityPos;
        worldPos.unityPosition = textSaveable.unityPos;
        dataText = textSaveable;

    }

    public TextSaveable GetSaveable ()
    {
        return dataText;
    }

    // Start is called before the first frame update
    void Start()
    {
        ServiceLocator.SetTextObject(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
