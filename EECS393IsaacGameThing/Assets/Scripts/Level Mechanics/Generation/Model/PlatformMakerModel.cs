using UnityEngine;
using System.Collections;

public class PlatformMakerModel {

    private float gravity, horizSpeed, vertSpeed;

    public PlatformMakerModel(float grav, float horizontalSpeed, float vertSpeed)
    {
        this.gravity = grav;
        this.horizSpeed = horizontalSpeed;
        this.vertSpeed = vertSpeed;
    }

    public Vector2 getPositionInRange(float maxX, float minX, float maxY, float minY)
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        //this is the x value of the highest point of the jump
        float midPointX = vertSpeed * horizSpeed / gravity;

        //if the spot is before and under the high point, we're good.
        //Otherwise, check if its within the jump curve
        while (!((x < midPointX && y < getMaxYFromX(midPointX)) || y < getMaxYFromX(x)))
        {
            y = Random.Range(minY, maxY);
            x = Random.Range(minX, maxX);
        }
        return new Vector2(x, y);
    }

    //this generates the y from a given x using the players jump velocity, gravity, and move speed. Physics 1 in action
    public float getMaxYFromX(float x)
    {
        //this is the position equation, with the player's running speed factored in for t
        return (-0.5f * gravity * Mathf.Pow((x / horizSpeed), 2)) + (vertSpeed * x / horizSpeed);
    }
}
