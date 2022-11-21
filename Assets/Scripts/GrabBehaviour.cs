using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabBehaviour : MonoBehaviour
{
    [SerializeField] GameObject handPoint;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void OnFire(InputValue input)
    {
    }
}
