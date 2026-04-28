using UnityEngine;

public class SimpleFPSController : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 2f;

    private float xRotation = 0f;
    private Transform playerCamera;

    void Start()
    {
        // Находим камеру внутри игрока
        playerCamera = GetComponentInChildren<Camera>().transform;
        // Блокируем курсор в центре экрана
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Вращение камеры от мыши
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Движение с помощью клавиш WASD
        float moveX = Input.GetAxis("Horizontal"); // A и D
        float moveZ = Input.GetAxis("Vertical");   // W и S

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        transform.position += move * speed * Time.deltaTime;
    }
}