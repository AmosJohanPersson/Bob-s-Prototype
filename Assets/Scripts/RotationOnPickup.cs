using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationOnPickup : MonoBehaviour
{
    [SerializeField] Vector3 rotationOnHeld;
    [SerializeField] Vector3 rotationOnDown;
    public virtual void PickupRotation()
    {
        if (rotationOnHeld != Vector3.zero)
            transform.rotation = Quaternion.Euler(rotationOnHeld.x, rotationOnHeld.y, rotationOnHeld.z);
    }

    public virtual void PutDownRotation()
    {
        if (rotationOnDown != Vector3.zero)
            transform.rotation = Quaternion.Euler(rotationOnDown.x, rotationOnDown.y, rotationOnDown.z);
    }
}
