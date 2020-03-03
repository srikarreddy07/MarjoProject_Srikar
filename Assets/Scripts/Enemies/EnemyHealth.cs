using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float health = 100f;

    [Header("Attack")]
    [SerializeField] float attackRadius;
    [SerializeField] Transform attackTrans;

    [Header("Components")]
    [SerializeField] SpriteRenderer spr;

    [Header("Gizmos")]
    [SerializeField] Color attackColor = Color.cyan;

    void Start()
    {
        health = 100f;
        spr = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float hitPoints)
    {
        if (health <= 0)
        {
            health = 0;

            GameManager.instance.SetKillCount();
            UIManager.instance.SetKillCountText();

            PlayerPrefs.SetInt("Kills", GameManager.instance.killCount);

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

                GameManager.instance.SetKillCount();
                UIManager.instance.SetKillCountText();

                PlayerPrefs.SetInt("Kills", GameManager.instance.killCount);

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

    private void OnDrawGizmos()
    {
        Gizmos.color = attackColor;
        Gizmos.DrawWireCube(attackTrans.position, attackTrans.localScale);
    }
}
