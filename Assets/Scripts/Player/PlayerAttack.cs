﻿using UnityEngine;

public class PlayerAttack : AbstractBehaviour
{
    [Header("Attack")]
    [SerializeField] float attackRate = 0.125f;
    [SerializeField] float nextAttack;
    [SerializeField] float attackRadius = 0.4f;
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

        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackTrans.position, attackRadius, enemyLayer);

        if (enemies.Length > 0)
        {
            if (kick)
            {
                if (Time.time > nextAttack && punchHoldTime < 0.125f)
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        enemies[i].GetComponent<EnemyHealth>().TakeDamage(10f);
                        nextAttack = Time.time + attackRate;
                    }
                }
            }

            if (KickB)
            {
                if (Time.time > nextAttack && KickBHoldTime < 0.125f)
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        enemies[i].GetComponent<EnemyHealth>().TakeDamage(10f);
                        nextAttack = Time.time + attackRate;
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
