using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathUI : MonoBehaviour
{
    public GameObject DeathText;
    private void Update()
    {
        if (GameObject.Find("Player") == null)
        {
            DeathText.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
