using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Text progress;
    [SerializeField] private Text goToBed;


    public void UpdateUI(int count, int max)
    {
        progress.text = string.Format("Clean desk ({0}/{1})", count, max);
        if (count >= max)
        {
            goToBed.gameObject.SetActive(true);
        }
    }
}
