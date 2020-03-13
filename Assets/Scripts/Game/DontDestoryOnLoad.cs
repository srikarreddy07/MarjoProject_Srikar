using UnityEngine;

public class DontDestoryOnLoad : MonoBehaviour
{
    private void Awake()
    {
        transform.SetParent(null);
        Object.DontDestroyOnLoad(gameObject);
    }
}
