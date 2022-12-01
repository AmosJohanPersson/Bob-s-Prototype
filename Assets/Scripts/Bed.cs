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
        SceneManager.LoadSceneAsync(day);
        currentDay = day;
    }

    public void SetSleepEnabled(bool state)
    {
        Color newColor = state ? Color.white : new Color(0.8207547f, 0.1587308f, 0.1587308f);
        GetComponent<BedOutline>().OutlineColor = newColor;
        maySleep = state;
    }

    public void OnInteract()
    {
        Debug.Log(maySleep);
        if (maySleep && currentDay + 1 < SceneManager.sceneCount) 
        { 
            LoadDay(currentDay + 1); 
        }
        else if (maySleep)
        {
            //end game
        }
    }
}
