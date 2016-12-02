using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Monster : MonoBehaviour {

	public float updatesToUpgrade;
	private float updateClock;
	public float maxVelocity; //Highest possible speed of monster. Increases as game progresses.
	public float acceleration; //Ability for monster to return to speed when hit. Increases.
	public float defense; //Modifier for the amount that damage slows down monster. Increases.
	private Rigidbody2D rigidMonster;
    private PlayerMovement player;
    private UIController ui;

	void Start () {
		rigidMonster = GetComponent<Rigidbody2D>();
        gameObject.tag = "Monster";
        player = FindObjectOfType<PlayerMovement>();
        ui = FindObjectOfType<UIController>();
    }

	public void takeDamage(int damage){
		maxVelocity -= damage/defense; //slow the monster when hit by projectile
	}

	void Update () {
		if(rigidMonster.velocity.x<maxVelocity){
			rigidMonster.velocity = new Vector2(rigidMonster.velocity.x+acceleration, rigidMonster.velocity.y);
			if(rigidMonster.velocity.x > maxVelocity){rigidMonster.velocity=new Vector2(maxVelocity, rigidMonster.velocity.y);}
		}
        rigidMonster.velocity = new Vector2(rigidMonster.velocity.x, player.transform.position.y - transform.position.y); //keeps the monster on the same y plane
		if (updateClock >= updatesToUpgrade) {
			updateClock = 0;
			UpgradeMonster ();
		} else {
			updateClock += Time.deltaTime;
		}
	}

	void UpgradeMonster(){
        maxVelocity += 0.5f;
		acceleration += 0.1f;
		defense += 0.5f;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet"))
        {
            Bullet bul = col.GetComponent<Bullet>();
			if (!bul.enemyMode) {
				rigidMonster.velocity = new Vector2 (rigidMonster.velocity.x - bul.damage / (1 + defense), 0);
				ui.thingTookDamage (col.transform.position, (int)(bul.damage / (1 + defense)));
			}
        }
        if(!col.CompareTag("LevelTrigger"))
            Destroy(col.gameObject);
    }
}