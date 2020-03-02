using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum FaceDirection
    {
        Left, Right
    }

    public enum AIStatus
    {
        Patrol, Chase, Attack
    }

    [Header("Variables")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] bool canMove = true;
    [SerializeField] AIStatus aiStatus;
    [SerializeField] FaceDirection faceDirection;

    [Header("Patrol")]
    [SerializeField] Transform[] PatrolPoints;

    [Header("Raycast")]
    [SerializeField] float rayDistance = 2f;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] RaycastHit2D hit2D;
    [SerializeField] Transform targetTrans;

    [Header("Attack")]
    [SerializeField] float attackRate = 0.5f;
    [SerializeField] float nextAttack;
    [SerializeField] float targetDetectionRange = 5f;
    [SerializeField] Transform attackTrans;

    void Start()
    {
        canMove = true;

        aiStatus = AIStatus.Patrol;
        faceDirection = FaceDirection.Right;

        targetTrans =  GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (targetTrans == null)
            return;

        GetFacing();

        if (Vector2.Distance(transform.position, targetTrans.position) < targetDetectionRange)
            aiStatus = AIStatus.Chase;
        else
            aiStatus = AIStatus.Patrol;

        if (!canMove)
            return;

        switch (aiStatus)
        {
            case AIStatus.Patrol:
                if(faceDirection == FaceDirection.Left)
                {
                    transform.position = Vector2.MoveTowards(transform.position, PatrolPoints[0].position, moveSpeed * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, PatrolPoints[1].position, moveSpeed * Time.deltaTime);
                }
                break;
            case AIStatus.Chase:
                if(canMove)
                    transform.position = Vector2.MoveTowards(transform.position, new Vector3(targetTrans.position.x, transform.position.y, transform.position.z), moveSpeed * 0.25f * Time.deltaTime);
                break;
            case AIStatus.Attack:
                break;
            default:
                transform.position = transform.position;
                break;
        }
    }

    private void FixedUpdate()
    {
        if(IsTargetInRange())
        {
            if (faceDirection == FaceDirection.Right)
                hit2D = Physics2D.Raycast(transform.position, transform.right, rayDistance, playerLayer);
            else
                hit2D = Physics2D.Raycast(transform.position, -transform.right, rayDistance, playerLayer);

            if (hit2D)
            {
                canMove = false;

                Attack();
            }
            else
            {
                canMove = true;
                Debug.DrawRay(transform.position, transform.right * rayDistance, Color.blue);
            }
        }
        else
            canMove = true;
    }

    void GetFacing ()
    {
        if (Vector2.Distance(transform.position, PatrolPoints[1].position) < 0.1f)
        {
            faceDirection = FaceDirection.Left;

            Vector3 scale = transform.localScale;
            scale.x = -1;
            transform.localScale = scale;
        }
        else if (Vector2.Distance(transform.position, PatrolPoints[0].position) < 0.1f)
        {
            faceDirection = FaceDirection.Right;

            Vector3 scale = transform.localScale;
            scale.x = 1;
            transform.localScale = scale;
        }
    }

    void Attack ()
    {
        if(IsTargetInRange())
        {
            Collider2D attackCollider = Physics2D.OverlapBox(attackTrans.position, attackTrans.localScale, 0f, playerLayer);

            if (attackCollider != null)
            {
                if(attackCollider.GetComponent<PlayerHealth>() != null)
                {
                    if(Time.time > nextAttack)
                    {
                        attackCollider.GetComponent<PlayerHealth>().TakeDamage(5f);
                        nextAttack = Time.time + attackRate;
                    }
                }
            }
        }
        // Animation
        // Sound
    }
    
    bool IsTargetInRange ()
    {
        if (Vector2.Distance(transform.position, targetTrans.position) < targetDetectionRange)
            return true;
        else
            return false;
    }
}