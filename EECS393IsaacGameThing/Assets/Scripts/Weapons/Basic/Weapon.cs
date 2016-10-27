﻿using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour {

    public int damage;
    public float timeBetweenShots;
    public int numProjectiles;

    protected float timeSinceFired;

    public abstract void fire();

    //gets the vector pointing from origin to the mouse
    protected Vector2 getAimVector(Vector2 origin)
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 aimPos = Camera.main.ScreenToWorldPoint(mousePos);
        return aimPos - origin;
    }
}