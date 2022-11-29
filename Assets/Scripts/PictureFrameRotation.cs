using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureFrameRotation : CustomPickupBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Vector3 cameraLookAtAdjustment;

    public override void OnPickup()
    {
        if (text != null && !hasSentText)
        {
            SendNarrative();
            hasSentText = true;
        }

        if (detritus != null)
            Destroy(detritus);

        Vector3 lookAtTarget = cam.transform.position - cameraLookAtAdjustment;
        transform.LookAt(lookAtTarget);
    }
}
