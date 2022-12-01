using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Text progress;
    [SerializeField] private Text goToBed;

    [SerializeField] private GameObject narrativeBackground;
    [SerializeField] private Text narrativeMessagebox;

    private Queue<string> messageQueue;
    private Queue<float> durationQueue;
    private float timeCounter;

    private float curDuration;

    private void Start()
    {
        messageQueue = new Queue<string>();
        durationQueue = new Queue<float>();
        curDuration = 0;
    }

    private void Update()
    {
        TickQueue();
    }

    public void UpdateUI(int count, int max)
    {
        progress.text = string.Format("Clean desk ({0}/{1})", count, max);
        if (count >= max)
        {
            goToBed.gameObject.SetActive(true);
        }
        else
        {
            goToBed.gameObject.SetActive(false);
        }
    }

    public void QueueNarrative(string message, float duration = 8f)
    {
        messageQueue.Enqueue(message);
        durationQueue.Enqueue(duration);
    }

    public void TickQueue()
    {
        timeCounter += Time.deltaTime;

        if (timeCounter >= curDuration)
        {
            HideMessage();
            curDuration = 0;
            timeCounter = 0;

            string newMessage;
            float newDuration;
            if (messageQueue.TryDequeue(out newMessage))
                DisplayMessage(newMessage);
            if (durationQueue.TryDequeue(out newDuration))
                curDuration = newDuration;
        }
    }

    public void HideMessage()
    {
        narrativeBackground.SetActive(false);
        narrativeMessagebox.gameObject.SetActive(false);
    }

    public void DisplayMessage(string message)
    {
        narrativeMessagebox.text = message;
        narrativeBackground.SetActive(true);
        narrativeMessagebox.gameObject.SetActive(true);
    }
}
