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
        if (timeSinceFired > timeBetweenShots) //controls the fire rate
        {
            timeSinceFired = 0;
        
            //makes a bullet in the direction of the mouse, at the specified speed
            bullet.direction = getAimVector(bulletOrigin.position);
            bullet.damage = damage;
            Instantiate(bullet, bulletOrigin.transform.position, player.transform.rotation);
        }
    }
}
