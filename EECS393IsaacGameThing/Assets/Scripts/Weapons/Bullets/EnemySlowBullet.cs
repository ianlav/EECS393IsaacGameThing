using UnityEngine;
using System.Collections;

public class EnemySlowBullet : Bullet {

	new void Start()
	{
		base.Start();
		damage = 10;
		speed = 20;
		effect = "Slow";
	}

	//default enemy hit: damage enemy
	public override void hit(PlayerMovement player)
	{
		player.applyEffect(effect);
		player.takeDamage(damage);
	}


	//default platform hit: just destroy bullet
	public override void hit(Platform platform)
	{
	}
}
