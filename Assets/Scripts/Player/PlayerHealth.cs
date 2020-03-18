using UnityEngine;
using LevelManagement.Data;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float currentHealth;

    [Header("Components")]
    [SerializeField] SpriteRenderer spr;
    [SerializeField] UpdateAnimations animator;

    private void Start()
    {
        if (DataManager.Instance != null)
        {
            DataManager.Instance.Load();
            currentHealth = DataManager.Instance.PlayerHealth;
        }
        else
            currentHealth = 100f;

        spr = GetComponent<SpriteRenderer>();
        animator = GetComponent<UpdateAnimations>();
    }

    public void TakeDamage(float hitPoints)
    {
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            if (DataManager.Instance != null)
                DataManager.Instance.PlayerHealth = currentHealth;

            Debug.Log("Game Over");
            GameManager.Instance.EndLevel();
        }
        else
        {
            currentHealth -= hitPoints;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                Debug.Log("Game Over");
                GameManager.Instance.EndLevel();
            }

            // Animation
            animator.HurtAninamation();

            // Save health to disk
            if (DataManager.Instance != null)
                DataManager.Instance.PlayerHealth = currentHealth;
        }

        if (DataManager.Instance != null)
            DataManager.Instance.Save();
    }
}
