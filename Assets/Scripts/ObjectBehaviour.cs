using UnityEngine;

enum ObjectInteractionState
{
    held,
    up,
    down,
    put
}

public class ObjectBehaviour : MonoBehaviour
{

    [SerializeField] private Camera cam;
    [SerializeField] private float tolerance;
    [SerializeField] private float speed;

    [SerializeField] private AnimationCurve upCurve;
    [SerializeField] private float launch;
    [SerializeField] private float launchDuration;

    private Vector3 origin;
    private Vector3 moveTarget;
    private Rigidbody rigid;
    private float timeSinceInteract;
    private ObjectInteractionState state;

    private void Start()
    {
        origin = transform.position;
        rigid = GetComponent<Rigidbody>();
        timeSinceInteract = 0;
        state = ObjectInteractionState.put;
        moveTarget = origin;
    }

    private void Update()
    {
        HandleMovement(moveTarget);
    }

    private void HandleMovement(Vector3 target)
    {
        timeSinceInteract += Time.deltaTime;

        switch (state)
        {
            case ObjectInteractionState.up:
                float interpolation = (launchDuration - timeSinceInteract) / launchDuration;
                Vector3 direction = (target - transform.position) + launch * upCurve.Evaluate(interpolation) * Vector3.up;
                Vector3 translation = speed * Time.deltaTime * direction.normalized;

                if (Vector3.Magnitude(translation) < Vector3.Distance(transform.position, target))
                {
                    rigid.MovePosition(transform.position + translation);
                }
                else
                {
                    rigid.MovePosition(target);
                    state = ObjectInteractionState.held;
                }
                break;
            case ObjectInteractionState.held:
                //move with player
                break;
            case ObjectInteractionState.down:
                break;
            case ObjectInteractionState.put:
                break;
        }
    }

    public void PickUp(Vector3 targetPosition)
    {
        if (state == ObjectInteractionState.put)
        {
            timeSinceInteract = 0;
            state = ObjectInteractionState.up;
            moveTarget = targetPosition;
        }
    }

    public void PutDown(Vector3 targetPosition)
    {
        if (state == ObjectInteractionState.held)
        {
            timeSinceInteract = 0;
            state = ObjectInteractionState.down;
            moveTarget = targetPosition;
        }
    }
}
