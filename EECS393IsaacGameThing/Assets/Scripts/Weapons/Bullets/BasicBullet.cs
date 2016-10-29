using UnityEngine;
using System.Collections;
using System;

public class BasicBullet : Bullet {
    //default hit function. Just subtracts damage
    public override void hit(Enemy other)
    {
        other.hp -= damage;
    }
}
