using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator anim;

    private void Awake ()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        UpdateAnimation(true, false, 0);
    }

    public void UpdateAnimation (bool Walk, bool Attack, int Hurt)
    {
        anim.SetBool("IsWalking", Walk);
        anim.SetBool("IsAttacking", Attack);
        anim.SetTrigger(Hurt);
    }
}
