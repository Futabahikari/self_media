using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResumeUI : MonoBehaviour
{
    public GameObject ResumeButton;
    public GameObject ExitButton;
    public void ClickRestart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
