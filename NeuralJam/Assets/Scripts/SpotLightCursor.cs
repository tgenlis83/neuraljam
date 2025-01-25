using UnityEngine;

public class SpotLightCursor : MonoBehaviour
{
    public Light spotLight;
    private Camera cam;

    public Vector3 localOffset;
    public float rotationSpeed = 5f; // Speed of the rotation

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.nearClipPlane;
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        
        // Set the spotlight position to the origin middle bottom of the camera with local offset
        spotLight.transform.position = cam.ViewportToWorldPoint(new Vector3(0.5f, 0f, cam.nearClipPlane)) + cam.transform.TransformVector(localOffset);

        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;
        Quaternion targetRotation;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            targetRotation = Quaternion.LookRotation(hit.point - spotLight.transform.position);
        }
        else
        {
            targetRotation = Quaternion.LookRotation(cam.transform.forward);
        }

        // Smoothly interpolate the rotation using Slerp
        spotLight.transform.rotation = Quaternion.Slerp(spotLight.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
