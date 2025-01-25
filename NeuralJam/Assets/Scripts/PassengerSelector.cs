using UnityEngine;

public class PassengerSelector : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    private OutlineInteraction currentPassenger;
    public CameraController cameraController;
    public GhostController ghostController;
    public HoverCursorHandler hoverCursorHandler;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentPassenger != null)
            {
                currentPassenger.OnInteract();
            }
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            OutlineInteraction item = hit.collider.GetComponent<OutlineInteraction>();

            if (item != null)
            {
                if (currentPassenger != item)
                {
                    if (currentPassenger != null)
                    {
                        currentPassenger.OnNotHovered();
                        hoverCursorHandler.DisableCursor();
                    }

                    currentPassenger = item;
                    currentPassenger.OnHovered();
                    hoverCursorHandler.SetCursorText(currentPassenger.interactionName);
                    hoverCursorHandler.EnableCursor();
                }
            }
            else
            {
                if (currentPassenger != null)
                {
                    currentPassenger.OnNotHovered();
                    hoverCursorHandler.DisableCursor();
                    currentPassenger = null;
                }
            }
        }
        else
        {
            if (currentPassenger != null)
            {
                currentPassenger.OnNotHovered();
                hoverCursorHandler.DisableCursor();
                currentPassenger = null;
            }
        }
    }
}
