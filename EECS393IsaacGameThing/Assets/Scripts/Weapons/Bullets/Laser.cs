//using UnityEngine;
//using System.Collections;
//using System;

public class Laser : Bullet {

    new public void Start()
    {
        base.Start();
        speed = 50;
        damage = 25;
    }

    public override void hit(PlayerMovement player)
    {
        base.hit(player);
    }


    //default platform hit: just destroy bullet
    public override void hit(Platform platform)
    {
    }

    public override void hit(Bullet bul)
    {
        Destroy(bul.gameObject);
    }

    public override void hit(Enemy enemy)
    {
        enemy.takeDamage(damage);
    }
}
