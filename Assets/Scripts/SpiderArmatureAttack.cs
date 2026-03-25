using UnityEngine;

public class SpiderArmatureAttack : MonoBehaviour
{
    public SpiderAttack hitbox;

    public void StartAttack() => hitbox.StartAttack();
    public void EndAttack() => hitbox.EndAttack();
}
