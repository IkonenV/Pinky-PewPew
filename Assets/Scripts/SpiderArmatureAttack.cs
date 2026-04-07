using UnityEngine;

public class SpiderArmatureAttack : MonoBehaviour
{
    public SpiderAttack hitbox;
    public BasicSpider basicSpider;

    public void StartAttack() => hitbox.StartAttack();
    public void EndAttack() => hitbox.EndAttack();

    public void StartStun() => basicSpider.StopMoving();
    public void EndStun() => basicSpider.StartMoving();
}
