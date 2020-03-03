using UnityEngine;

public class CrateManager : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
            Debug.Log("Collision working");

        if (collision.gameObject.layer == playerLayer)
            Debug.Log("Player and Layer Detected");
    }
}
