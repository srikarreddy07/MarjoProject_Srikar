using UnityEngine;

public class PlayerAttack : AbstractBehaviour
{
    [Header("Attack")]
    [SerializeField] float attackRate = 0.125f;
    [SerializeField] float nextAttack;
    [SerializeField] float attackRadius = 0.7f;
    [SerializeField] Transform attackTrans;
    [SerializeField] LayerMask enemyLayer;

    [Header("Gizmos")]
    [SerializeField] Color attackColor = Color.magenta;

    private void FixedUpdate()
    {
        var kick = inputState.GetButtonValue(inputButton[0]);
        var punchHoldTime = inputState.GetButtonHoldTime(inputButton[0]);
        var KickB = inputState.GetButtonValue(inputButton[0]);
        var KickBHoldTime = inputState.GetButtonHoldTime(inputButton[0]);

        if (kick)
        {
            if (Time.time > nextAttack && punchHoldTime < 0.0125f)
            {
                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackTrans.position, attackRadius, enemyLayer);

                if (enemies.Length > 0)
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        enemies[i].GetComponent<EnemyHealth>().EnemyTakeDamage(2f);
                        nextAttack = Time.time + attackRate;

                        Debug.Log("Hit " + enemies[i].transform.name);
                    }
                    animator.SetTrigger("Kick");
                }
            }

            if (KickB)
            {
                if (Time.time > nextAttack && punchHoldTime < 0.0125f)
                {
                    Collider2D[] KickBenemies = Physics2D.OverlapCircleAll(attackTrans.position, attackRadius, enemyLayer);

                    if (KickBenemies.Length > 0)
                    {
                        for (int i = 0; i < KickBenemies.Length; i++)
                        {
                            KickBenemies[i].GetComponent<EnemyHealth>().EnemyTakeDamage(4f);
                            nextAttack = Time.time + attackRate;

                            Debug.Log("Hit " + KickBenemies[i].transform.name);
                        }
                        animator.SetTrigger("KickB");
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = attackColor;
        Gizmos.DrawWireSphere(attackTrans.position, attackRadius);
    }
}
