using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth = 100f;
    public float maxHealth = 100f;
    public Slider healthSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthSlider = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float takenAmount)
    {
        currentHealth -= takenAmount;
        healthSlider.value = currentHealth / 100;
        if(currentHealth <= 0)
        {
            Debug.Log("Kuolit");
        }
    }
    public void GetHealth(float gottenAmount)
    {
        currentHealth += gottenAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthSlider.value = currentHealth / 100;
        
    }
}
