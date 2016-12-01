using UnityEngine;
using System.Collections;

public class DeadlyBullet : Bullet {

    new void Start()
    {
        base.Start();
        damage = 200;
        speed = 25;
    }

    public override void hit(Enemy enemy)
    {
        enemy.takeDamage(100);
        Destroy(enemy.gameObject);
    }

    public override void hit(PlayerMovement enemy)
    {
        enemy.takeDamage(100);
        Destroy(enemy.gameObject);
    }

    public override void hit(Platform platform)
    {
    }
}
