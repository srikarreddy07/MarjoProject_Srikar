using UnityEngine;

public class EnemyPatrollerController : MonoBehaviour
{
    public enum FaceDirection
    {
        Left, Right
    }

    public enum AIStatus
    {
        Patrol, Attack
    }

    [Header("Variables")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] bool canMove = true;
    [SerializeField] AIStatus _aiCurrentStatus;
    [SerializeField] FaceDirection _faceDirection;
    [SerializeField] Vector3 lastTransformPos;

    [Header("Patrol")]
    [SerializeField] Transform[] PatrolPoints;
    [SerializeField] RaycastHit2D checkWalls;
    [SerializeField] LayerMask patrolLayer;

    [Header("Attack")]
    [SerializeField] float attackRadius = 1f;
    [SerializeField] float attackRate = 0.5f;
    [SerializeField] float nextAttack;
    [SerializeField] float targetDetectionRange = 0.75f;
    [SerializeField] RaycastHit2D hit2D;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform targetTrans;
    [SerializeField] Transform attackTrans;

    [Header("Animation")]
    [SerializeField] AnimationController enemyAnimator;

    [Header("SFX")]
    [SerializeField] GameObject sfxGameObject;

    void Start()
    {
        canMove = true;

        _aiCurrentStatus = AIStatus.Patrol;
        _faceDirection = FaceDirection.Right;

        targetTrans =  GameObject.FindGameObjectWithTag("Player").transform;
        enemyAnimator = GetComponent<AnimationController>();
    }

    void Update()
    {
        if (targetTrans == null)
            return;

        //GetFacing();
        
        switch (_aiCurrentStatus)
        {
            case AIStatus.Patrol:
                canMove = true;

                //if(_faceDirection == FaceDirection.Left)
                //    transform.position = Vector2.MoveTowards(transform.position, PatrolPoints[0].position, moveSpeed * Time.deltaTime);
                //else
                //    transform.position = Vector2.MoveTowards(transform.position, PatrolPoints[1].position, moveSpeed * Time.deltaTime);

                if(checkWalls.collider == true)
                {
                    if (_faceDirection == FaceDirection.Right)
                    {
                        transform.eulerAngles = new Vector3(0f, -180f, 0f);

                        _faceDirection = FaceDirection.Left;
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0f, 0f, 0f);

                        _faceDirection = FaceDirection.Right;
                    }
                }
                if(canMove)
                    transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

                lastTransformPos = transform.position;

                // Animation
                enemyAnimator.UpdateAnimation(true, false, 0);
                break;
            //case AIStatus.Chase:
            //    canMove = true;

            //    if (Vector2.Distance(transform.position, targetTrans.position) > 1.5f)
            //    {
            //        transform.position = Vector2.MoveTowards(transform.position, new Vector3(targetTrans.position.x, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);

            //        lastTransformPos = transform.position;
            //    }
            //    else
            //        _aiCurrentStatus = AIStatus.Attack;

            //    // Animation
            //    enemyAnimator.UpdateAnimation(true, false, 0);
            //    break;
            case AIStatus.Attack:
                // Attack //
                canMove = false;
                transform.position = lastTransformPos;

                Collider2D attackCollider = Physics2D.OverlapCircle(attackTrans.position, attackRadius, playerLayer);

                if (attackCollider != null)
                {
                    if (attackCollider.GetComponent<PlayerHealth>() != null)
                    {
                        if (Time.time > nextAttack)
                        {
                            //attackCollider.GetComponent<PlayerHealth>().TakeDamage(5f);
                            nextAttack = Time.time + attackRate;

                            // Animation
                            enemyAnimator.UpdateAnimation(false, true, 0);

                            // SFX
                            GameObject obj = Instantiate(sfxGameObject, attackTrans .position, Quaternion.identity);
                            transform.parent = obj.transform;
                        }
                    }
                }                
                
                // Sound

                break;
            default:
                transform.position = lastTransformPos;
                break;
        }
    }

    private void FixedUpdate()
    {
        // Raycast for walls
        checkWalls = Physics2D.Raycast(transform.position, transform.right, 1f, patrolLayer);


        // Raycast for Player
        hit2D = Physics2D.Raycast(attackTrans.position, transform.right, targetDetectionRange, playerLayer);

        if (hit2D == true)
        {
            _aiCurrentStatus = AIStatus.Attack;
            
        }
        else
        {
            //Debug.DrawRay(attackTrans.position, transform.right * rayDistance, Color.blue);

            _aiCurrentStatus = AIStatus.Patrol;
            Debug.DrawRay(transform.position, transform.right * targetDetectionRange, Color.blue);
        }
    }

    //void GetFacing ()
    //{
    //    if (Vector2.Distance(transform.position, PatrolPoints[1].position) <= 1f)
    //    {
    //        _faceDirection = FaceDirection.Left;

    //        Vector3 scale = transform.localScale;
    //        scale.x = -1;
    //        transform.localScale = scale;
    //    }
    //    else if (Vector2.Distance(transform.position, PatrolPoints[0].position) <= 1f)
    //    {
    //        _faceDirection = FaceDirection.Right;

    //        Vector3 scale = transform.localScale;
    //        scale.x = 1;
    //        transform.localScale = scale;
    //    }
    //}

    void GetFacing ()
    {
        if(_faceDirection == FaceDirection.Left)
        {
            Vector3 scale = transform.localScale;
            scale.x = -1f;
            transform.localScale = scale;
        }
        else
        {
            Vector3 scale = transform.localScale;
            scale.x = 1f;
            transform.localScale = scale;
        }
    }

    //bool IsTargetInRange ()
    //{
    //    if (Vector2.Distance(transform.position, targetTrans.position) < targetDetectionRange)
    //        return true;
    //    else
    //        return false;
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackTrans.position, attackRadius);
    }
}