using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
    void Start()
    {
        DestroyTime();
    }
    void DestroyTime()
    {
        Destroy(gameObject, 5f);
    }
}
