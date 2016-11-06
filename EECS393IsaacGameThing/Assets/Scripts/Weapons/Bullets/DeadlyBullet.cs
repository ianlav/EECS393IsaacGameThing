﻿using UnityEngine;
using System.Collections;

public class DeadlyBullet : Bullet {

    new void Start()
    {
        base.Start();
        damage = 100;
        speed = 25;
    }

    public override void hit(Enemy enemy)
    {
        Destroy(enemy.gameObject);
        Destroy(gameObject);
    }
}