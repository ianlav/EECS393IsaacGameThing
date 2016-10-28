using UnityEngine;
using System.Collections.Generic;
//using AssemblyCSharp;
using System;
using System.Linq;

public class EnemyMaker : MonoBehaviour {

	public static int maxEnemyCost = 10; //Total number of monster "points" available. Threat correlates to cost
	public static int currentEnemyCost = 0; //Total point worth of monsters currently spawned
	public Enemy newestEnemy;
	public Enemy[] enemyTypes;
	public List<Enemy> extantEnemies;

	public void Start()
	{	
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
		print("Making enemy!");
		//Current implementation only considers arenawide range, but would use the maker for
		//enemy-mediated spawns as well (i.e. hive spawning bees). Overload later?
		if (currentEnemyCost < maxEnemyCost) {
			//Select an enemy type at random
			//If its associated cost won't push us over the total, we are allowed to spawn it.
			//This will definitely lead to "missed" spawn calls, which is fine (variety!)
			Enemy chosenEnemy = enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Length)];
			//Need to re-generalize this once we get that thing working...
			print("chosenEnemy Enemy cost: "+chosenEnemy.cost);
			if (testIfEnemyMakeable(chosenEnemy)) {
				makeSpecificEnemy(chosenEnemy, plat, pos);
				return true;
			} else {return false;}
		} else {return false;}
	}
		
	public void makeSpecificEnemy(Enemy chosenEnemy, Platform plat, Vector2 pos){
			newestEnemy = Instantiate (chosenEnemy, 
				pos - (Vector2)plat.leftSide.position + (Vector2)plat.transform.position //the set position, offset by the distance between the left side and center. Because localPosition wont give me that
				, Quaternion.identity) as Enemy;
	}

	//Contains all the logic to determine whether we can "afford" to spawn a specific enemy.
	//Currently just cost, but in the future may contain other things as well.
	public bool testIfEnemyMakeable(Enemy chosenEnemy){
		if (currentEnemyCost + chosenEnemy.cost <= maxEnemyCost) {
			currentEnemyCost += chosenEnemy.cost;
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