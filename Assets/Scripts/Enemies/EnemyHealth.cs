using UnityEngine;
using LevelManagement.Data;

public class EnemyHealth : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float health = 100f;

    [Header("Components")]
    [SerializeField] SpriteRenderer spr;

    [Header("Gizmos")]
    [SerializeField] Color attackColor = Color.cyan;

    void Start()
    {
        health = 100f;
        spr = GetComponent<SpriteRenderer>();
    }

    public void EnemyTakeDamage(float hitPoints)
    {
        if (health <= 0)
        {
            health = 0;

            DataManager.Instance.PlayerKillCount++;

            this.gameObject.SetActive(false);
        }
        else
        {
            health -= hitPoints;

            spr.color = new Color(1f, 0f, 0f);
            Invoke("ResetColor", 0.5f);

            if (health <= 0)
            {
                health = 0;

                DataManager.Instance.PlayerKillCount++;

                this.gameObject.SetActive(false);
            }
        }

        // Animation
        // Sound
    }

    void ResetColor()
    {
        CancelInvoke();
        spr.color = new Color(1f, 1f, 1f);
    }
}
