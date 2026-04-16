using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    private float timeLeft;

public void Awake()
    {
        ParticleSystem Psystem = GetComponent<ParticleSystem>();
        timeLeft = Psystem.main.startLifetime.constant;
    }
    public void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
}

