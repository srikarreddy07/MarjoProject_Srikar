using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float currentHealth;

    [Header("Components")]
    [SerializeField] SpriteRenderer spr;

    private void Start()
    {
        currentHealth = 100f;

        spr = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float hitPoints)
    {
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Game Over");            
        }
        else
        {
            currentHealth -= hitPoints;

            UIManager.instance.SetHealthUI(currentHealth);

            spr.color = new Color(1f, 0f, 0f);
            Invoke("ResetColor", 0.5f);

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                Debug.Log("Game Over");

                //if(GameObject.Find("Level Manager").GetComponent<LevelManager>())
                    LevelManager.instance.LoadStartScence();
            }
        }
    }

    void ResetColor ()
    {
        CancelInvoke();
        spr.color = new Color(1f, 1f, 1f);
    }
}
