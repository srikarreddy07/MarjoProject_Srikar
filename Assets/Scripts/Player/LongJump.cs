using UnityEngine;

public class LongJump : Jump
{
    [Header("Variables")]
    [SerializeField] bool canLongJump;
    [SerializeField] bool isLongJumping;
    [SerializeField] float longJumpDelay = 0.15f;
    [SerializeField] float longMultipler = 1.5f;

    protected override void Update()
    {
        bool canJump = inputState.GetButtonValue(inputButton[0]);
        float holdTime = inputState.GetButtonHoldTime(inputButton[0]);

        if (!canJump)
            canLongJump = false;

        if (collState.standing && isLongJumping)
            isLongJumping = false;

        base.Update();

        if(canLongJump && !collState.standing && holdTime > longJumpDelay)
        {
            Vector2 rb = rbdy2D.velocity;

            rbdy2D.velocity = new Vector2(0f, jumpSpeed * longMultipler);

            canLongJump = false;
            isLongJumping = true;
        }
    }

    protected override void OnJump()
    {
        base.OnJump();
        canLongJump = true;
    }
}
