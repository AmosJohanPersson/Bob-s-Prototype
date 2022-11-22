using UnityEngine;
using UnityEngine.InputSystem;

public class GrabBehaviour : MonoBehaviour
{
    [SerializeField] GameObject handPoint;
    [SerializeField] GameObject markerPrefab;

    private Camera cam;
    private ObjectBehaviour carried;
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
            carried = other.GetComponent<ObjectBehaviour>();
            carried.PickUp(handPoint.transform);
            isCarrying = true;
        }
        else if (didHit && other.CompareTag("Surface") && isCarrying)
        {
            var position = hit.point;
            GameObject destination = Instantiate(markerPrefab, position, Quaternion.identity);
            carried.PutDown(destination.transform);
            isCarrying = false;
            Destroy(destination, 5);
        }
    }
}
