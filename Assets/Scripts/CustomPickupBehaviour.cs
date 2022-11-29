using UnityEngine;

public class CustomPickupBehaviour : MonoBehaviour
{
    [SerializeField] protected GameObject detritus;

    [SerializeField] protected Vector3 rotationOnHeld;
    [SerializeField] protected Vector3 rotationOnDown;
    [SerializeField] protected Vector3 rotationOnGoal;

    [SerializeField] protected string text;
    [SerializeField] protected float textDuration;

    protected bool hasSentText = false;

    public virtual void OnPickup()
    {
        if (text != null && !hasSentText)
        {
            SendNarrative();
            hasSentText = true;
        }

        if (detritus != null)
            Destroy(detritus);

        if (rotationOnHeld != Vector3.zero)
            transform.rotation = Quaternion.Euler(rotationOnHeld);
    }

    public virtual void OnPutDown(bool onGoal = false)
    {
        if (onGoal && rotationOnGoal != Vector3.zero)
            transform.rotation = Quaternion.Euler(rotationOnGoal);
        else if (rotationOnDown != Vector3.zero)
            transform.rotation = Quaternion.Euler(rotationOnDown);
    }

    public virtual void SendNarrative()
    {
        UIHandler UI = DeskManager.GetUI();
        UI.QueueNarrative(text, textDuration);
    }
}
