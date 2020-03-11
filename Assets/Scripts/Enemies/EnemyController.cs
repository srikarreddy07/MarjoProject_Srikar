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
    [SerializeField] AIStatus _aiCurrentStatus;
    [SerializeField] FaceDirection _faceDirection;
    [SerializeField] Vector3 lastTransformPos;

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

    [Header("SFX")]
    [SerializeField] GameObject sfxGameObject;

    void Start()
    {
        canMove = true;

        _aiCurrentStatus = AIStatus.Patrol;
        _faceDirection = FaceDirection.Right;

        targetTrans =  GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (targetTrans == null)
            return;

        GetFacing();
        
        switch (_aiCurrentStatus)
        {
            case AIStatus.Patrol:
                canMove = true;

                if(_faceDirection == FaceDirection.Left)
                    transform.position = Vector2.MoveTowards(transform.position, PatrolPoints[0].position, moveSpeed * Time.deltaTime);
                else
                    transform.position = Vector2.MoveTowards(transform.position, PatrolPoints[1].position, moveSpeed * Time.deltaTime);

                AnimationController.Instance.UpdateAnimation(true, false, 0);
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

                AnimationController.Instance.UpdateAnimation(true, false, 0);
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
                            AnimationController.Instance.UpdateAnimation(false, true, 0);
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
        if (_faceDirection == FaceDirection.Right)
            hit2D = Physics2D.Raycast(attackTrans.position, transform.right, rayDistance, playerLayer);
        else
            hit2D = Physics2D.Raycast(attackTrans.position, -transform.right, rayDistance, playerLayer);

        if (hit2D == true)
        {
            _aiCurrentStatus = AIStatus.Chase;
            Debug.Log("Found Player");
        }
        else
        {
            if(_faceDirection == FaceDirection.Right)
                Debug.DrawRay(attackTrans.position, transform.right * rayDistance, Color.blue);
            else
                Debug.DrawRay(attackTrans.position, -transform.right * rayDistance, Color.blue);

            _aiCurrentStatus = AIStatus.Patrol;
        }
    }

    void GetFacing ()
    {
        if (Vector2.Distance(transform.position, PatrolPoints[1].position) <= 1f)
        {
            _faceDirection = FaceDirection.Left;

            Vector3 scale = transform.localScale;
            scale.x = -1;
            transform.localScale = scale;
        }
        else if (Vector2.Distance(transform.position, PatrolPoints[0].position) <= 1f)
        {
            _faceDirection = FaceDirection.Right;

            Vector3 scale = transform.localScale;
            scale.x = 1;
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