using UnityEngine;

public class BigSpiderArmature : MonoBehaviour
{
    public BigSpiderAttack hitbox;
    public BigSpider bigSpider;
    public GameObject slash;
    public Transform firePoint;

    public void StartAttack() => hitbox.StartAttack();
    public void EndAttack() => hitbox.EndAttack();
    public void StartAttackSound() => hitbox.StartAttackSound();

    public void StartStun() => bigSpider.StopMoving();
    public void EndStun() => bigSpider.StartMoving();
    public void InstantiateSlash()
    {
        Instantiate(slash, firePoint);
    }
}

