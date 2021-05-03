using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARLineObject : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public bool arMode = false;

    new public Camera camera;

    public GameObject linePrefab = null;

    private float myLineWidth = 0.005f;
    private int mySortingOrder = 1;
    private Color lineColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && UIManager.stopDrawing == false)
        {
            // skip the touches fall on the UI
            //if (Input.mousePosition.y < drawingBound)
            //{
            //   return;
           //}

            SpawnNewLine();
        }
    }
    bool GetArRaycastLogic(out Vector3 hitPosition)
    {

            // 1
            var hits = new List<ARRaycastHit>();

            // 2
            bool hasHit = raycastManager.Raycast(Input.mousePosition, hits, TrackableType.PlaneWithinInfinity);

            // 3
            if (hasHit == false || hits.Count == 0)
            {
                hitPosition = Vector3.zero;
                return false;
            }
            else
            {
                hitPosition = hits[0].pose.position;
                return true;
            }
    }

    public void SpawnNewLine()
    {
        if (linePrefab == null)
        {
            return;
        }

        var newLine = Instantiate(linePrefab).GetComponent<DrawLine>();
        SetupRaycastLogic(newLine);

        newLine.ChangeLineWidth(myLineWidth);
        newLine.ChangeLineColor(lineColor);

        newLine.SetLineOrder(mySortingOrder);

        Transform t = newLine.transform;
        t.parent = transform;

        mySortingOrder++;
    }

    void SetupRaycastLogic(DrawLine drawLine)
    {
        if (arMode)
        {
            drawLine.raycastDelegate = GetArRaycastLogic;
        }
        else
        {
            drawLine.raycastDelegate = GetNonArRaycastLogic;
        }
        drawLine.gameObject.SetActive(true);
    }

    bool GetNonArRaycastLogic(out Vector3 hitPosition)
    {
        var point = Input.mousePosition;


        Ray ray = GetComponent<Camera>().ScreenPointToRay(point);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            hitPosition = hit.point;
            return true;
        }
        else
        {
            hitPosition = Vector3.zero;
            return false;
        }
    }
    public void SetLineWidth(float width)
    {
        myLineWidth = width;
    }
    public void SetLineColor(Color colour)
    {
        lineColor = colour;

    }
}
