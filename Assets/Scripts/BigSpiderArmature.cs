using UnityEngine;

public class BigSpiderArmature : MonoBehaviour
{
    public BigSpiderAttack hitbox;
    public BigSpider bigSpider;

    public void StartAttack() => hitbox.StartAttack();
    public void EndAttack() => hitbox.EndAttack();
    public void StartAttackSound() => hitbox.StartAttackSound();

// laita animaatioon kun saat bigspider hurt animaation
    public void StartStun() => bigSpider.StopMoving();
    public void EndStun() => bigSpider.StartMoving();
}

