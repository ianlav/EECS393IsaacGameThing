﻿using UnityEngine;
using System.Collections;
using System;

public class StartGun : Weapon {

    public new void Start()
    {
        base.Start();
        numProjectiles = bullets.Length;
        timeBetweenShots = 0.4f;
    }

    public override string getName()
    {
        return "Pistol";
    }

    //basic implementation. fires a straight line bullet towards the mouse
    public override void fire()
    {
        if (timeSinceFired > timeBetweenShots) //controls the fire rate
        {
            timeSinceFired = 0;
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i].direction = aimVector;
                bullets[i].damage = damage;
                Instantiate(bullets[i], bulletOrigin.transform.position, player.transform.rotation);
            }
        }
    }
}
