using UnityEngine;
using System.Collections;

public class MachineGun : Weapon {

	//initialization
	new void Start () {
        base.Start();
        damage = 5;
        timeBetweenShots = 0.3f;
	}

    public override void fire()
    {
        //not implemented
        //should fire in bursts of 3-5 (maybe 3 then upgrade to 5?)
    }
}
