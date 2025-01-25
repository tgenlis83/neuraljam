using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;
    public Vector3 movingTarget;
    public Vector3 lookRotation;
    public float smoothSpeed = 0.125f;
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;

    public void SetCameraPosition(Vector3 position)
    {
        movingTarget = position + offset;
        transform.position = movingTarget;
    }

    void Update()
    {
        if (IsMouseInScreen() && Application.isFocused)
        {
            HandleMousePan();
        }

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, movingTarget, smoothSpeed);
        transform.position = smoothedPosition;
        transform.rotation = FinalRotation();
    }

    bool IsMouseInScreen()
    {
        return Input.mousePosition.x >= 0 && Input.mousePosition.x <= Screen.width &&
               Input.mousePosition.y >= 0 && Input.mousePosition.y <= Screen.height;
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
    }
}