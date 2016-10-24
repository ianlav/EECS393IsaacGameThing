using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    //any public values can be modified easily in the inspector
    public float horizontalSpeed;
    public float horizontalAccel;
    public float jumpSpeed;
    public Weapon[] weapons;

    private Rigidbody2D rigid;
    private bool isJumping = true;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire"))
        {
            Debug.Log("sup");
            foreach (Weapon wep in weapons)
            {
                wep.fire();
            }
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
        if (Input.GetButton("Jump") && !isJumping)
        {
            isJumping = true;
            rigid.velocity += new Vector2(0, jumpSpeed);
        }
    }

    //is run whenever this object enters a collision with another 2D object. Self explanatory but yeah
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Floor")) //this checks the objects tag (directly below its name in the inspector panel). Just a quick way to check what something is
        {
            isJumping = false;
        }
    }
}
