using UnityEngine;
using System.Collections;

public class MachineGun : Weapon {

    //initialization
    new void Start ()
    {
        base.Start();
        damage = 5;
        timeBetweenShots = 0.4f;
        numProjectiles = bullets.Length;
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
                Instantiate(bullets[i], bulletOrigin.transform.position, player.transform.rotation);
            }
        }
    }
}
