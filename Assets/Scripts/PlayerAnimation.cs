using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
public Shooting shooting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AnimationShoot()
    {
        shooting.Fire();
    }
    public void AnimationChargeShoot()
    {
        shooting.ChargedFire();
    }
}
