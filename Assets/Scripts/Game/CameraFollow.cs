using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] Transform target;
    [SerializeField] float smoothTime = 0.15f; // time to follow target
    [SerializeField] Vector3 velocity = Vector3.zero;
    [SerializeField] Vector3 offset;

    [Header("Bounds")]
    [SerializeField] bool xMinEnabled = false;
    [SerializeField] float xMinValue = 0f;
    [SerializeField] bool xMaxEnabled = false;
    [SerializeField] float xMaxValue = 0f;
    [SerializeField] bool yMinEnabled = false;
    [SerializeField] float yMinValue = 0f;
    [SerializeField] bool yMaxEnabled = false;
    [SerializeField] float yMaxValue = 0f;


    private void FixedUpdate()
    {
        // target position
        Vector3 targetPosition = target.position - offset;

        // Vertical
        if (yMinEnabled && yMaxEnabled)
            targetPosition.y = Mathf.Clamp(targetPosition.y, yMinValue, yMaxValue);
        else if (yMinEnabled)
            targetPosition.y = Mathf.Clamp(targetPosition.y, yMinValue, targetPosition.y);
        else if (yMaxEnabled)
            targetPosition.y = Mathf.Clamp(targetPosition.y, target.position.y, yMaxValue);

        // Horizontal
        if (xMinEnabled && xMaxEnabled)
            targetPosition.x = Mathf.Clamp(targetPosition.x, xMinValue, xMaxValue);
        else if (xMinEnabled)
            targetPosition.x = Mathf.Clamp(targetPosition.x, xMinValue, targetPosition.x);
        else if (xMaxEnabled)
            targetPosition.x = Mathf.Clamp(targetPosition.x, target.position.x, xMaxValue);

        // Algin the camera target z position
        targetPosition.z = transform.position.z;

        // Using smooth damp we will graduallx change the camera position based on the camera position and smooth time
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
