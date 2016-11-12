using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SineGun : Weapon {
    
    float bulletSpeed;
    int bulletTimeToExist;
    //references to modify bullets mid-flight
    List<Bullet> bulletRefs = new List<Bullet>(); 
    List<Vector2> directionRefs = new List<Vector2>();

    //initialization
    new void Start () {
        base.Start();
        damage = 5;
        bulletSpeed = 12;
        timeBetweenShots = 0.7f;
        bulletTimeToExist = 2;
        timeBetweenBulletPatternUpdates = 0.01f;
	}
	
	//update is called once per frame
	new void Update () {
        base.Update();
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
                bullets[i].speed = bulletSpeed;
                bullets[i].timeToExist = bulletTimeToExist;
                //insert references in first open slot (null = bullet was destroyed)
                int index = bulletRefs.IndexOf(null);
                if (index > 0)
                {
                    bulletRefs.Insert(index, (Instantiate(bullets[i], bulletOrigin.transform.position, player.transform.rotation)
                        as Bullet).GetComponent<Bullet>());
                    directionRefs.Insert(index, aimVector);
                }
                else
                {
                    bulletRefs.Add((Instantiate(bullets[i], bulletOrigin.transform.position, player.transform.rotation)
                        as Bullet).GetComponent<Bullet>());
                    directionRefs.Add(aimVector);
                    index = bulletRefs.Count - 1;
                }
            }
        }
    }

    protected override void updateBulletPattern()
    {
        for (int i = 0; i < bulletRefs.Count; i++)
        {
            //rotate the original direction vector as a function of time in a sine pattern
            if (bulletRefs[i] != null)
                bulletRefs[i].direction = directionRefs[i].Rotate(Mathf.Sin(2*timeSinceFired/timeBetweenShots * 2*Mathf.PI) * Mathf.Rad2Deg);
        }
    }
}
