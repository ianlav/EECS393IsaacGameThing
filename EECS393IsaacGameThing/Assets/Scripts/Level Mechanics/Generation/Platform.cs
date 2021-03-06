﻿using UnityEngine;
using System.Collections;

//a platform to be placed in the scene
public class Platform : MonoBehaviour {

    public PlatformMaker maker;
    public Transform leftSide, rightSide; //each platform needs these. They identify the left and right sides of the platform
	public EnemyMaker enemyMaker;

    bool exitedTrigger = false;

	// Use this for initialization
	void Start () {
        maker = FindObjectOfType<PlatformMaker>();
        gameObject.tag = "Platform";
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //when the platform leaves the player's trigger, we destroy it and make a new one
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("LevelTrigger"))
        {
            maker.makeRandomPlatformInRange();
            exitedTrigger = true;
        }
    }

    void OnDestroy()
    {
        if(!exitedTrigger)
            maker.makeRandomPlatformInRange();
    }
}
