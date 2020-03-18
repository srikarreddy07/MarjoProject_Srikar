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
    [SerializeField] float attackRadius = 2f;
    [SerializeField] Vector3 raycastDirection;
    [SerializeField] RaycastHit2D hit2D;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform targetPlayer;

    [Header("Attack")]
    [SerializeField] bool isAtacking;
    [SerializeField] float nextAttack;
    [SerializeField] float attackRate = 0.5f;
    [SerializeField] Transform attackTrans;

    [Header("Animator")]
    [SerializeField] AnimationController animtorController;

    private void Awake()
    {
        animtorController = GetComponent<AnimationController>();
        canMove = true;
    }

    private void Start()
    {
        if (direction == Direction.Left)
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
        else
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }

    private void Update()
    {
        if (!canMove)
            return;

        if(direction == Direction.Left)
            transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
        else
            transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);

        if (this.gameObject.GetComponent<Renderer>().isVisible == false)
            this.gameObject.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (direction == Direction.Left)
        {
            hit2D = Physics2D.Raycast(transform.position, -raycastDirection, attackRadius, playerLayer);

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
                Debug.DrawRay(transform.position, -raycastDirection * attackRadius, Color.blue);
            }
        }
        else
        {
            hit2D = Physics2D.Raycast(transform.position, raycastDirection, attackRadius, playerLayer);

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
                Debug.DrawRay(transform.position, raycastDirection * attackRadius, Color.blue);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackTrans.position, attackRadius);
    }
}
