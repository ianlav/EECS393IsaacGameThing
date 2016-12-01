using UnityEngine;
using System.Collections;
using System;

public class LaserGun : Weapon {

    //initialization
    new void Start () {
        base.Start();
        damage = 25;
        timeBetweenShots = 1.5f;
	}

    public override void fire()
    {
        if (timeSinceFired > timeBetweenShots) //controls the fire rate
        {
            timeSinceFired = 0;
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i].direction = aimVector;
                bullets[i].damage = damage;
                Bullet bul = Instantiate(bullets[i], bulletOrigin.transform.position, player.transform.rotation) as Bullet;
                bul.transform.rotation = transform.rotation;
            }
        }
    }

    public override string getName()
    {
        return "Laser Gun";
    }
}
