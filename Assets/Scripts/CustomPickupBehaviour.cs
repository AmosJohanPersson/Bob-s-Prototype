using UnityEngine;

public class CustomPickupBehaviour : MonoBehaviour
{
    [SerializeField] Vector3 rotationOnHeld;
    [SerializeField] Vector3 rotationOnDown;
    [SerializeField] string text;
    [SerializeField] float textDuration;
    public virtual void OnPickup()
    {
        if (rotationOnHeld != Vector3.zero)
            transform.rotation = Quaternion.Euler(rotationOnHeld.x, rotationOnHeld.y, rotationOnHeld.z);
    }

    public virtual void OnPutDown()
    {
        if (rotationOnDown != Vector3.zero)
            transform.rotation = Quaternion.Euler(rotationOnDown.x, rotationOnDown.y, rotationOnDown.z);
    }

    public virtual void SendNarrative()
    {
        UIHandler UI = DeskManager.GetUI();
        UI.QueueNarrative(text, textDuration);
    }
}