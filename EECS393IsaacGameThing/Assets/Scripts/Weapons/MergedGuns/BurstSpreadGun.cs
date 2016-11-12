using UnityEngine;
using System.Collections;

public class BurstSpreadGun : SpreadGun {

    public int numSpreadsPerBurst = 3;

    float timeBetweenSpreadBursts; //separation between spreads in burst
    float timeSinceLastSpread;
    int burstsFired;

    //initialization
    new void Start()
    {
        base.Start();
        damage = 5;
        timeBetweenShots = 0.75f; //controls overall fire rate
        timeBetweenSpreadBursts = 0.05f; //controls burst separation
        spreadAngleDegrees = 60;
        numProjectiles = bullets.Length;
        bulletRotations = new float[numProjectiles];
        calculateSpread();
    }

    public override void fire()
    {
        if (timeSinceFired > timeBetweenShots) //controls the burst fire rate
        {
            burstsFired = 0;
            timeSinceFired = 0;
            timeSinceLastSpread = 0;
        }
    }

    //update is called once per frame
    new void Update()
    {
        base.Update();
        //fire bursts
        if (burstsFired < numSpreadsPerBurst)
        {
            timeSinceLastSpread += Time.deltaTime;
            if (timeSinceLastSpread > timeBetweenSpreadBursts)
            {
                for (int i = 0; i < bullets.Length; i++)
                {
                    bullets[i].direction = aimVector.Rotate(bulletRotations[i]);
                    bullets[i].damage = damage;
                    Instantiate(bullets[i], bulletOrigin.transform.position, player.transform.rotation);
                }
                timeSinceLastSpread = 0;
                burstsFired++;
            }
        }
    }
}
