using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static bool stopDrawing = true;
    GameObject[] array;
    List<GameObject> inActive = new List<GameObject>();
    GameObject[] allObjects;


    public Canvas textCanvas;
    public Canvas drawCanvas;
    public Text inputText;
    private string writtenText = "";
    public Button textButton;
    public Button drawButton;
    public ObjectPlacement objectPlacement;
    public Image textBackground;
    public Color textColour;
    public Color backgroundColour;
    public bool backgroundEnabled = true;
    public TextAnchor textAlignment = TextAnchor.UpperLeft;
    public Canvas createModeCanvas;
    public Canvas viewModeCanvas;
    public Canvas clearCanvas;
    public Button textBackgroundButton;
    public Button textAlignmentButton;
    private int backgroundIndex = 0;
    private int alignmentIndex = 0;
    public Sprite gpsEnabled;
    public Sprite gpsDisabled;
    public Image gpsIndicator;
    public Vector3 drawButtonOrigion;

    public ARLineObject lineDrawer;

   


    public List<Sprite> textBackgroundIcons = new List<Sprite>();
    public List<Sprite> textAlignmentIcons = new List<Sprite>();


    // Start is called before the first frame update
    void Start()
    {
        drawButtonOrigion = drawButton.transform.position;
    }

    public void UpdateLocationStatus ()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            gpsIndicator.sprite = gpsEnabled;
        }
        else
        {
            gpsIndicator.sprite = gpsDisabled;
        }
    }
    public string GetStringText()
    {
        return writtenText;
    }
    public void ToggleTextCanvas()
    {
        textCanvas.gameObject.SetActive(!textCanvas.gameObject.activeSelf);
        drawButton.gameObject.SetActive(!drawButton.gameObject.activeSelf);
    }
    public void ToggleDrawCanvas()
    {
        drawCanvas.gameObject.SetActive(!drawCanvas.gameObject.activeSelf);
        textButton.gameObject.SetActive(!textButton.gameObject.activeSelf);
        if (drawCanvas.gameObject.activeSelf)
        {
            drawButton.transform.position = textButton.transform.position;
        }
        else
        {
            drawButton.transform.position = drawButtonOrigion;
        }
    }
    public void ConfirmText()
    {
        writtenText = inputText.text;
        textColour = inputText.color;
        backgroundColour = textBackground.color;
        inputText.color = Color.black;
        textBackground.color = Color.white;
        inputText.GetComponentInParent<InputField>().text = "";
        objectPlacement.enabled = true;
        //objectPlacement.PlaceObject(); 
        ToggleTextCanvas();
    }

    public void ViewMode ()
    {
        viewModeCanvas.gameObject.SetActive(true);
        createModeCanvas.gameObject.SetActive(false);
    }
    public void CreateMode()
    {
        viewModeCanvas.gameObject.SetActive(false);
        createModeCanvas.gameObject.SetActive(true);
    }

    public void ClearButton()
    {
        SaveSystem.ClearData();
        ToggleClearCanvas();
        allObjects = GameObject.FindGameObjectsWithTag("SpawnedObject");
        for (int i =0; i < allObjects.Length; i++)
        {
            Destroy(allObjects[i]);
        }
    }
    public void ToggleClearCanvas()
    {
        clearCanvas.gameObject.SetActive(!clearCanvas.gameObject.activeSelf);
    }
    private void FixedUpdate()
    {
        UpdateLocationStatus();
    }
    // Update is called once per frame
    void Update()
    {
        array = GameObject.FindGameObjectsWithTag("Plane");

        if (stopDrawing == true)
        {
            foreach (GameObject plane in array)
            {
                plane.SetActive(false);
                inActive.Add(plane);
            }


        }
        else
        {
            foreach (GameObject plane in inActive)
            {
                plane.SetActive(true);
            }
        }
    }

    public void Stop()
    {
        if(stopDrawing == true)
        {
            stopDrawing = false;
        }
        else
        {
            stopDrawing = true;
        }
    }
    public void ChangeTextBackground()
    {
        backgroundIndex ++;
        if (backgroundIndex >= textBackgroundIcons.Count)
        {
            backgroundIndex = 0;
        }
        textBackgroundButton.GetComponent<Image>().sprite = textBackgroundIcons[backgroundIndex];
        backgroundEnabled = !backgroundEnabled;

    }
    public void ChangeTextAlignment()
    {
        alignmentIndex++;
        if (alignmentIndex >= textAlignmentIcons.Count)
        {
            alignmentIndex = 0;
        }
        textAlignmentButton.GetComponent<Image>().sprite = textAlignmentIcons[alignmentIndex];
        if (textAlignment == TextAnchor.UpperLeft)
        {
            textAlignment = TextAnchor.UpperCenter;
        }
        else if (textAlignment == TextAnchor.UpperCenter)
        {
            textAlignment = TextAnchor.UpperRight;
        }
        else
        {
            textAlignment = TextAnchor.UpperLeft;
        }
        inputText.alignment = textAlignment;
    }

    public void SetTextBlack()
    {
        inputText.color = Color.black;
        textBackground.color = Color.white;
    }
    public void SetTextWhite()
    {
        inputText.color = Color.white;
        textBackground.color = Color.black;
    }
    public void SetTextRed()
    {
        inputText.color = Color.red;
        textBackground.color = Color.white;
    }
    public void SetTextBlue()
    {
        inputText.color = Color.blue;
        textBackground.color = Color.white;
    }
    public void SetTextYellow()
    {
        inputText.color = Color.yellow;
        textBackground.color = Color.black;
    }
    public void ToggleBackground ()
    {
        backgroundEnabled = !backgroundEnabled;
    }

    public void SetLineWidth(int i)
    {
        if (i == 0)
        {
            lineDrawer.SetLineWidth(0.001f);
        }
        else if (i == 1)
        {
            lineDrawer.SetLineWidth(0.005f);
        }
        else
        {
            lineDrawer.SetLineWidth(0.025f);
        }
    }
    public void SetLineBlack()
    {
        lineDrawer.SetLineColor(Color.black);
    }
    public void SetLineWhite()
    {
        lineDrawer.SetLineColor(Color.white);
    }
    public void SetLineRed()
    {
        lineDrawer.SetLineColor(Color.red);
    }
    public void SetLineYellow()
    {
        lineDrawer.SetLineColor(Color.yellow);
    }

}
