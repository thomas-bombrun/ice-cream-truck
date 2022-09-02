using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Range(0f, 500f)]
    public float mouseSensitivity = 100.0f;

    public Transform playerTransform;

    float xRotation = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void FixedUpdate()
    {
        float xMouse = Input.GetAxis("Mouse X") * this.mouseSensitivity * Time.fixedDeltaTime;
        float yMouse = Input.GetAxis("Mouse Y") * this.mouseSensitivity * Time.fixedDeltaTime;

        this.xRotation -= yMouse;
        this.xRotation = Mathf.Clamp(this.xRotation, -90.0f, 90.0f);
        this.transform.localRotation = Quaternion.Euler(this.xRotation, 0.0f, 0.0f);
        this.playerTransform.Rotate(Vector3.up * xMouse);
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
