using UnityEngine;

public class RangedArmature : MonoBehaviour
{
    public RangedEnemy rangedEnemy;

    public void Shoot() => rangedEnemy.FireProjectile();
    public void StartStun() => rangedEnemy.StopMoving();
    public void EndStun() => rangedEnemy.StartMoving();
}
