using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int currentScenIndex;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    private void OnLevelWasLoaded()
    {
        currentScenIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene(currentScenIndex + 1); 
        }
    }

    public void LoadStartScence ()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
