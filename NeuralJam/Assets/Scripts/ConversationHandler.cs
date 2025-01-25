using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ConversationHandler : MonoBehaviour
{
    public GameObject receiveTextPrefab;
    public GameObject sendTextPrefab;

    public Transform textParent;
    public Scrollbar scrollbar;

    public void SendText(string text)
    {
        GameObject textObject = Instantiate(sendTextPrefab, textParent);
        textObject.GetComponentInChildren<TextMeshProUGUI>().text = text;
        ForceUpdateContentSizeFitter(textObject);
        // Set the slider value to the maximum value
        scrollbar.value = 0;
    }

    public void ReceiveText(string text)
    {
        GameObject textObject = Instantiate(receiveTextPrefab, textParent);
        textObject.GetComponentInChildren<TextMeshProUGUI>().text = text;
        ForceUpdateContentSizeFitter(textObject);
        // Set the slider value to the maximum value
        scrollbar.value = 0;
    }

    private void ForceUpdateContentSizeFitter(GameObject textObject)
    {
        ContentSizeFitter[] contentSizeFitters = textObject.GetComponentsInChildren<ContentSizeFitter>();
        foreach (var fitter in contentSizeFitters)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(fitter.GetComponent<RectTransform>());
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ReceiveText("Hi!");
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SendText("Hello!");
        }
    }
}
