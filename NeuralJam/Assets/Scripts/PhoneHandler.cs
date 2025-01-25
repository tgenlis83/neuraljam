using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhoneHandler : MonoBehaviour
{
    private static PhoneHandler instance;

    public static PhoneHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PhoneHandler>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("PhoneHandler");
                    instance = obj.AddComponent<PhoneHandler>();
                }
            }
            return instance;
        }
    }
    public ConversationHandler conversationHandler;
    public TMP_InputField inputField;


    public void SendText()
    {
        if (string.IsNullOrWhiteSpace(inputField.text))
        {
            return;
        }
        conversationHandler.SendText(inputField.text);
        ClearText();
    }

    public void ReceiveText(string text)
    {
        conversationHandler.ReceiveText(text);
    }

    public void ClearText()
    {
        inputField.text = "";
    }

    private CanvasGroup canvasGroup;
    public void Show(Passenger passenger)
    {
        // retrieve conversation and initialize the conversation handler

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }
}
