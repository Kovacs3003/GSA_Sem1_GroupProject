using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform pivot; // CameraPivot
    public float rotateSpeed = 3f;
    public float zoomSpeed = 5f;
    public float minDistance = 3f;
    public float maxDistance = 20f;

    float distance;
    float yaw = 0f;
    float pitch = 20f;

    void Start()
    {
        distance = Vector3.Distance(Camera.main.transform.position, pivot.position);
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            yaw += Input.GetAxis("Mouse X") * rotateSpeed;
            pitch -= Input.GetAxis("Mouse Y") * rotateSpeed;
            pitch = Mathf.Clamp(pitch, -80f, 80f);
        }

        distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);

        Camera.main.transform.position = pivot.position + offset;
        Camera.main.transform.LookAt(pivot);
    }
}