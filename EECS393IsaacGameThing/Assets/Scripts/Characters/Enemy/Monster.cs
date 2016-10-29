using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Monster : MonoBehaviour {

	public int maxVelocity = 10; //Highest possible speed of monster. Increases as game progresses.
	public float acceleration = 2.0f; //Ability for monster to return to speed when hit. Increases.
	public int defense = 5; //Modifier for the amount that damage slows down monster. Increases.
	private Rigidbody2D rigidMonster;

	void Start () {
		rigidMonster = GetComponent<Rigidbody2D>();
	}

	public void takeDamage(int damage){
		maxVelocity -= damage/defense; //slow the monster when hit by projectile
	}

	void Update () {
		if(rigidMonster.velocity.x<maxVelocity){
			rigidMonster.velocity = new Vector2(rigidMonster.velocity.x+acceleration, 0);
			if(rigidMonster.velocity.x > maxVelocity){rigidMonster.velocity=new Vector2(maxVelocity,0);}
		}
	}

	void UpgradeMonster(){
		maxVelocity = maxVelocity + 1;
		acceleration=acceleration+0.25f;
		defense=defense+1;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
            Destroy(col.gameObject);
    }
}