﻿using UnityEngine;
using System.Collections;
using System;

public class MachineGun : Weapon {

    //initialization
    public new void Start ()
    {
        base.Start();
        damage = 1;
        timeBetweenShots = 0.05f;
        numProjectiles = bullets.Length;
    }

    public override string getName()
    {
        return "Machine Gun";
    }

    public override void fire()
    {
        if (timeSinceFired > timeBetweenShots) //controls the fire rate
        {
            timeSinceFired = 0;
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i].direction = aimVector + new Vector2(UnityEngine.Random.Range(0, 2), UnityEngine.Random.Range(0, 2));
                bullets[i].damage = damage;
                Instantiate(bullets[i], bulletOrigin.transform.position, player.transform.rotation);
            }
        }
    }
}
