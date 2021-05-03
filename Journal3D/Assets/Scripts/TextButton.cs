using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextButton : MonoBehaviour
{
    public GameObject WindowText;

    public void OpenWindow()
    {
        if (WindowText != null)
        {
            bool isActive = WindowText.activeSelf;

            WindowText.SetActive(!isActive);
        }
    }
}
