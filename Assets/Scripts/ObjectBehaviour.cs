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

    private Transform moveTarget;
    private Rigidbody rigid;
    private ObjectInteractionState state;
    private Quaternion originRotation;
    private float timeSinceInteract;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        timeSinceInteract = 0;
        state = ObjectInteractionState.put;
        moveTarget = transform;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        timeSinceInteract += Time.deltaTime;
        switch (state)
        {
            case ObjectInteractionState.up:
                rigid.MovePosition(moveTarget.position);
                if (Vector3.Distance(transform.position, moveTarget.position) < 0.1)
                    state = ObjectInteractionState.held;
                transform.parent = moveTarget;
                break;
            //float interpolation = (launchDuration - timeSinceInteract) / launchDuration;
            //Vector3 direction = (moveTarget.position - transform.position) + launch * upCurve.Evaluate(interpolation) * Vector3.up;
            //Vector3 translation = speed * Time.deltaTime * direction.normalized;

            //if (Vector3.Magnitude(translation) < Vector3.Distance(transform.position, moveTarget.position))
            //{
            //    rigid.MovePosition(transform.position + translation);
            //}
            //else
            //{
            //    rigid.MovePosition(moveTarget.position);
            //    state = ObjectInteractionState.held;
            //    transform.parent = moveTarget;
            //}
            case ObjectInteractionState.held:
                break;
            case ObjectInteractionState.down:
                rigid.MovePosition(moveTarget.position);
                if (Vector3.Distance(transform.position, moveTarget.position) < 0.1)
                    state = ObjectInteractionState.put;
                    transform.rotation = originRotation;
                break;
            case ObjectInteractionState.put:
                break;
        }
    }

    public void PickUp(Transform target)
    {
        if (state == ObjectInteractionState.put)
        {
            timeSinceInteract = 0;
            state = ObjectInteractionState.up;
            moveTarget = target;
        }
    }

    public void PutDown(Transform target)
    {
        if (state == ObjectInteractionState.held)
        {
            target.Translate(new Vector3(0, GetComponent<Collider>().bounds.extents.y, 0));
            timeSinceInteract = 0;
            state = ObjectInteractionState.down;
            transform.parent = null;
            moveTarget = target;
        }
    }
}
