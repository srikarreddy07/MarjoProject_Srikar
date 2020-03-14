using UnityEngine;

public class EnemyPatrollerController : MonoBehaviour
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
    [SerializeField] AIStatus _aiCurrentStatus;
    [SerializeField] FaceDirection _faceDirection;
    [SerializeField] Vector3 lastTransformPos;

    [Header("Patrol")]
    [SerializeField] Transform[] PatrolPoints;
    [SerializeField] RaycastHit2D checkWalls;
    [SerializeField] LayerMask patrolLayer;

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
                    Debug.Log("Hit Collider: " + checkWalls.transform.name);

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

                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
                // Animation
                enemyAnimator.UpdateAnimation(true, false, 0);
                break;
            case AIStatus.Chase:
                canMove = true;

                if (Vector2.Distance(transform.position, targetTrans.position) > 1.5f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector3(targetTrans.position.x, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);

                    lastTransformPos = transform.position;
                }
                else
                    _aiCurrentStatus = AIStatus.Attack;

                // Animation
                enemyAnimator.UpdateAnimation(true, false, 0);
                break;
            case AIStatus.Attack:
                // Attack //
                canMove = false;
                transform.position = lastTransformPos;

                Collider2D attackCollider = Physics2D.OverlapBox(attackTrans.position, attackTrans.localScale, 0f, playerLayer);

                if (attackCollider != null)
                {
                    if (attackCollider.GetComponent<PlayerHealth>() != null)
                    {
                        if (Time.time > nextAttack)
                        {
                            attackCollider.GetComponent<PlayerHealth>().TakeDamage(5f);
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

        Debug.DrawRay(transform.position, transform.right * 1f, Color.blue);

        // Raycast for Player
        hit2D = Physics2D.Raycast(attackTrans.position, transform.right, rayDistance, playerLayer);

        if (hit2D == true)
        {
            _aiCurrentStatus = AIStatus.Chase;
            Debug.Log("Found Player");
        }
        else
        {
            //Debug.DrawRay(attackTrans.position, transform.right * rayDistance, Color.blue);

            _aiCurrentStatus = AIStatus.Patrol;
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
        
    bool IsTargetInRange ()
    {
        if (Vector2.Distance(transform.position, targetTrans.position) < rayDistance)
            return true;
        else
            return false;
    }
}