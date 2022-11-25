using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceHeight : MonoBehaviour
{
    [SerializeField] private float[] surfaceHeight;

    public float GetBestHeight(Vector3 position)
    {
        float bestDistance = float.PositiveInfinity;
        int bestIndex = 0;
        for (int i = 0; i < surfaceHeight.Length; i++)
        {
            float currentDistance = Mathf.Abs(position.y - surfaceHeight[i]);
            if (bestDistance > currentDistance)
            {
                bestIndex = i;
                bestDistance = currentDistance;
            }
        }
        return surfaceHeight[bestIndex];
    }

}
