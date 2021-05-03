using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopRenderer : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (UIManager.stopDrawing == true)
        {
            gameObject.GetComponent<LineRenderer>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<LineRenderer>().enabled = true;
        }
    }
}
