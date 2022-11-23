using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskManager : MonoBehaviour
{
    [SerializeField] private List<ObjectBehaviour> objectives;

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
    }
}