using UnityEngine;

public class PlayerMovement : AbstractBehaviour
{
    [Header("Variables")]
    public bool isRunning;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float runMultipler = 2f;

    [Header("Rope")]
    [SerializeField] bool slideTheRope = false;
    [SerializeField] float slideForece;
    [SerializeField] float slideCheckDistance;
    [SerializeField] Transform ropeCheckTrans;
    [SerializeField] LayerMask ropeLayer;
    private RaycastHit2D ropeCheckhit2D;


    [Header("Particles")]
    [SerializeField] ParticleSystem runTrail;

    void Update()
    {
        isRunning = false;

        var left = inputState.GetButtonValue(inputButton[0]);
        var right = inputState.GetButtonValue(inputButton[1]);
        var run = inputState.GetButtonValue(inputButton[2]);

        float tempSpeed = 0f;

        if (slideTheRope == false)
        {
            if (left || right)
            {
                tempSpeed = moveSpeed;

                if (run && runMultipler > 0f)
                {
                    isRunning = true;
                    tempSpeed *= runMultipler;
                }

                float velX = tempSpeed * (float)inputState.direction;

                rbdy2D.velocity = new Vector2(velX, rbdy2D.velocity.y);

                runTrail.Play();
            }
            runTrail.Stop();
        }

        // Rope Slide
        if (ropeCheckhit2D)
        {
            slideTheRope = true;
            StartRopeSlide();
        }
        else
        {
            slideTheRope = false;
            rbdy2D.gravityScale = 2f;
        }
    }

    private void FixedUpdate()
    {
        ropeCheckhit2D = Physics2D.CircleCast(ropeCheckTrans.position, slideCheckDistance, Vector2.right, 1f, ropeLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ropeCheckTrans.position, slideCheckDistance);
    }

    private void StartRopeSlide ()
    {
        if (slideTheRope == false)
            return;
        else
        {
            if (inputState.direction == Direction.Left)
            {
                rbdy2D.AddForce(Vector2.left * slideForece * Time.fixedDeltaTime);
                rbdy2D.gravityScale = 0.75f;
            }
            else if (inputState.direction == Direction.Right)
            {
                rbdy2D.AddForce(Vector2.right * slideForece * Time.fixedDeltaTime);
                rbdy2D.gravityScale = 0.75f;
            }
        }
    }
}
