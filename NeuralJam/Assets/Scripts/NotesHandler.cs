using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NotesHandler : MonoBehaviour
{
    private static NotesHandler instance;

    public static NotesHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<NotesHandler>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("NotesHandler");
                    instance = obj.AddComponent<NotesHandler>();
                }
            }
            return instance;
        }
    }
    public GameObject notePrefab;

    public Transform textParent;
    public VerticalLayoutGroup verticalLayoutGroup;
    public Scrollbar scrollbar;

    public void SendNote(string text)
    {
        GameObject textObject = Instantiate(notePrefab, textParent);
        textObject.GetComponentInChildren<TextMeshProUGUI>().text = text;
        ForceUpdateContentSizeFitter(textObject);
        // Set the slider value to the maximum value
        scrollbar.value = 0;
        // force update on verticalLayoutGroup
        LayoutRebuilder.ForceRebuildLayoutImmediate(verticalLayoutGroup.GetComponent<RectTransform>());
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
        if (Input.GetKeyDown(KeyCode.U))
        {
            SendNote("{name} talked to {name} about {topic}");
        }
    }

    private CanvasGroup canvasGroup;
    public void Show()
    {
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
