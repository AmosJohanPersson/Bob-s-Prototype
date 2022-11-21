using UnityEngine;
using UnityEngine.InputSystem;

public class GrabBehaviour : MonoBehaviour
{
    [SerializeField] GameObject handPoint;

    private Camera cam;
    private bool isCarrying;

    private void Start()
    {
        cam = Camera.main;
        isCarrying = false;
    }

    private void OnFire(InputValue input)
    {
        Vector3 lookingPoint = new Vector3(0.5f, 0.5f, 0f);
        Ray ray = cam.ViewportPointToRay(lookingPoint);
        RaycastHit hit;

        bool didHit = Physics.Raycast(ray, out hit);
        GameObject other = didHit ? hit.collider.gameObject : null;

        if (didHit && other.CompareTag("Grabbable") && !isCarrying)
        {
            other.GetComponent<ObjectBehaviour>().PickUp(handPoint.transform.position);
            isCarrying = true;
        }
    }
}
