﻿using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    public PlatformMaker maker;
    public Transform leftSide, rightSide; //each platform needs these. They identify the left and right sides of the platform

	// Use this for initialization
	void Start () {
        maker = FindObjectOfType<PlatformMaker>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //when the platform leaves the trigger, we destroy it and make a new one
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("LevelTrigger"))
        {
            maker.makeRandomPlatformInRange();
            Destroy(gameObject, 3);
        }
    }
}
