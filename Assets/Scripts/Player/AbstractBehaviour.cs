using UnityEngine;

public abstract class AbstractBehaviour : MonoBehaviour
{
    public Buttons[] inputButton;

    protected InputState inputState;
    protected Rigidbody2D rbdy2D;
    protected CollisionState collState;
    protected Animator animator;

    protected virtual void Awake ()
    {
        inputState = GetComponent<InputState>();
        rbdy2D = GetComponent<Rigidbody2D>();
        collState = GetComponent<CollisionState>();
        animator = GetComponent<Animator>();
    }
}
