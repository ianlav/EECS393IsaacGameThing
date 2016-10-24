using UnityEngine;
using System.Collections;
using System;

public class StartGun : Weapon {

    public Transform bulletOrigin;
    public Bullet bullet;
    public PlayerMovement player;

    //basic implementation. fires a straight line bullet towards the mouse
    public override void fire()
    {
        if (timeSinceFired > timeBetweenShots)
        {
            timeSinceFired = 0;
        
            bullet.direction = getAimVector(bulletOrigin.position);
            bullet.damage = damage;
            Instantiate(bullet, bulletOrigin.transform.position, player.transform.rotation);
        }
    }

    void Update()
    {
        timeSinceFired += Time.deltaTime;
    }
}
