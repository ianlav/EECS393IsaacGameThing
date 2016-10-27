using UnityEngine;
//using System.Collections;

public abstract class Enemy : MonoBehaviour {

	public int cost; //How much it "costs" to spawn the enemy. Higher for larger/dangerous enemies. Affects score.
	public double maxSpeed; //Max distance moved per update.
	public int baseDamage; //Damage dealt by bullet/on contact without any modifiers
	public EnemyMaker maker;

	void Start() {
		maker = FindObjectOfType<EnemyMaker>();
	}

	void Update () {}

	void OnTriggerExit2D(Collider2D col)
	{
		if(col.CompareTag("LevelTrigger")){
			maker.setCurrentCost (maker.getCurrentCost() - cost); //recycle monster's cost
			Destroy(gameObject, 3);	
		}
	}

}