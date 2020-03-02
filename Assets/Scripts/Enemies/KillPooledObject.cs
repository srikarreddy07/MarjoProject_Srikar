using UnityEngine;

public class KillPooledObject : MonoBehaviour
{
    [SerializeField] float lifeTime = 5f;

    private void OnEnable()
    {
        CancelInvoke();
        Invoke("Die", lifeTime);
    }

    void Die ()
    {
        CancelInvoke();
        this.gameObject.SetActive(false);
    }
}
