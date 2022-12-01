using System.Collections.Generic;
using UnityEngine;

public class DeskManager : MonoBehaviour
{
    [SerializeField] private List<ObjectBehaviour> objectives;
    [SerializeField] private UIHandler UI;
    [SerializeField] private Bed bed;

    private static DeskManager instance;

    private void Awake()
    {
        Debug.Log("waking up") ;
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

        if (bed == null)
        {
            bed = GameObject.FindGameObjectWithTag("Bed").GetComponent<Bed>();
            UpdateTask();
        }
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
            DM.bed.SetSleepEnabled(true);
            Debug.Log("You did it!");
        }
        else
        {
            DM.bed.SetSleepEnabled(false);
        }
        DM.UI.UpdateUI(correct, DM.objectives.Count);
    }

    public static UIHandler GetUI()
    {
        DeskManager DM = GetInstance();
        return DM.UI;
    }

    public static void SetBed(Bed newBed)
    {
        DeskManager DM = GetInstance();
        DM.bed = newBed;
    }
}