using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HoverCursorHandler : MonoBehaviour
{
    public TextMeshProUGUI cursorText;
    public GameObject cursor;

    public void SetCursorText(string text)
    {
        DisableCursor();
        EnableCursor();
        cursorText.text = text;
    }

    void Start()
    {
        SetCursorText("");
        DisableCursor();
    }

    void Update()
    {
        if (!cursor.activeSelf || !Application.isFocused)
            return;
        Vector3 cursorPosition = Input.mousePosition;
        cursorPosition.z = 18.0f; // Set this to the distance from the camera
        transform.position = Camera.main.ScreenToWorldPoint(cursorPosition);
    }

    public void EnableCursor()
    {
        cursor.gameObject.SetActive(true);
    }

    public void DisableCursor()
    {
        cursor.gameObject.SetActive(false);
    }
}
