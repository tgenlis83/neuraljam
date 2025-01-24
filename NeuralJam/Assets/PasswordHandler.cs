using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PasswordHandler : MonoBehaviour
{
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
        int maxDistance = Mathf.Max(password.Length, inputField.text.Length);
        int distance = levensteinDistance(password.ToLower(), inputField.text.ToLower());
        Debug.Log("Max Distance: " + maxDistance + ", Distance: " + distance);
        float value = Mathf.Clamp((maxDistance - distance) / (float)maxDistance, 0, 1) * 8;
        proximitySlider.value = value;
        Debug.Log("Value: " + value);
        noImage.sprite = value < 7 ? noSprite : neutralSprite;
        noCross.SetActive(value < 7);
        yesImage.sprite = value > 7 ? yesSprite : neutralSprite;
        noCheckmark.SetActive(value > 7);
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
}
