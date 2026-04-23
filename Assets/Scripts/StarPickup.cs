using UnityEngine;

public class StarPickup : MonoBehaviour
{
[Header("Pickup Settings")]
    public AudioClip collectSound;
   // public GameObject healthParticle;

    [Header("Hover Effect Settings")]
    public float floatSpeed = 2f;     
    public float floatHeight = 0.5f;  
    public float rotationSpeed = 50f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newY = (Mathf.Sin(Time.time * floatSpeed) + 1.0f) * 0.5f * floatHeight;
        transform.position = startPosition + new Vector3(0, newY, 0);
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Shooting player = other.GetComponent<Shooting>();

            if (player != null)
            {
                player.CollectStar();
                SoundFXManager.instance.PlaySoundFXClip(collectSound, transform, 0.3f);
                Destroy(gameObject);
            }
        }
    }
}

