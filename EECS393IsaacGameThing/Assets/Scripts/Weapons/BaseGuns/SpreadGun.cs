using UnityEngine;
using System.Collections;
using System;

public class SpreadGun : Weapon {

    protected float spreadAngleDegrees, spreadAngleIncrement;
    protected float[] bulletRotations;

    public new void Start()
    {
        base.Start();
        spreadAngleDegrees = 60;
        numProjectiles = bullets.Length;
        timeBetweenShots = 0.2f;
        bulletRotations = new float[numProjectiles];
        calculateSpread();
        timeSinceFired = 0;
    }

    public override string getName()
    {
        return "Spread Gun";
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

    protected override void Update()
    {
        base.Update();
    }

    //fires a grouping of bullets towards the enemy in a specified spread pattern
    public override void fire()
    {
        if (timeSinceFired > timeBetweenShots) //controls the fire rate
        {
            timeSinceFired = 0;

            //create all bullets and rotate to appropriate spread angles
            for (int i=0; i < bullets.Length; i++)
            {
                bullets[i].direction = aimVector.Rotate(bulletRotations[i]);
                bullets[i].damage = damage;
                Instantiate(bullets[i], bulletOrigin.transform.position, player.transform.rotation);
            }
        }
    }
}
