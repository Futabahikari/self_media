using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausUI : MonoBehaviour
{
    public bool isPause = false;
    public GameObject stopButton;
    void Start()
    {
        
    }

    public void ClickStop()
    {
        if (!isPause)
        {
            Time.timeScale = 0;
            isPause = true;
            stopButton.GetComponentInChildren<Text>().text = "resume";
        }
        else
        {
            Time.timeScale = 1;
            isPause = false;
            stopButton.GetComponentInChildren<Text>().text = "stop";
        }
    }
}
