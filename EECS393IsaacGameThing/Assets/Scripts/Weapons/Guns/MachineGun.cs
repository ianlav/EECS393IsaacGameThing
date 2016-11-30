using UnityEngine;
using System.Collections;
using System;

public class MachineGun : Weapon {

    //initialization
    new void Start ()
    {
        base.Start();
        damage = 5;
        timeBetweenShots = 0.05f;
        numProjectiles = bullets.Length;
    }

    public override void fire()
    {
        if (timeSinceFired > timeBetweenShots) //controls the fire rate
        {
            timeSinceFired = 0;
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i].direction = aimVector + new Vector2(UnityEngine.Random.Range(0, 0.5f), UnityEngine.Random.Range(0, 0.5f));
                bullets[i].damage = damage;
                Instantiate(bullets[i], bulletOrigin.transform.position, player.transform.rotation);
            }
        }
    }

    public override string getName()
    {
        return "Machine Gun";
    }
}
