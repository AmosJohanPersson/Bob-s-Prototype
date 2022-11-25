using System.Collections.Generic;
using UnityEngine;

public class DeskManager : MonoBehaviour
{
    [SerializeField] private List<ObjectBehaviour> objectives;
    [SerializeField] private UIHandler UI;

    private static DeskManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UI.UpdateUI(0, objectives.Count);
    }

    private static DeskManager GetInstance()
    {
        return instance;
    }

    public static void UpdateTask()
    {
        DeskManager DM = GetInstance();
        int correct = 0;
        foreach (ObjectBehaviour o in DM.objectives)
        {
            if (o.InCorrectSpot()) correct++;
        }
        if (correct >= DM.objectives.Count)
        {
            Debug.Log("You did it!");
        }
        DM.UI.UpdateUI(correct, DM.objectives.Count);
    }

    public static UIHandler GetUI()
    {
        DeskManager DM = GetInstance();
        return DM.UI;
    }
}
