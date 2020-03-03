using UnityEngine;

public class DustEffect : MonoBehaviour
{
    [SerializeField] float lifeTime = 2f;

    private void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }
}
