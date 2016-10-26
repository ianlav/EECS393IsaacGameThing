using UnityEngine;
using System.Collections.Generic;
using AssemblyCSharp;
using System;
using System.Linq;

namespace AssemblyCSharp{

public class EnemyMaker : Maker {

	public int maxEnemyCost = 10; //Total number of monster "points" available. Threat correlates to cost
	public int currentEnemyCost = 0; //Total point worth of monsters currently spawned
	public Enemy newestEnemy;
	public Enemy[] enemyTemplates;
	public List<Enemy> extantEnemies;
	public Platform[] spawningPlatforms; //Array of not-yet-spawned Platforms from PlatformMaker

	public override void Start()
	{	
		base.Start();
		spawningPlatforms = FindObjectOfType<PlatformMaker> ().platformOptions;

		//Sloppily instantiates one of each Enemy inheritor to act as a reference object b/c how2reflectlol
		/*Type[] enemyTypes = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
			from assemblyType in domainAssembly.GetTypes()
			where typeof(Enemy).IsAssignableFrom(assemblyType) && !typeof(Enemy).IsAbstract
			select assemblyType).ToArray();
		enemyTemplates = new Enemy[enemyTypes.Length];
		for(int i = 0; i < enemyTypes.Length; i++) {
			enemyTemplates[i]= (Enemy)Activator.CreateInstance(enemyTypes[i]);
		}
		print("Fight me "+enemyTypes.Length);*/
		enemyTemplates[0] = new CrabEnemy ();
	}

	override public bool makeInRange()
	{
		//Current implementation only considers arenawide range, but would use the maker for
		//enemy-mediated spawns as well (i.e. hive spawning bees). Overload later?
		if (currentEnemyCost < maxEnemyCost) {
			//Select an enemy type at random
			//If its associated cost won't push us over the total, we are allowed to spawn it.
			//This will definitely lead to "missed" spawn calls, which is fine (variety!)
			//(int)UnityEngine.Random.Range(0, enemyTemplates.Length)
			Enemy enemyRef = enemyTemplates[0];
			if (currentEnemyCost + enemyRef.cost <= maxEnemyCost) {
				return makeSpecificInRange (enemyRef.GetType ());
			} else {return false;}
		} else {return false;}
	}
		
	public bool makeSpecificInRange(Type enemyType){
		Enemy monster = (Enemy)Activator.CreateInstance (enemyType);
		if (currentEnemyCost + monster.cost <= maxEnemyCost && spawningPlatforms.Length>0) {
			Platform location = spawningPlatforms [(int)UnityEngine.Random.Range (0, spawningPlatforms.Length)];
			newestEnemy = Instantiate (monster, 
				(Vector2)location.leftSide.position + (Vector2)location.transform.position //the set position, offset by the distance between the left side and center. Because localPosition wont give me that
				, Quaternion.identity) as Enemy;
			return true;
		} else
			return false;
	}

	public void setCurrentCost(int newCost){
		currentEnemyCost = newCost;
	}
	public void setMaxCost(int newCost){
		maxEnemyCost = newCost;
	}
	public int getCurrentCost(){
		return currentEnemyCost;
	}
	public Enemy[] getEnemyTemplates(){
		return enemyTemplates;
	}
}
}