using UnityEngine;

public class UpdateAnimations : AbstractBehaviour
{
    [Header("Animation")]
    [SerializeField] float animatorSpeedMultiplier = 2f;

    void Update()
    {
        var left = inputState.GetButtonValue(inputButton[0]);
        var right = inputState.GetButtonValue(inputButton[1]);
        var run = inputState.GetButtonValue(inputButton[2]);

        if (left || right)
        {
            animator.SetBool("IsWalking", true);

            if (run)
                animator.speed = animatorSpeedMultiplier;
            else
                animator.speed = 1f;
        }
        else
            animator.SetBool("IsWalking", false);

        if (Input.GetKeyDown(KeyCode.Space))
            animator.SetTrigger("Jump");

        if (Input.GetKeyDown(KeyCode.J))
            animator.SetTrigger("Kick");

        if (Input.GetKeyDown(KeyCode.K))
            animator.SetTrigger("KickB");
    }
}
