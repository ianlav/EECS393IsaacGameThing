using System;

public class HoverEnemy : Enemy{

	//Not sure of the accepted way of doing this, but it'll work for Round 1.
	new public static int cost = 7;
	new public static double maxSpeed = 2;
	new public static double baseDamage = 15;

	new void Start () {
        base.Start();
	}

	new void Update () {
        base.Update();
		//Eventually we'll have *moving* monsters.
	}


}

