using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Monster : MonoBehaviour {

	public int updatesToUpgrade;
	private int updateClock;
	public float maxVelocity; //Highest possible speed of monster. Increases as game progresses.
	public float acceleration; //Ability for monster to return to speed when hit. Increases.
	public float defense; //Modifier for the amount that damage slows down monster. Increases.
	private Rigidbody2D rigidMonster;

	void Start () {
		rigidMonster = GetComponent<Rigidbody2D>();
        gameObject.tag = "Monster";
        gameObject.layer = LayerMask.NameToLayer("Monster");
    }

	public void takeDamage(int damage){
		maxVelocity -= damage/defense; //slow the monster when hit by projectile
	}

	void Update () {
		if(rigidMonster.velocity.x<maxVelocity){
			Vector2 playerPos = GameObject.Find ("Player").transform.position;
			rigidMonster.velocity = new Vector2(rigidMonster.velocity.x+acceleration, playerPos.y);
			if(rigidMonster.velocity.x > maxVelocity){rigidMonster.velocity=new Vector2(maxVelocity, playerPos.y);}
		}
		if (updateClock == updatesToUpgrade) {
			updateClock = 0;
			UpgradeMonster ();
		} else {
			updateClock++;
		}
	}

	void UpgradeMonster(){
		maxVelocity += 0.25f;
		acceleration += 0.1f;
		defense += 1f;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player") || col.CompareTag("Floor"))
            Destroy(col.gameObject);
		if (col.CompareTag ("Bullet"))
			rigidMonster.velocity = new Vector2 (rigidMonster.velocity.x - 10 / (1 + defense), 0);
    }
}