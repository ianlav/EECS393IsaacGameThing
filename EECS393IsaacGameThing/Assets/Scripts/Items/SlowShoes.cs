/* This item decreases player's speed for 10 seconds */ 

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic; 

public class SlowShoes : Item { 

	new void Start () {
        base.Start();
	}

	new void Update () {
        base.Update();
	}
	
	void OnCollisionEnter2D(Collision2D col) {
	
		if (col.CompareTag("Player")) { 
			private float endTime = Time.time + 10; 
			private float slowHorizontalSpeed = PlayerMovement.horizontalSpeed*0.5; 
			private float slowJumpSpeed = PlayerMovement.jumpSpeed*0.5; 
			private float originalHorizontalSpeed = PlayerMovement.horizontalSpeed; 
			private float originalJumpSpeed = PlayerMovement.jumpSpeed; 
			
			if (Time.time <= endTime) { 
				PlayerMovement.horizontalSpeed = slowHorizontalSpeed; 
				PlayerMovement.jumpSpeed = slowJumpSpeed; 
			} 
			
			PlayerMovement.horizontalSpeed = originalHorizontalSpeed; 
			PlayerMovement.jumpSpeed = originalJumpSpeed; 
		}
	
	}

} 