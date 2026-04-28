using System.Collections;
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
    public float invisibleBeforeSpawn;
    MeshRenderer meshRenderer;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        //meshRenderer.enabled = false;
        //StartCoroutine(InvisibleTime());
    }


    void Update()
    {
        float newY = (Mathf.Sin(Time.time * floatSpeed) + 1.0f) * 0.5f * floatHeight;
        transform.position = startPosition + new Vector3(0, newY, 0);
        
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerScore playerScore = other.GetComponent<PlayerScore>();
            playerScore.GetScore(scoreFrom);
            SoundFXManager.instance.PlayRandomSoundFXClip(scoreSoundClips, transform, 0.2f);
            Destroy(gameObject);
        }
    }
    public IEnumerator InvisibleTime()
    {
        yield return new WaitForSeconds(invisibleBeforeSpawn);
        meshRenderer.enabled = true;
    }
}
