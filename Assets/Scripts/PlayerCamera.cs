using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("General")]
    public Transform playerBody = null;
    [Header("Rotation")]
    [SerializeField] float mouseSensitivity = 0f;
    [SerializeField] float verticalRotationLimit = 0f;
    float xRotation;

    void Start()
    {
        xRotation = 0f;
    }

    void Update()
    {
        CameraRotation();
    }

    void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalRotationLimit, verticalRotationLimit);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
