﻿using UnityEngine;
using System.Collections;
using System;

public class BurstGun : Weapon {
    
    float timeBetweenBurstBullets; //separation between bullets in burst
    float timeSinceLastBullet;
    int bulletsFired;

    //initialization
    public new void Start()
    {
        base.Start();
        damage = 5;
        timeBetweenShots = 1f;
        timeBetweenBurstBullets = 0.05f;
        numProjectiles = bullets.Length;
    }

    public override string getName()
    {
        return "Burst Gun";
    }

    public override void fire()
    {
        if (timeSinceFired > timeBetweenShots) //controls the burst fire rate
        {
            bulletsFired = 0;
            timeSinceFired = 0;
            timeSinceLastBullet = 0;
        }
    }

    new void Update()
    {
        base.Update();
        //fire bursts
        if (bulletsFired < bullets.Length)
        {
            timeSinceLastBullet += Time.deltaTime;
            if (timeSinceLastBullet > timeBetweenBurstBullets)
            {
                bullets[bulletsFired].direction = aimVector;
                bullets[bulletsFired].damage = damage;
                Instantiate(bullets[bulletsFired], bulletOrigin.transform.position, player.transform.rotation);
                timeSinceLastBullet = 0;
                bulletsFired++;
            }
        }
    }
}
