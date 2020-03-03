using UnityEngine;

public class Jump : AbstractBehaviour
{
    [Header("Variables")]
    public float jumpSpeed = 200f;
    [SerializeField] float jumpDelay = 0.1f;
    [SerializeField] int jumpCount = 2;

    protected float lastJumpTime = 0f;
    protected int jumpRemaning = 0;

    [Header("FX")]
    [SerializeField] GameObject dustFx;
    
    protected virtual void Update()
    {
        bool canJump = inputState.GetButtonValue(inputButton[0]);
        float holdTime = inputState.GetButtonHoldTime(inputButton[0]);
        
        if (collState.standing)
        {
            //if (jumpRemaning == 0)
                jumpRemaning = jumpCount;

            if (canJump && holdTime < 0.125f)
            {
                OnJump();
                jumpRemaning--;
            }
        }
        else
        {
            if(canJump && holdTime < 0.125f && Time.time - lastJumpTime > jumpDelay)
            {
                if(jumpRemaning > 0)
                {
                    OnJump();
                    jumpRemaning--;

                    GameObject dust = Instantiate(dustFx);
                    dust.transform.SetParent(this.transform);
                    dust.transform.position = this.transform.position;
                }
            }
        }
    }

    protected virtual void OnJump ()
    {
        Vector2 vel = rbdy2D.velocity;
        lastJumpTime = Time.time;

        rbdy2D.velocity = new Vector2(vel.x, jumpSpeed);
    }
}
