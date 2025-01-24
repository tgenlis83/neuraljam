using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhoneHandler : MonoBehaviour
{
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
}
