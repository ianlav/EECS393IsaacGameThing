using UnityEngine;
using System.Collections;

public class DeadlyBullet : Bullet {

    new void Start()
    {
        base.Start();
        damage = 100;
        speed = 25;
    }

    public override void hit(CharacterModel enemy)
    {
        enemy.takeDamage(100);
        Destroy(enemy.gameObject);
        Destroy(gameObject);
    }
}
