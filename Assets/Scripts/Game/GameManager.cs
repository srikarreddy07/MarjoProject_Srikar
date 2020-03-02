
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Score")]
    public int killCount;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

        killCount = 0;
        PlayerPrefs.SetInt("Kills", killCount);
    }

    private void OnLevelWasLoaded()
    {
        killCount = PlayerPrefs.GetInt("Kills");
        UIManager.instance.SetKillCountText();
    }

    public void SetKillCount ()
    {
        killCount++;
    }
}
