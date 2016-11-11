//using UnityEngine;
//using System.Collections;
//using System;

public class BasicBullet : Bullet {

    new public void Start()
    {
        base.Start();
        if (damage == 0)
            damage = 10;
        if (speed == 0)
            speed = 20;
    }
}
