using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using AssemblyCSharp;

public class EnemyMakerTest {

	EnemyMaker maker = new EnemyMaker();

	[Test]
	//Test ability to correctly account for enemy types. Will require change when reflection is implemented.
	public void EnemyTypeCountTest(){
		int numberOfEnemyTypes = 1;
		maker.start();
		Assert.AreEqual (numberOfEnemyTypes, maker.getEnemyTemplates.Length);
	}

	[Test]
	//Test ability to spawn an enemy when enough points are present
	public void EnemySpawnTest(){
		maker.setCurrentCost (0);
		maker.setMaxCost (10);
		//Need to force a platform to exist
		Assert.IsTrue(maker.makeSpecificInRange(CrabEnemy));
	}

	[Test]
	//Test for correct behavior (no spawn) when no platforms are present
	public void EnemySpawnNoPlatformsTest(){
		//Need to force no platforms to exist
	}

	[Test]
	//Test for correct behavior (no spawn) when there are not enough points to spawn an enemy.
	public void EnemySpawnNoRoomTest(){
		//Need to force a platform to exist
		maker.setCurrentCost (10);
		maker.setMaxCost (5);
		Assert.IsTrue(maker.makeSpecificInRange(CrabEnemy));
	}
}
