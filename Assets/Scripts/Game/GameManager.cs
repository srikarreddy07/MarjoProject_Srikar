using UnityEngine;
using LevelManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [Header("Level")]
    private bool isGameOver;
    public bool IsGameOver { get => isGameOver; }

    public static GameManager Instance { get => instance;}

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
    }

    private void Update()
    {
        if (objective != null && objective.IsCompleted)
            EndLevel();
    }

    public void EndLevel()
    {
        if (playerObject != null)
        {            
            Rigidbody rbody = playerObject.GetComponent<Rigidbody>();

            if (rbody != null)
            {
                rbody.velocity = Vector3.zero;
                rbody.Sleep();
            }
        }

        if (IsGameOver == false)
        {
            isGameOver = true;
            LevelCompleted.Open();
        }
    }
}
