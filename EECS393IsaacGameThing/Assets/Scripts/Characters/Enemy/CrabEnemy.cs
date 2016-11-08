using System;

public class CrabEnemy : Enemy{

	//Not sure of the accepted way of doing this, but it'll work for Round 1.
	new public static int cost = 1;
	new public static double maxSpeed = 0;
	new public static double baseDamage = 5;

	new void Start () {
        base.Start();
	}

	new void Update () {
        base.Update();
		//Eventually we'll have *moving* monsters.
	}


}

