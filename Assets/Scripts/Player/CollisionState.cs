using UnityEngine;

public class CollisionState : MonoBehaviour
{
    [Header("Variables")]
    public bool standing;
    [SerializeField] float collisionRadius = 10f;
    [SerializeField] Vector2 bottomPosition = Vector2.zero;
    [SerializeField] LayerMask collisionLayer;
    [SerializeField] Color debugColor = Color.red;
    
    void FixedUpdate()
    {
        var pos = bottomPosition;
        pos.x += transform.position.x;
        pos.y += transform.position.y;

        standing = Physics2D.OverlapCircle(pos, collisionRadius, collisionLayer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = debugColor;

        Vector2 position = bottomPosition;
        position.x += transform.position.x;
        position.y += transform.position.y;

        Gizmos.DrawWireSphere(position, collisionRadius);
    }
}
