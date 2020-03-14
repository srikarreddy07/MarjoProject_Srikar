using UnityEngine;

public class DestoryGameObject : MonoBehaviour
{
    public float lifeTime = 2f;

    private void Awake()
    {
        Destroy(this.gameObject, lifeTime);
    }
}
