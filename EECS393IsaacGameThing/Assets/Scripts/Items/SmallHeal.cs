/* This item adds 10 hp to player on contact */ 

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic; 

using System;

public class SmallHeal : Item{

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
            player.hp += 10;
            Destroy(gameObject);
            ui.displayPopUpText("+10", player.transform.position);
        }
    }

}