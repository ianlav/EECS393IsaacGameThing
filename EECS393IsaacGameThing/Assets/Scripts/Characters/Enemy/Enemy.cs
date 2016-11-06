using UnityEngine;
using System.Collections;

//abstract parent class for all placeable enemies
public abstract class Enemy : CharacterModel {

	public int cost; //How much it "costs" to spawn the enemy. Higher for larger/dangerous enemies. Affects score.
	public double maxSpeed; //Max distance moved per update.
	public int baseDamage; //Damage dealt by bullet/on contact without any modifiers
	public EnemyMaker maker;

	protected void Start() {
		maker = FindObjectOfType<EnemyMaker>(); //finds and holds the enemy maker
	}

    protected void Update()
    {
        if (hp <= 0)
        {
            EnemyMaker.currentEnemyCost -= cost;
            Destroy(gameObject);
        }
    }

	void OnTriggerExit2D(Collider2D col)
	{
        //if the enemy leaves the player's range, despawn the enemy
		if(col.CompareTag("LevelTrigger")){
			Destroy(gameObject);	
		}
	}

    void OnCollisionEnter2D(Collision2D col)
    {
    }

    //called when the enemy is destroyed
    void OnDestroy()
    {
        maker.setCurrentCost(maker.getCurrentCost() - cost); //recycle monster's cost
    }

}