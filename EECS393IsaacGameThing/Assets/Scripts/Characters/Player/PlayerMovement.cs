using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic; 

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : CharacterModel {

    //any public values can be modified easily in the inspector
    public float horizontalSpeed;
    public float horizontalAccel;
    public float jumpSpeed;
    public List<Weapon> weapons;
    public float contactDamageRate;
    public float curPlatY = 0;

    private Rigidbody2D rigid;
    private bool isJumping = true;
	private CharacterModel character;
    private float contactDamageCounter = 0;

	// Use this for initialization
	new void Start () {
        base.Start();
        gameObject.tag = "Player";
        rigid = GetComponent<Rigidbody2D>();
        ScoreController.setCurrentScore(0);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire"))
        {
            //if the player has multiple weapons, fire them all
            StartCoroutine(fireWeapons(0.1f));
        }

        //just adds a vertical velocity
        if (Input.GetButton("Jump") && !isJumping)
        {
            isJumping = true;
            rigid.velocity += new Vector2(0, jumpSpeed);
        }

        if (Input.GetButtonUp("Jump") && rigid.velocity.y > 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
        }

        if (getHp() <= 0)
        {
            Destroy(gameObject);
        }
    }
   
    // Fixed Update is called every fixed amount of time
    void FixedUpdate()
    {
        //basic lateral movement. Linsearly interpolates the velocity between current and intended velocity.
        if(Input.GetButton("MoveLeft"))
        {
            rigid.velocity = new Vector2(Mathf.Lerp(rigid.velocity.x, -horizontalSpeed, horizontalAccel), rigid.velocity.y);
        } else if (Input.GetButton("MoveRight")) {
            rigid.velocity = new Vector2(Mathf.Lerp(rigid.velocity.x, horizontalSpeed, horizontalAccel), rigid.velocity.y);
        }

        ScoreController.setCurrentScore(Mathf.Max((int)transform.position.x, ScoreController.getCurrentScore()));

        if(transform.position.y < curPlatY - 20)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator fireWeapons(float waitTime)
    {
        foreach (Weapon wep in weapons)
        {
            wep.fire();
            yield return new WaitForSeconds(waitTime);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //when the player touches a platform, it can jump again
		if (col.gameObject.CompareTag ("Platform")) { 
			isJumping = false;

		}
        //if it runs into the monster, destroy it
        if (col.gameObject.CompareTag("Monster")) {
			print ("MONSTER COLLISION!");
            Destroy(gameObject);
        }
        //if it runs into an enemy, take damage it
        if (col.gameObject.CompareTag("Enemy"))
        {
            print(col.collider.bounds.max.y + " " + col.contacts[0].point.y);
            if((col.collider.bounds.max.y - 0.1f) < col.contacts[0].point.y)
                isJumping = false;
            print("ENEMY COLLISION!");
            Enemy e = (Enemy)col.gameObject.GetComponent(typeof(Enemy));
            contactDamageCounter = 100000;
            takeContactDamage(e);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            print("ENEMY COLLISION!");
            Enemy e = (Enemy)col.gameObject.GetComponent(typeof(Enemy));
            contactDamageCounter = 100000; //arbitrary number to ensure the first time an enemy touches the player, player takes damage
            takeContactDamage(e);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Enemy e = (Enemy)col.gameObject.GetComponent(typeof(Enemy));
            takeContactDamage(e);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            print("ENEMY COLLISION! " + col.name);
            Enemy e = (Enemy)col.gameObject.GetComponent(typeof(Enemy));
            takeContactDamage(e);
        }
    }

    void takeContactDamage(Enemy e)
    {
        if (e != null && contactDamageCounter > contactDamageRate)
        {
            contactDamageCounter = 0;
            takeDamage(e.baseDamage);
        }
        contactDamageCounter += Time.deltaTime;
    }

    void OnDestroy()
    {
        SceneManager.LoadScene("Game Over", LoadSceneMode.Single);
    }
}
