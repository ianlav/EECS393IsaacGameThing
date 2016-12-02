using UnityEngine;
using System.Collections;
using System;

public class SequentialSpreadGun : SpreadGun {

    float timeBetweenSpreadBullets; //separation between bullets in spread
    float timeSinceLastBullet;
    int bulletsFired;

    //initialization
    public new void Start () {
        base.Start();
        damage = 5;
        timeBetweenShots = 0.6f;
        timeBetweenSpreadBullets = 0.1f;
        spreadAngleDegrees = 60;
        numProjectiles = bullets.Length;
        bulletRotations = new float[numProjectiles];
        calculateSpread();
    }

    public override string getName()
    {
        return "Sequential Spread Gun";
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

    //update is called once per frame
    new void Update()
    {
        base.Update();
        //fire each bullet on a delay
        if (bulletsFired < bullets.Length)
        {
            timeSinceLastBullet += Time.deltaTime;
            if (timeSinceLastBullet > timeBetweenSpreadBullets)
            {
                bullets[bulletsFired].direction = aimVector.Rotate(bulletRotations[bulletsFired]);
                bullets[bulletsFired].damage = damage;
                Instantiate(bullets[bulletsFired], bulletOrigin.transform.position, player.transform.rotation);
                timeSinceLastBullet = 0;
                bulletsFired++;
            }
        }
    }
}
