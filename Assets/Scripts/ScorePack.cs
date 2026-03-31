using UnityEngine;

public class ScorePack : MonoBehaviour
{
    [Header("Pickup Settings")]
    public float scoreFrom = 1;
    public AudioClip[] scoreSoundClips;
    //public GameObject collectParticle;

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
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = startPosition + new Vector3(0, newY, 0);
        
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerScore playerScore = other.GetComponent<PlayerScore>();
            playerScore.GetScore(scoreFrom);
            SoundFXManager.instance.PlayRandomSoundFXClip(scoreSoundClips, transform, 0.3f);
            Destroy(gameObject);
        }
    }
}
