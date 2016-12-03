/* This item decreases player's speed for 10 seconds */ 

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic; 

public class SlowShoes : Item { 

	PlayerMovement player; 

	new void Start () {
        base.Start();
	}

	new void Update () {
        base.Update();
	}
	
	protected override void OnTriggerEnter2D(Collider2D col) { 
	
		if (col.CompareTag("Player")) { 
			float endTime = Time.time + 10; 
			double slowHorizontalSpeed = (double)player.horizontalSpeed*0.5; 
			double slowJumpSpeed = (double)player.jumpSpeed*0.5; 
			float originalHorizontalSpeed = player.horizontalSpeed; 
			float originalJumpSpeed = player.jumpSpeed; 
			
			if (Time.time <= endTime) { 
				player.horizontalSpeed = (float)slowHorizontalSpeed; 
				player.jumpSpeed = (float)slowJumpSpeed; 
			} 
			
			player.horizontalSpeed = originalHorizontalSpeed; 
			player.jumpSpeed = originalJumpSpeed; 
		}
	
	}

} 