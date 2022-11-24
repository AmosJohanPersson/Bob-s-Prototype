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
    [Header("Height Offset")]
    [SerializeField] float offset;
    [Header("Goal")]
    [SerializeField] GameObject goalObject;
    [Tooltip("Maximum snap-to distance. Not used if goal has a collider component.")]
    [SerializeField] private float tolerance;

    private Transform moveTarget;
    private Rigidbody rigid;
    private ObjectInteractionState state;
    private Quaternion originRotation;
    private Transform originParent;
    private RotationOnPickup rotationScript;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        state = ObjectInteractionState.put;
        moveTarget = transform;
        originRotation = transform.rotation;
        rotationScript = GetComponent<RotationOnPickup>();
        originParent = transform.parent;
    }

    private void Update()
    {
        HandleMovement();
        
    }

    private void HandleMovement()
    {
        switch (state)
        {
            case ObjectInteractionState.up:
                rigid.MovePosition(moveTarget.position);
                if (Vector3.Distance(transform.position, moveTarget.position) < 0.05)
                {
                    state = ObjectInteractionState.held;
                    transform.parent = moveTarget;
                    DeskManager.UpdateTask();
                    if (rotationScript != null) rotationScript.PickupRotation();
                }
                break;
            case ObjectInteractionState.held:
                break;
            case ObjectInteractionState.down:
                rigid.MovePosition(moveTarget.position);
                if (Vector3.Distance(transform.position, moveTarget.position) < 0.05)
                {
                    state = ObjectInteractionState.put;
                    if (rotationScript == null)
                    {
                        transform.rotation = originRotation;
                    }
                    else
                    {
                        rotationScript.PutDownRotation();
                    }
                    DeskManager.UpdateTask();
                }
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
            state = ObjectInteractionState.up;
            moveTarget = target;
            ShowGoal(true);
        }
    }

    public void PutDown(Transform target)
    {
        if (state == ObjectInteractionState.held)
        {
            Bounds hitBox = GetComponent<Collider>().bounds;
            var heightAdjustment = transform.position - hitBox.ClosestPoint(transform.position - Vector3.up);
            
            target.Translate(new Vector3(0, heightAdjustment.y + offset, 0));
            state = ObjectInteractionState.down;
            transform.parent = originParent;
            moveTarget = target;
            ShowGoal(false);
        }
    }

    public bool IsPointCloseToGoal(Vector3 target)
    {
        if (goalObject == null) return false;

        var collider = goalObject.GetComponent<Collider>();
        if (collider != null)
        {
            return collider.bounds.Contains(target);
        }
        else
        {
            return CalculateErrorDistance(target) < tolerance;
        }
    }

    private void ShowGoal(bool show)
    {
        if (goalObject == null) return;
        var particles = goalObject.GetComponent<ParticleSystem>();
        if (particles == null) return;

        if(show)
        {
            particles.Play();
        }
        else
        {
            particles.Stop();
        }
    }
    public bool InCorrectSpot()
    {
        return IsPointCloseToGoal(transform.position);
    }

    public Vector3 GetGoalPosition()
    {
        return goalObject.transform.position;
    }
}
