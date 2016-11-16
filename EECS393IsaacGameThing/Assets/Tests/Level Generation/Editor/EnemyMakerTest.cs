using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections;
//using AssemblyCSharp;

public class EnemyMakerTest {

	EnemyMaker maker = new EnemyMaker();
	CrabEnemy enemy = new CrabEnemy ();

	[Test]
	//Test for "seeker" enemies moving towards player 
	public void EnemyTowardsPlayerTest(){
		//Need to force a platform to exist
		enemy.moveTowardsPlayerAccel = 2;
		enemy.maxSpeed = 6;
		enemy.isCoward = false;
		Vector2 enemyPos = new Vector2 (0f, 0f);
		Vector2 playerPos = new Vector2 (10f, 0f);
		Vector2 newEnemyPos = enemy.MoveEnemy(enemyPos, enemyPos, playerPos);
		Assert.IsTrue(enemyPos.x<newEnemyPos.x);
	}

	[Test]
	//Test for "seeker" enemies hit with the fear effect moving away from player 
	public void AfraidEnemyFromPlayerTest(){
		//Need to force a platform to exist
		enemy.moveTowardsPlayerAccel = 2;
		enemy.maxSpeed = 6;
		enemy.isCoward=true;
		Vector2 enemyPos = new Vector2 (0f, 0f);
		Vector2 playerPos = new Vector2 (10f, 0f);
		Vector2 newEnemyPos = (enemy.MoveEnemy(enemyPos, enemyPos, playerPos));
		Assert.IsTrue(enemyPos.x>newEnemyPos.x);
	}

	[Test]
	//Test ability to spawn an enemy when enough points are present
	public void EnemySpawnTest(){
		maker.setCurrentCost (0);
		maker.setMaxCost (10);
		Assert.IsTrue(maker.testIfEnemyMakeable(enemy));
	}

	[Test]
	//Test for correct behavior (no spawn) when there are not enough points to spawn an enemy.
	public void EnemySpawnNoRoomTest(){
		//Need to force a platform to exist
		maker.setCurrentCost (10);
		maker.setMaxCost (5);
		Assert.IsFalse(maker.testIfEnemyMakeable(enemy));
	}
}
