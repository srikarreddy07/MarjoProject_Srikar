using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator anim;

    private static AnimationController instance;
    public static AnimationController Instance { get => instance; }

    private void Awake ()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        AnimationController.Instance.UpdateAnimation(true, false, 0);
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    public void UpdateAnimation (bool Walk, bool Attack, int Hurt)
    {
        anim.SetBool("IsWalking", Walk);
        anim.SetBool("IsAttacking", Attack);
        anim.SetTrigger(Hurt);
    }
}
