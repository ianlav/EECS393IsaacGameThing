using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float horizontalSpeed;
    public float horizontalAccel;

    private Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
   
    // Fixed Update is called every fixed amount of time
    void FixedUpdate()
    {
        if(Input.GetButton("MoveLeft"))
        {
            rigid.velocity = new Vector2(Mathf.Lerp(rigid.velocity.x, -horizontalSpeed, horizontalAccel), rigid.velocity.y);
        } else if (Input.GetButton("MoveRight"))
        {
            rigid.velocity = new Vector2(Mathf.Lerp(rigid.velocity.x, horizontalSpeed, horizontalAccel), rigid.velocity.y);
        } else {
            rigid.velocity = new Vector2(Mathf.Lerp(rigid.velocity.x, 0, horizontalAccel), rigid.velocity.y);
        }
    }
}
