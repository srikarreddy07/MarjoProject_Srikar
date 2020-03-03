using UnityEngine;

public class EAttackerController : MonoBehaviour
{
    public enum Direction
    {
        Left, Right
    }

    [Header("Variables")]
    [SerializeField] bool canMove = true;
    [SerializeField] float speed = 1.5f;
    public Direction direction;

    [Header("Raycast")]
    [SerializeField] float lookDistance = 2f;
    [SerializeField] RaycastHit2D hit2D;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform targetPlayer;

    [Header("Attack")]
    [SerializeField] float nextAttack;
    [SerializeField] float attackRate = 0.5f;
    [SerializeField] Transform attackTrans;

    private void Awake()
    {
        canMove = true;
    }

    private void Update()
    {
        if (!canMove)
            return;

        if(direction == Direction.Left)
            transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
        else
            transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
    }

    private void FixedUpdate()
    {
        if (direction == Direction.Left)
        {
            hit2D = Physics2D.Raycast(transform.position, -transform.right, lookDistance, playerLayer);

            if (hit2D)
            {
                canMove = false;

                targetPlayer = hit2D.transform;
                Attack(targetPlayer);
            }
            else
            {
                canMove = true;

                targetPlayer = null;
                Debug.DrawRay(transform.position, -transform.right * lookDistance, Color.blue);
            }
        }
        else
        {
            hit2D = Physics2D.Raycast(transform.position, transform.right, lookDistance, playerLayer);

            if (hit2D)
            {
                canMove = false;

                targetPlayer = hit2D.transform;
                Attack(targetPlayer);
            }
            else
            {
                canMove = true;

                targetPlayer = null;
                Debug.DrawRay(transform.position, transform.right * lookDistance, Color.blue);
            }
        }
    }

    void Attack (Transform target)
    {
        Collider2D attackCollider = Physics2D.OverlapBox(attackTrans.position, attackTrans.localScale, 0f, playerLayer);

        if (attackCollider != null)
        {
            if (attackCollider.GetComponent<PlayerHealth>() != null)
            {
                if (Time.time > nextAttack)
                {
                    attackCollider.GetComponent<PlayerHealth>().TakeDamage(0.25f);
                    nextAttack = Time.time + attackRate;
                }
            }
        }
    }
}
