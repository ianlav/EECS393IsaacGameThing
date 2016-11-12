using UnityEngine;
using System.Collections;

//abstract parent class for all placeable enemies
public abstract class Enemy : CharacterModel {

	public int cost; //How much it "costs" to spawn the enemy. Higher for larger/dangerous enemies. Affects score.
	public double maxSpeed; //Max distance moved per update.
	public int baseDamage; //Damage dealt by bullet/on contact without any modifiers
	public EnemyMaker maker;
	//Movement AI builder
	public float moveRandomSpeed;
		public float randomVelocityX;
		public float randomVelocityY;
	public float moveTowardsPlayerSpeed;
	public float movePatrolSpeed;
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

		//Figure out how to trigger this less often to save precious flops?
		if (moveTowardsPlayerSpeed + moveRandomSpeed + movePatrolSpeed > 0) {
			Vector2 movementVector = rigidEnemy.velocity;
			Vector2 enemyPos = this.transform.position;
			Vector2 playerPos = GameObject.Find ("Player").transform.position;

			if (movementVector.SqrMagnitude()<maxSpeed*maxSpeed && Vector2.Distance (enemyPos, playerPos) < 5) {
				if (moveTowardsPlayerSpeed > 0) {
					if (playerPos.x > enemyPos.x) {
						movementVector.x += moveTowardsPlayerSpeed;
					} else {
						movementVector.x -= moveTowardsPlayerSpeed;
					}
					if (flying) {
						if (playerPos.y > enemyPos.y) {
							movementVector.y += moveTowardsPlayerSpeed;
						} else {
							movementVector.y -= moveTowardsPlayerSpeed;
						}
					}
				}
				if (moveRandomSpeed > 0f) {
					randomVelocityX += Random.Range(-1,1) * moveRandomSpeed;
					movementVector.x += randomVelocityX;
					if (flying) {
						randomVelocityY += Random.value * moveRandomSpeed;
						movementVector.y += randomVelocityY;
					}
				}
				if (movePatrolSpeed > 0f) {
					if (patrolDirectionIsLeft) {
						movementVector.x -= movePatrolSpeed;
					} else {
						movementVector.x += movePatrolSpeed;
					}
				}
				rigidEnemy.velocity = movementVector;
			}
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