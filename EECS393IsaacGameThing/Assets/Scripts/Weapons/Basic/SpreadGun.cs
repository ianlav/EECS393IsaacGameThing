﻿using UnityEngine;
using System.Collections;

public class SpreadGun : Weapon {

    public Transform bulletOrigin;
    public Bullet[] bullets;
    public PlayerMovement player;
    //protected int numBullets;
    protected float spreadAngleDegrees, spreadAngleIncrement;
    protected float[] bulletRotations;

    void Start()
    {
        spreadAngleDegrees = 60;
        //numBullets = 3;
        numProjectiles = bullets.Length;
        timeBetweenShots = 1;
        bulletRotations = new float[numProjectiles];
        //bulletOrigin = transform.Find("SpreadGun");
        //player = (PlayerMovement)FindObjectOfType(typeof(PlayerMovement));
        calculateSpread();
    }

    //caching for spread angles, no reason to recalculate all the time
    public void calculateSpread()
    {
        float spreadAngleIncrement = spreadAngleDegrees / (numProjectiles - 1);
        for (int i = 0; i < numProjectiles; i++)
        {
            bulletRotations[i] = -i * spreadAngleIncrement + spreadAngleDegrees / 2;
        }
    }

    //fires a grouping of bullets towards the enemy in a specified spread pattern
    public override void fire()
    {
        if (timeSinceFired > timeBetweenShots) //controls the fire rate
        {
            timeSinceFired = 0;
            
            //create all bullets and rotate to appropriate spread angles
            for(int i=0; i < bullets.Length; i++)
            {
                bullets[i].direction = getAimVector(bulletOrigin.position).Rotate(bulletRotations[i]);
                bullets[i].damage = damage;
                Instantiate(bullets[i], bulletOrigin.transform.position, player.transform.rotation);
            }
        }
    }
}
