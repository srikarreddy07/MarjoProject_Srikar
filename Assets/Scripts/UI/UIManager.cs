using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Player")]
    [SerializeField] Image playerHealth;
    [SerializeField] TextMeshProUGUI killText;

    private void Awake()
    {
        if(instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
    }
    private void OnLevelWasLoaded()
    {
        playerHealth.fillAmount = 1f;
        killText.text = "X " + PlayerPrefs.GetInt("Kills").ToString();
    }

    //private void Start()
    //{
    //    playerHealth.fillAmount = 1f;
    //    killText.text = "X " + PlayerPrefs.GetInt("Kills").ToString();
    //}

    //public void SetHealthUI(float value)
    //{
    //    float temp = value / 100f;

    //    temp = Mathf.Clamp(temp, 0f, 1f);
    //    playerHealth.fillAmount = temp;
    //}

    //public void SetKillCountText ()
    //{
    //    killText.text = "X " + GameManager.instance.killCount;
    //}
}
