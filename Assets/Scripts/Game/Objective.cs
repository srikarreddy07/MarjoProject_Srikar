using UnityEngine;

public class Objective : MonoBehaviour
{
    [SerializeField] bool isCompleted;
    public bool IsCompleted { get => isCompleted; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            isCompleted = true;
    }
}
