using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;
    public Transform target;
    private Vector3 movingTarget;
    public Vector3 lookRotation;
    public float smoothSpeed = 0.125f;
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;

    void Start()
    {
        transform.rotation = FinalRotation();
        if (target != null)
        {
            transform.position = FinalPosition();
            movingTarget = transform.position;
        }
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 desiredPosition = FinalPosition();
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.rotation = FinalRotation();
        }
        else if (IsMouseInScreen() && Application.isFocused)
        {
            movingTarget = transform.position;
            HandleMousePan();
        }
    }

    bool IsMouseInScreen()
    {
        return Input.mousePosition.x >= 0 && Input.mousePosition.x <= Screen.width &&
               Input.mousePosition.y >= 0 && Input.mousePosition.y <= Screen.height;
    }

    Vector3 FinalPosition()
    {
        return target.position + offset;
    }

    Quaternion FinalRotation()
    {
        return Quaternion.Euler(lookRotation);
    }

    void HandleMousePan()
    {
        Vector3 panDirection = Vector3.zero;
        float panBorderThicknessInPixels = Screen.width * (panBorderThickness / 100f);

        if (Input.mousePosition.x >= Screen.width - panBorderThicknessInPixels)
        {
            panDirection.z += 1;
        }
        else if (Input.mousePosition.x <= panBorderThicknessInPixels)
        {
            panDirection.z -= 1;
        }

        if (panDirection != Vector3.zero)
        {
            movingTarget += panDirection * panSpeed * Time.deltaTime;
        }
        transform.position = Vector3.Lerp(transform.position, movingTarget, smoothSpeed);
    }
}
