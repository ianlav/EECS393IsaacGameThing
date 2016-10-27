using UnityEngine;
using System.Collections.Generic;
using AssemblyCSharp;
using System;
using System.Linq;

public class EnemyMaker : MonoBehaviour {

	public int maxEnemyCost = 10; //Total number of monster "points" available. Threat correlates to cost
	public int currentEnemyCost = 0; //Total point worth of monsters currently spawned
	public Enemy newestEnemy;
	public Enemy[] enemyTypes;
	public List<Enemy> extantEnemies;
	public Platform[] spawningPlatforms; //Array of not-yet-spawned Platforms from PlatformMaker

	void Start()
	{	
		spawningPlatforms = FindObjectOfType<PlatformMaker> ().platformOptions;
		//Sloppily instantiates one of each Enemy inheritor to act as a reference object b/c how2reflectlol
		/*Type[] enemyTypes = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
			from assemblyType in domainAssembly.GetTypes()
			where typeof(Enemy).IsAssignableFrom(assemblyType) && !typeof(Enemy).IsAbstract
			select assemblyType).ToArray();
		enemyTemplates = new Enemy[enemyTypes.Length];
		for(int i = 0; i < enemyTypes.Length; i++) {
			enemyTemplates[i]= (Enemy)Activator.CreateInstance(enemyTypes[i]);
		}*/
	}

	public bool makeRandomEnemy(Platform plat, Vector2 pos)
	{
		//Current implementation only considers arenawide range, but would use the maker for
		//enemy-mediated spawns as well (i.e. hive spawning bees). Overload later?
		if (currentEnemyCost < maxEnemyCost) {
			//Select an enemy type at random
			//If its associated cost won't push us over the total, we are allowed to spawn it.
			//This will definitely lead to "missed" spawn calls, which is fine (variety!)
			Enemy chosenEnemy = enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Length)];
			//Need to re-generalize this once we get that thing working...
			if (currentEnemyCost + CrabEnemy.cost <= maxEnemyCost) {
				return makeSpecificEnemy(chosenEnemy, plat, pos);
			} else {return false;}
		} else {return false;}
	}
		
	public bool makeSpecificEnemy(Enemy chosenEnemy, Platform plat, Vector2 pos){
		if (currentEnemyCost + chosenEnemy.cost <= maxEnemyCost && spawningPlatforms.Length>0) {
			newestEnemy = Instantiate (chosenEnemy, 
				pos - (Vector2)plat.leftSide.position + (Vector2)plat.transform.position //the set position, offset by the distance between the left side and center. Because localPosition wont give me that
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
		return enemyTypes;
	}
}