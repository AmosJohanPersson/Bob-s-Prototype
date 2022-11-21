using UnityEngine;
using UnityEngine.InputSystem;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] Vector3 camOffset;
    [Range(0.1f, 9f)] [SerializeField] float sensitivity = 2f;
    [Range(0f, 90f)] [SerializeField] float yRotationLimit = 88f;

    public float Sensitivity
    {
        get { return sensitivity; }
        set { sensitivity = value; }
    }

    private Camera cam;

    private Vector2 camRotation = Vector2.zero;
    private Vector2 mouseInput = Vector2.zero;

    private void Start()
    {
        Cursor.visible = false;
        cam = Camera.main;
    }
    void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    private void OnLook(InputValue value)
    {
        mouseInput = value.Get<Vector2>();
    }


    private void HandleRotation()
    {
        //read input
        camRotation += mouseInput * sensitivity;
        camRotation.y = Mathf.Clamp(camRotation.y, -yRotationLimit, yRotationLimit);
        //transform to quaternions
        var xQuat = Quaternion.AngleAxis(camRotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(camRotation.y, Vector3.left);
        //compose together to get final rotation
        cam.transform.localRotation = xQuat * yQuat;
    }

    private void HandleMovement()
    {
        cam.transform.position = transform.position + camOffset;
    }

    public Quaternion Get2DRotation()
    {
        Vector3 rotation2D = cam.transform.localRotation.eulerAngles;
        rotation2D.x = 0;
        rotation2D.z = 0;
        return Quaternion.Euler(rotation2D);
    }

}
