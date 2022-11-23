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
        switch (state)
        {
            case ObjectInteractionState.up:
                rigid.MovePosition(moveTarget.position);
                if (Vector3.Distance(transform.position, moveTarget.position) < 0.05)
                {
                    state = ObjectInteractionState.held;
                    transform.parent = moveTarget;
                }
                break;
            case ObjectInteractionState.held:
                break;
            case ObjectInteractionState.down:
                rigid.MovePosition(moveTarget.position);
                if (Vector3.Distance(transform.position, moveTarget.position) < 0.05)
                {
                    state = ObjectInteractionState.put;
                    transform.rotation = originRotation;
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
            target.Translate(new Vector3(0, GetComponent<Collider>().bounds.extents.y, 0));
            state = ObjectInteractionState.down;
            transform.parent = null;
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
