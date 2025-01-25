using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

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
        if (string.IsNullOrWhiteSpace(inputField.text) || currentPassenger == null)
        {
            return;
        }
        conversationHandler.SendText(inputField.text);
        StartCoroutine(ISendText());
        ClearText();
    }

    private IEnumerator ISendText()
    {
        string text = inputField.text;
        var chatWithCharacterTask = APIHelper.ChatWithCharacter(GameManager.Instance.sessionId, currentPassenger.uid, new APIHelper.ChatMessage
        {
            message = text
        });
        yield return new WaitUntil(() => chatWithCharacterTask.IsCompleted);
        ReceiveText(chatWithCharacterTask.Result.response);
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
    private Passenger currentPassenger;
    public void Show(Passenger passenger)
    {
        currentPassenger = passenger;
        StartCoroutine(IShow(passenger));
    }

    private IEnumerator IShow(Passenger passenger)
    {
        conversationHandler.ClearTexts();
        // retrieve conversation and initialize the conversation handler
        var chatHistoryTask = APIHelper.GetChatHistory(GameManager.Instance.sessionId, passenger.uid);
        yield return new WaitUntil(() => chatHistoryTask.IsCompleted);

        APIHelper.ChatHistory chatHistory = chatHistoryTask.Result;

        for (int i = 0; i < chatHistory.messages.Length; i++)
        {
            if (chatHistory.messages[i].role == "assistant")
            {
                ReceiveText(chatHistory.messages[i].content);
            }
            else
            {
                conversationHandler.SendText(chatHistory.messages[i].content);
            }
        }

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        currentPassenger = null;
    }

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }
}
