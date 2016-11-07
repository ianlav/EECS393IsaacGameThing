using UnityEngine;
using System.Collections;

public class LaserGun : Weapon {

    //initialization
    new void Start () {
        base.Start();
        damage = 25;
        timeBetweenShots = 1.5f;
	}

    public override void fire()
    {
        //not implemented
        //should fire 1 long bullet that pierces
    }
}
