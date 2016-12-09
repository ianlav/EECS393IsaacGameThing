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
	public bool isCoward;
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
    private PlayerMovement player;


	public virtual void Start() {
		base.Start();
		rigidEnemy = GetComponent<Rigidbody2D>();
		maker = FindObjectOfType<EnemyMaker>(); //finds and holds the enemy maker
		gameObject.tag = "Enemy";
		if(!shootsTowardsPlayer && bulletVelocity != 0){
			bulletVector = new Vector2 (Mathf.Cos (bulletAngle * Mathf.PI/180f)/6.28f*bulletVelocity, 
				(float)Mathf.Sin (bulletAngle * Mathf.PI/180f)/6.28f*bulletVelocity);
		}
        player = FindObjectOfType<PlayerMovement>();
	}

	protected void Update()
	{
		if (hp <= 0)
		{
			Destroy(gameObject);
		}

		//Figure out how to trigger this less often to save precmoveTowardsPlayerAccelTowmoveRandomAccel + moveRandomAccel + movePatrolAccel > 0) {
		Vector2 movementVector = rigidEnemy.velocity;
		Vector2 enemyPos = this.transform.position;

		if (Vector2.Distance (enemyPos, player.transform.position) < perceptionRange) {
			rigidEnemy.velocity = MoveEnemy (movementVector, enemyPos, player.transform.position);
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

	public void applyEffect(string effect){
		if(effect.Equals("Fear")){
			this.isCoward=true;
		}
	}

	public Vector2 MoveEnemy(Vector2 movementVector, Vector2 enemyPos, Vector2 playerPos){
		//modifier is used to do wholesale effects on enemy motion, like slowing or freezing enemies.
		float modifier = 1;
		if(isCoward){modifier*=-1;}
		if (moveTowardsPlayerAccel > 0f) {
			if (enemyPos.x < playerPos.x) {
				movementVector.x += modifier*moveTowardsPlayerAccel;
			} else {
				movementVector.x -= modifier*moveTowardsPlayerAccel;
			}
			if (flying) {
				if (enemyPos.y < playerPos.y) {
					movementVector.y += modifier*moveTowardsPlayerAccel;
				} else {
					movementVector.y -= modifier*moveTowardsPlayerAccel;
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
		return movementVector;
	}


	private void FireBullet(){
		//Just straight up stealing the procedure from Weapon
		if (bulletVelocity != 0) {
			if (timeSinceLastBullet >= timeBetweenBullets) {
				timeSinceLastBullet = 0;
				Bullet bul = Instantiate (bullets[0], transform.position, rigidEnemy.transform.rotation) as Bullet;
				bul.speed = 2;
                if (shootsTowardsPlayer)
                    bul.direction = getPlayerVector();
                else
                    bul.direction = bulletVector;
				bul.damage = damage;
				bul.enemyMode = true;
			} else {
				timeSinceLastBullet++;
			}
		}

	}

    protected Vector2 getPlayerVector()
    {
        return player.transform.position - transform.position;
    }

	protected void onDestroy(){
		EnemyMaker.currentEnemyCost -= cost;
	}
} 
