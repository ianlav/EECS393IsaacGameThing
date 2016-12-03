/* This item increases player's speed for 10 seconds */ 

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic; 

public class FastShoes : Item { 

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
			double fastHorizontalSpeed = (double)player.horizontalSpeed*1.5; 
			double fastJumpSpeed = (double)player.jumpSpeed*1.5; 
			float originalHorizontalSpeed = player.horizontalSpeed; 
			float originalJumpSpeed = player.jumpSpeed; 
			
			if (Time.time <= endTime) { 
				player.horizontalSpeed = (float)fastHorizontalSpeed; 
				player.jumpSpeed = (float)fastJumpSpeed; 
			} 
			
			player.horizontalSpeed = originalHorizontalSpeed; 
			player.jumpSpeed = originalJumpSpeed; 
		}
		
	} 
} 