using UnityEngine;
using UnityEngine.Rendering.Universal;

enum ObjectInteractionState
{
    held,
    up,
    down,
    put
}

public class ObjectBehaviour : MonoBehaviour
{

    //[SerializeField] private float speed;
    //[SerializeField] private AnimationCurve upCurve;
    //[SerializeField] private float launch;
    //[SerializeField] private float launchDuration;
    //private float timeSinceInteract;
    [Header("Goal")]
    [SerializeField] GameObject goalObject;
    [Tooltip("Maximum snap-to distance. Not used if goal has a collider component.")]
    [SerializeField] private float tolerance;

    private Transform moveTarget;
    private Rigidbody rigid;
    private ObjectInteractionState state;
    private Quaternion originRotation;

    private void Start()
    {
        //timeSinceInteract = 0;
        rigid = GetComponent<Rigidbody>();
        state = ObjectInteractionState.put;
        moveTarget = transform;
        originRotation = transform.rotation;
    }

    private void Update()
    {
        HandleMovement();
        
    }

    private void HandleMovement()
    {
        //timeSinceInteract += Time.deltaTime;
        switch (state)
        {
            case ObjectInteractionState.up:
                rigid.MovePosition(moveTarget.position);
                if (Vector3.Distance(transform.position, moveTarget.position) < 0.05)
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
                if (Vector3.Distance(transform.position, moveTarget.position) < 0.05)
                    state = ObjectInteractionState.put;
                    transform.rotation = originRotation;
                break;
            case ObjectInteractionState.put:
                break;
        }
    }

    private float CalculateErrorDistance(Vector3 position)
    {
        Vector3 position2D = new Vector3(position.x, 0, position.z);
        Vector3 goal2D = new Vector3(goalObject.transform.position.x, 0, goalObject.transform.position.z);
        return Vector3.Distance(position2D, goal2D);
    }

    public void PickUp(Transform target)
    {
        if (state == ObjectInteractionState.put)
        {
            //timeSinceInteract = 0;
            state = ObjectInteractionState.up;
            moveTarget = target;
        }
    }

    public void PutDown(Transform target)
    {
        if (state == ObjectInteractionState.held)
        {
            //timeSinceInteract = 0;
            target.Translate(new Vector3(0, GetComponent<Collider>().bounds.extents.y, 0));
            state = ObjectInteractionState.down;
            transform.parent = null;
            moveTarget = target;
        }
    }

    public bool IsCloseEnough(Vector3 target)
    {
        if (goalObject != null && goalObject.GetComponent<Collider>() != null)
        {
            return goalObject.GetComponent<Collider>().bounds.Contains(target);
        }
        else if (goalObject != null)
        {
            return CalculateErrorDistance(target) < tolerance;
        }
        return false;
    }

    public Vector3 GetGoalPosition()
    {
        return goalObject.transform.position;
    }
}
