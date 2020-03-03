using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] InputState inputState;
    [SerializeField] Rigidbody2D rbdy2D;
    [SerializeField] Animator anim;
    [SerializeField] CollisionState collState;

    void Start()
    {
        anim = GetComponentInParent<Animator>();

        inputState = gameObject.GetComponentInParent<InputState>();
        rbdy2D = gameObject.GetComponentInParent<Rigidbody2D>();
        collState = gameObject.GetComponentInParent<CollisionState>();
    }

    void Update()
    {
        if (collState.standing)
            ChangeAnimationState(0); //Idle

        if (inputState.absVelX > 0.1f)
        {
            if (gameObject.GetComponentInParent<PlayerMovement>().isRunning)
                ChangeAnimationState(2); //Run
            else
                ChangeAnimationState(1); //Walk
        }
        
        if(inputState.absVelY > 0.1f)
            ChangeAnimationState(2); //Jump/Fall

        // Attack
        // TaktHit
        // Death

        // Bat Idle
        // Bat Walk
        // Bat Run
        // Bat Jump/Fall
        // Bat Attack
        // Bat TakeHit
        // Bat Death
    }

    void ChangeAnimationState (int value)
    {
        anim.SetInteger("AnimState", value);
    }
}
