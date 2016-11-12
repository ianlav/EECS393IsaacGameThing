using UnityEngine;
using System.Collections;

public class MachineSpreadGun : SpreadGun {

    new void Start()
    {
        base.Start();
        spreadAngleDegrees = 60;
        numProjectiles = bullets.Length;
        timeBetweenShots = 0.4f;
        bulletRotations = new float[numProjectiles];
        //bulletOrigin = transform.Find("SpreadGun");
        //player = (PlayerMovement)FindObjectOfType(typeof(PlayerMovement));
        calculateSpread();
    }
}
