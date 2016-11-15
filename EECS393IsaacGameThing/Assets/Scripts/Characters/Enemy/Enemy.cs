using UnityEngine;
using System.Collections;

//abstract parent class for all placeable enemies
public abstract class Enemy : CharacterModel {

	public int cost; //How much it "costs" to spawn the enemy. Higher for larger/dangerous enemies. Affects score.
	public float maxSpeed; //Max distance moved per update.
	public int baseDamage; //Damage dealt by bullet/on contact without any modifiers
	public EnemyMaker maker;
	Rigidbody2D rigidEnemy;
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
	//Ranged Weapon "AI" builder
	//Could be expanded for plural bullet headings per enemy by using iteration over arrays...
	public Bullet[] bullets;
	public bool shootsTowardsPlayer;
	public int bulletAngle;
	private Vector2 bulletVector;
	public float bulletVelocity;
	public int bulletAngleVariation; //Essentially accuracy
	public int damage;
	public int timeBetweenBullets;
	private int timeSinceLastBullet;


	protected void Start() {
        base.Start();
		rigidEnemy = GetComponent<Rigidbody2D>();
		maker = FindObjectOfType<EnemyMaker>(); //finds and holds the enemy maker
        gameObject.tag = "Enemy";
        gameObject.layer = LayerMask.NameToLayer("Enemy");
		if(!shootsTowardsPlayer && bulletVelocity != 0){
			bulletVector = new Vector2 (Mathf.Cos (bulletAngle * Mathf.PI/180f)/6.28f*bulletVelocity, 
				(float)Mathf.Sin (bulletAngle * Mathf.PI/180f)/6.28f*bulletVelocity);
		}
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

		if (Vector2.Distance (enemyPos, playerPos) < perceptionRange) {
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
						movementVector.y -= moveTowardsPlayerAccel;
					}	
				}
			}
			if (moveRandomAccel > 0f) {
				movementVector.x += Random.Range (-1, 1) * moveRandomAccel;
				if (flying) {
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

			if (movementVector.x > maxSpeed) {
				movementVector.x = maxSpeed;
			}
			if (movementVector.x < -maxSpeed) {
				movementVector.x = -maxSpeed;
			}
			if (movementVector.y > maxSpeed) {
				movementVector.y = maxSpeed;
			}
			if (movementVector.y < -maxSpeed) {
				movementVector.y = -maxSpeed;
			}
			rigidEnemy.velocity = movementVector;
		}
		FireBullet ();
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

	private void MoveEnemy(){
		
	}

	private void FireBullet(){
		//Just straight up stealing the procedure from Weapon
		if (bulletVelocity != 0) {
			if (timeSinceLastBullet >= timeBetweenBullets) {
				timeSinceLastBullet = 0;
				bullets[0].direction = bulletVector;
				bullets[0].damage = damage;
				bullets [0].enemyMode = true;
				Instantiate (bullets[0], rigidEnemy.transform.position, rigidEnemy.transform.rotation);
			} else {
				timeSinceLastBullet++;
			}
		}
			
	}

}