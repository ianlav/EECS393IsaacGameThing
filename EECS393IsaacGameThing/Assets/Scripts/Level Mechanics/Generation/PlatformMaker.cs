using UnityEngine;
using System.Collections;

public class PlatformMaker : MonoBehaviour {

    public Platform newestPlatform;

    public Platform[] platformOptions;
    public PlayerMovement player;
    public Rigidbody2D playerRigid;

    public float minDistX, maxDistX;
    public float minDistY, maxDistY;

    void Start()
    {
        playerRigid = player.GetComponent<Rigidbody2D>();

        //we'll start with the initial platform, and make 4 more
        makeRandomPlatformInRange();
        makeRandomPlatformInRange();
        makeRandomPlatformInRange();
        makeRandomPlatformInRange();
    }

    public void makeRandomPlatformInRange()
    {
        float x = Random.Range(minDistX, maxDistX);
        float y = Random.Range(minDistY, maxDistY);

        //this is the x value of the highest point of the jump
        float midPointX = player.jumpSpeed * player.horizontalSpeed / (playerRigid.gravityScale * -9.81f);

        //if the spot is before and under the high point, we're good.
        //Otherwise, check if its within the jump curve
        while (!((x < midPointX && y < getMaxYFromX(midPointX)) || y < getMaxYFromX(x)))
        {
            y = Random.Range(minDistY, maxDistY);
            x = Random.Range(minDistX, maxDistX);
        }

        //make the platform, using the generated values offset from the right side of the platform
        makeRandomPlatform(new Vector2(newestPlatform.rightSide.position.x + x,  newestPlatform.rightSide.position.y + y));
    }

    //selects a random platform from the list and places it
	void makeRandomPlatform(Vector2 pos)
    {
        makePlatform(platformOptions[(int)Random.Range(0, platformOptions.Length)], pos);
    }

    void makePlatform(Platform plat, Vector2 pos)
    {
        newestPlatform = Instantiate(plat, 
            pos - (Vector2)plat.leftSide.position + (Vector2)plat.transform.position //the set position, offset by the distance between the left side and center. Because localPosition wont give me that
            , Quaternion.identity) as Platform;
    }

    //this generates the y from a given x using the players jump velocity, gravity, and move speed. Physics 1 in action
    float getMaxYFromX(float x)
    {
        //this is the position equation, with the player's running speed factored in for t
        return (-0.5f * 9.81f * playerRigid.gravityScale * Mathf.Pow((x / player.horizontalSpeed), 2)) + (player.jumpSpeed * x / player.horizontalSpeed);
    }
}
