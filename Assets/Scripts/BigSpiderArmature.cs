using UnityEngine;

public class BigSpiderArmature : MonoBehaviour
{
    public BigSpiderAttack hitbox;

    public void StartAttack() => hitbox.StartAttack();
    public void EndAttack() => hitbox.EndAttack();
    public void StartAttackSound() => hitbox.StartAttackSound();
}

