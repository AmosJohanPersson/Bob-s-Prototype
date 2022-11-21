using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{

    [SerializeField] private Camera cam;
    [SerializeField] private float tolerance;
    [SerializeField] private float speed;

    [SerializeField] private AnimationCurve upCurve;
    [SerializeField] private float launch;
    [SerializeField] private float launchDuration;

    private Vector3 origin;
    private Rigidbody rigid;
    private float timeSinceInteract;

    private void Start()
    {
        origin = transform.position;
        rigid = GetComponent<Rigidbody>();
        timeSinceInteract = 0;
    }

    private void Update()
    {
        HandleMovement(cam.transform.position + new Vector3(-.25f, -.5f, .15f));
    }

    private void HandleMovement(Vector3 target)
    {
        timeSinceInteract += Time.deltaTime;
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
        }
    }
}
