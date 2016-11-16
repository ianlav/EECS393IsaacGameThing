using UnityEngine;
using System.Collections;

public class PersistentBullet : Bullet {

    new void Start()
    {
        base.Start();
        damage = 10;
        speed = 20;
        effect = "Fear";
    }

    //default enemy hit: damage enemy
    public override void hit(Enemy enemy)
    {
        print("here");
        enemy.applyEffect(effect);
        enemy.takeDamage(damage);
    }

    public override void hit(PlayerMovement player)
    {
        base.hit(player);
    }


    //default platform hit: just destroy bullet
    public override void hit(Platform platform)
    {
    }
}
