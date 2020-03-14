using UnityEngine;
using LevelManagement.Data;

public class PlayerHealth : AbstractBehaviour
{
    [Header("Health")]
    [SerializeField] float currentHealth;

    [Header("Components")]
    [SerializeField] SpriteRenderer spr;

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
    }

    public void TakeDamage(float hitPoints)
    {
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            if (DataManager.Instance != null)
                DataManager.Instance.PlayerHealth = currentHealth;

            Debug.Log("Game Over");
            GameManager.instance.EndLevel();
        }
        else
        {
            currentHealth -= hitPoints;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                Debug.Log("Game Over");
                GameManager.instance.EndLevel();

                DataManager.Instance.PlayerHealth = 100;
            }

            // Animation
            animator.SetTrigger("Hurt");

            // Save health to disk
            if (DataManager.Instance != null)
                DataManager.Instance.PlayerHealth = currentHealth;
        }
        if (DataManager.Instance != null)
            DataManager.Instance.Save();
    }
}
