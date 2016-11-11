using UnityEngine;
using System.Collections;
using System;

public class StartGun : Weapon {

    //basic implementation. fires a straight line bullet towards the mouse
    public override void fire()
    {
        if (timeSinceFired > timeBetweenShots) //controls the fire rate
        {
            timeSinceFired = 0;
        
            //makes a bullet in the direction of the mouse, at the specified speed
            bullets[0].direction = aimVector;
            bullets[0].damage = damage;
            Instantiate(bullets[0], bulletOrigin.transform.position, player.transform.rotation);
        }
    }
}
