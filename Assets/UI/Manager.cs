using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    private GameObject player;
    private GameObject enemyG;

    private bool isPause = false;
    GameObject stopUI;
    GameObject resumeUI;
    GameObject ExitUI;

    private GameObject InGameUI;
    private GameObject DeathUI;

    private void Awake()
    {
        
    }
    private void Start()
    {
        player = GameObject.Find("Player");
        enemyG = GameObject.Find("EnemyGenerate");

        stopUI = GameObject.Find("StopUI").GetComponent<PausUI>().stopButton;
        resumeUI = GameObject.Find("Restart").GetComponent<ResumeUI>().ResumeButton;
        ExitUI = GameObject.Find("Restart").GetComponent<ResumeUI>().ExitButton;
        //InGameUIButtons = GameObject.Find("InGameUIButtons");
        //DeathUI = GameObject.Find("DeathUI");

        player.GetComponent<PlayerHealth>().enabled = false;
        player.GetComponent<Player>().enabled = false;
        enemyG.GetComponent<EnemyGenerate>().enabled = false;

        //stopUI.SetActive(false);
        //DeathUI.SetActive(false);
    }

    

    public void ClickOut()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//编辑状态下退出
#else
        Application.Quit();//打包编译后退出
#endif
    }

    public void ClickStart()
    {
        player.GetComponent<PlayerHealth>().enabled = true;
        player.GetComponent<Player>().enabled = true;
        enemyG.GetComponent<EnemyGenerate>().enabled = true;
        stopUI.SetActive(true);
        resumeUI.SetActive(true);
        ExitUI.SetActive(true);


        GameObject.Find("StartUI").SetActive(false);
    }
}

