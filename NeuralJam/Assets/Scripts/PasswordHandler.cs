using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PasswordHandler : MonoBehaviour
{
    private static PasswordHandler instance;

    public static PasswordHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PasswordHandler>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("PasswordHandler");
                    instance = obj.AddComponent<PasswordHandler>();
                }
            }
            return instance;
        }
    }

    public TMP_InputField inputField;
    public Slider proximitySlider;
    public Image noImage;
    public GameObject noCross;
    public Sprite neutralSprite;
    public Sprite noSprite;
    public Sprite yesSprite;
    public Image yesImage;
    public GameObject noCheckmark;

    [SerializeField]
    string password;

    public void SetPassword(string password)
    {
        this.password = password;
    }

    public void OnFieldUpdated()
    {
        int maxDistance = Mathf.Max(password.Length, inputField.text.Length, 1);
        int distance = levensteinDistance(password.ToLower(), inputField.text.ToLower());
        int value = Mathf.FloorToInt((maxDistance - distance) / (float)maxDistance * 8);
        Debug.Log("Distance: " + distance + " Value: " + value + " Max: " + maxDistance);
        proximitySlider.value = value;
        noImage.sprite = value < 7 ? noSprite : neutralSprite;
        noCross.SetActive(value < 7);
        yesImage.sprite = value >= 8 ? yesSprite : neutralSprite;
        noCheckmark.SetActive(value >= 8);
        if (value >= 8)
        {
            GameManager.Instance.NextWagon();
            inputField.text = "";
            Hide();
        }
    }

    int levensteinDistance(string a, string b)
    {
        int[,] dp = new int[a.Length + 1, b.Length + 1];

        for (int i = 0; i <= a.Length; i++)
        {
            for (int j = 0; j <= b.Length; j++)
            {
                if (i == 0)
                {
                    dp[i, j] = j;
                }
                else if (j == 0)
                {
                    dp[i, j] = i;
                }
                else
                {
                    dp[i, j] = Mathf.Min(
                        dp[i - 1, j - 1] + (a[i - 1] == b[j - 1] ? 0 : 1),
                        dp[i - 1, j] + 1,
                        dp[i, j - 1] + 1
                    );
                }
            }
        }

        return dp[a.Length, b.Length];
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
