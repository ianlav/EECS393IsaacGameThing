/* This item increases player's speed for 10 seconds */ 

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic; 

public class FastShoes : Item { 
	
	new void Start () {
        base.Start();
	}

	new void Update () {
        base.Update();
	}

	protected override void OnTriggerEnter2D(Collider2D col) { 
		
		if (col.CompareTag("Player")) { 
			private float endTime = Time.time + 10; 
			private float fastHorizontalSpeed = PlayerMovement.horizontalSpeed*1.5; 
			private float fastJumpSpeed = PlayerMovement.jumpSpeed*1.5; 
			private float originalHorizontalSpeed = PlayerMovement.horizontalSpeed; 
			private float originalJumpSpeed = PlayerMovement.jumpSpeed; 
			
			if (Time.time <= endTime) { 
				PlayerMovement.horizontalSpeed = fastHorizontalSpeed; 
				PlayerMovement.jumpSpeed = fastJumpSpeed; 
			} 
			
			PlayerMovement.horizontalSpeed = originalHorizontalSpeed; 
			PlayerMovement.jumpSpeed = originalJumpSpeed; 
		}
		
	} 
} 