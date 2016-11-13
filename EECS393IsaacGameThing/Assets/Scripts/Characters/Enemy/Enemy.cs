using UnityEngine;
using System.Collections;

//abstract parent class for all placeable enemies
public abstract class Enemy : CharacterModel {

	public int cost; //How much it "costs" to spawn the enemy. Higher for larger/dangerous enemies. Affects score.
	public float maxSpeed; //Max distance moved per update.
	public int baseDamage; //Damage dealt by bullet/on contact without any modifiers
	public EnemyMaker maker;
	//Movement AI builder
	public int perceptionRange;
	public float moveRandomAccel;
		public float randomVelocityX;
		public float randomVelocityY;
	public float moveTowardsPlayerAccel;
	public float movePatrolAccel;
	public bool patrolDirectionIsLeft;
	public bool flying;
	public bool movementDetectsEdges; //NYI
	Rigidbody2D rigidEnemy;


	protected void Start() {
        base.Start();
		rigidEnemy = GetComponent<Rigidbody2D>();
		maker = FindObjectOfType<EnemyMaker>(); //finds and holds the enemy maker
        gameObject.tag = "Enemy";
        gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

    protected void Update()
    {
        if (hp <= 0)
        {
            EnemyMaker.currentEnemyCost -= cost;
            Destroy(gameObject);
        }

		//Figure out how to trigger this less often to save precmoveTowardsPlayerAccelTowmoveRandomAccel + moveRandomAccel + movePatrolAccel > 0) {
			Vector2 movementVector = rigidEnemy.velocity;
			Vector2 enemyPos = this.transform.position;
			Vector2 playerPos = GameObject.Find ("Player").transform.position;

			if (Vector2.Distance(enemyPos, playerPos) < perceptionRange) {
			if (moveTowardsPlayerAccel > 0f) {
				if (enemyPos.x < playerPos.x) {
					movementVector.x += moveTowardsPlayerAccel;
				} else {
					movementVector.x -= moveTowardsPlayerAccel;
				}
				if (flying) {
					if (enemyPos.y < playerPos.y) {
						movementVector.y += moveTowardsPlayerAccel;
					} else {
						movementVector.y += moveTowardsPlayerAccel;
					}	
				}
			}
			if (moveRandomAccel > 0f) {
				movementVector.x += Random.Range(-1,1) * moveRandomAccel;
				if(flying) {
					movementVector.y += Random.value * moveRandomAccel;
				}
			}
			if (movePatrolAccel > 0f) {
				if (patrolDirectionIsLeft) {
					movementVector.x -= movePatrolAccel;
				} else {
					movementVector.x += movePatrolAccel;
				}
			}

			if(movementVector.x > maxSpeed){
				movementVector.x = maxSpeed;
			}
			if (movementVector.x < -maxSpeed) {
				movementVector.x = -maxSpeed;
			}
			if(movementVector.y > maxSpeed){
				movementVector.y = maxSpeed;
			}
			if (movementVector.y < -maxSpeed) {
				movementVector.y = -maxSpeed;
			}
				rigidEnemy.velocity = movementVector;
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