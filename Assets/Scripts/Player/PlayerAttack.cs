using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] float attackRate = 0.125f;
    [SerializeField] float nextAttack;
    [SerializeField] float attackRadius = 0.75f;
    [SerializeField] Transform attackTrans;
    [SerializeField] LayerMask enemyLayer;

    [Header("Gizmos")]
    [SerializeField] Color attackColor = Color.magenta;

    private void FixedUpdate()
    {
    //    var kick = inputState.GetButtonValue(inputButton[0]);
    //    var punchHoldTime = inputState.GetButtonHoldTime(inputButton[0]);
    //    var KickB = inputState.GetButtonValue(inputButton[0]);
    //    var KickBHoldTime = inputState.GetButtonHoldTime(inputButton[0]);

    //    if (kick)
    //    {
    //        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackTrans.position, attackRadius, enemyLayer);

    //        if (enemies.Length > 0)
    //        {
    //            for (int i = 0; i < enemies.Length; i++)
    //            {
    //                Debug.Log(enemies[i].transform.name);

    //                if (Time.time > nextAttack && punchHoldTime < 0.125f)
    //                {
    //                    for (int j = 0; i < enemies.Length; j++)
    //                    {
    //                        enemies[j].GetComponent<EnemyHealth>().EnemyTakeDamage(10f);
    //                        nextAttack = Time.time + attackRate;

    //                        Debug.Log("Hit " + enemies[j].transform.name);
    //                    }
    //                }

    //            }

    //            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackTrans.position, attackRadius, enemyLayer);

    //            if (enemies.Length > 0)
    //            {
    //                for (int i = 0; i < enemies.Length; i++)
    //                {
    //                    Debug.Log(enemies[i].transform.name);
    //                }

    //                if (kick)
    //                {
    //                    if (Time.time > nextAttack && punchHoldTime < 0.125f)
    //                    {
    //                        for (int i = 0; i < enemies.Length; i++)
    //                        {
    //                            enemies[i].GetComponent<EnemyHealth>().TakeDamage(10f);
    //                            nextAttack = Time.time + attackRate;

    //                            Debug.Log("Hit " + enemies[i].transform.name);
    //                        }
    //                    }
    //                }

    //                if (KickB)
    //                {
    //                    if (Time.time > nextAttack && KickBHoldTime < 0.125f)
    //                    {
    //                        for (int i = 0; i < enemies.Length; i++)
    //                        {
    //                            enemies[i].GetComponent<EnemyHealth>().TakeDamage(10f);
    //                            nextAttack = Time.time + attackRate;


    //                            Debug.Log("Hit " + enemies[i].transform.name);
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = attackColor;
    //    Gizmos.DrawWireSphere(attackTrans.position, attackRadius);
    }
}
