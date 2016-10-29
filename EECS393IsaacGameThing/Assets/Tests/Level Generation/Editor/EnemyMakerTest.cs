using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections;
//using AssemblyCSharp;

public class EnemyMakerTest {

	EnemyMaker maker = new EnemyMaker();
	CrabEnemy enemy = new CrabEnemy ();

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
