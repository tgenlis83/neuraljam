using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class KeepActive : MonoBehaviour
{
    private TMP_InputField inputField;
    public UnityEvent onReturnPressed;

    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        d();
    }

    public void d()
    {
        inputField.ActivateInputField();
    }

    private bool wasFocused;

    void Update()
    {
        bool isFocused = inputField.isFocused;
        if (wasFocused && !isFocused && Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Hello World");
            onReturnPressed?.Invoke();
        }
        wasFocused = isFocused;
    }
}