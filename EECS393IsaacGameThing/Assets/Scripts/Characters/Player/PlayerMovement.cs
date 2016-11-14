using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : CharacterModel {

    //any public values can be modified easily in the inspector
    public float horizontalSpeed;
    public float horizontalAccel;
    public float jumpSpeed;
    public Weapon[] weapons;

    private Rigidbody2D rigid;
    private bool isJumping = true;
	private CharacterModel character;

	// Use this for initialization
	new void Start () {
        base.Start();
        gameObject.tag = "Player";
        gameObject.layer = LayerMask.NameToLayer("Player");
        rigid = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire"))
        {
            //if the player has multiple weapons, fire them all
            foreach (Weapon wep in weapons)
            {
                wep.fire();
            }
        }

        if(getHp() <= 0)
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
        } else {
            rigid.velocity = new Vector2(Mathf.Lerp(rigid.velocity.x, 0, horizontalAccel), rigid.velocity.y);
        }

        //just adds a vertical velocity
        // TO-DO make this a variable height jump, ie a tap is a short jump than a hold
        if (Input.GetButton("Jump") && !isJumping)
        {
            isJumping = true;
            rigid.velocity += new Vector2(0, jumpSpeed);
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
        //if it runs into an enemy, destroy it
        if (col.gameObject.CompareTag("Enemy"))
        {
            print("ENEMY COLLISION!");
            Enemy e = (Enemy)col.gameObject.GetComponent(typeof(Enemy));
            if (e != null)
                takeDamage(e.baseDamage);
        }
    }

    void OnDestroy()
    {
        SceneManager.LoadScene("Game Over", LoadSceneMode.Single);
    }
}
