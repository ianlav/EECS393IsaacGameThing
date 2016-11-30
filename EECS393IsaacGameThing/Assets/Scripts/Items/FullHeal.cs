/* This item restores full hp to player on contact */ 

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic; 

public class FullHeal : Item {

    PlayerMovement player;

	new void Start () {
        base.Start();
        player = FindObjectOfType<PlayerMovement>();
	}

	new void Update () {
        base.Update();
	}

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player.hp = 100;
            Destroy(gameObject);
            ui.displayPopUpText("+100", player.transform.position);
        }
    }

}