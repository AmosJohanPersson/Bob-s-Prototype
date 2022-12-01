using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bed : MonoBehaviour
{
    private bool maySleep = false;
    private int currentDay = 0;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void LoadDay(int day)
    {
        SceneManager.LoadScene(day);
        currentDay = day;
        GetComponent<BedOutline>().TurnOffOutline();
    }

    public void SetSleepEnabled(bool state)
    {
        Color newColor = state ? Color.white : new Color(0.8207547f, 0.1587308f, 0.1587308f);
        GetComponent<BedOutline>().OutlineColor = newColor;
        maySleep = state;
    }

    public void OnInteract()
    {
        if (maySleep && currentDay + 1 < SceneManager.sceneCountInBuildSettings) 
        { 
            LoadDay(currentDay + 1); 
        }
        else if (maySleep)
        {
            Application.Quit();
        }
    }
}
