using UnityEngine;
using LevelManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Score")]
    public int killCount;

    [Header("Level")]
    private bool isGameOver;
    public bool IsGameOver { get => isGameOver; }

    [Header("Scripts")]
    [SerializeField] Objective objective;
    [SerializeField] GameObject playerObject;

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;


        objective = Object.FindObjectOfType<Objective>();
        playerObject = GameObject.FindGameObjectWithTag("Player");

        killCount = 0;
    }

    private void OnLevelWasLoaded()
    {
    //    killCount = PlayerPrefs.GetInt("Kills");
    //    UIManager.instance.SetKillCountText();
    }

    public void SetKillCount ()
    {
        killCount++;
    }

    private void Update()
    {
        if (IsGameOver == false)
            EndLevel();
    }

    public void EndLevel()
    {
        //if (playerObject != null)
        //{
        //    Rigidbody rbody = playerObject.GetComponent<Rigidbody>();
        //    if (rbody != null)
        //    {
        //        rbody.velocity = Vector3.zero;
        //    }            
        //    playerObject.Move(Vector3.zero, false, false);
        //}
        
        if (objective != null && objective.IsCompleted)
        {
            isGameOver = true;
            LevelCompleted.Open();
        }
    }
}
