using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveBehaviour : MonoBehaviour
{

    [SerializeField] float moveSpeed;

    private Rigidbody rigid;
    private CameraBehaviour camBehaviour;
    private Vector2 moveInput = Vector2.zero;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        camBehaviour = GetComponent<CameraBehaviour>();
    }

    private void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }


    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void HandleRotation()
    {
        transform.localRotation = camBehaviour.Get2DRotation();
    }

    private void HandleMovement()
    {
        var movement = transform.localRotation * new Vector3(moveInput.x, 0, moveInput.y);
        rigid.MovePosition(rigid.position + moveSpeed * Time.deltaTime * movement);
    }
}
