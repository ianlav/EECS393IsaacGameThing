using UnityEngine;
using System.Collections;

public class PlatformMaker : MonoBehaviour {

    public Platform newestPlatform;

    public Platform[] platformOptions;
    public PlayerMovement player;
    public Rigidbody2D playerRigid;

    public float minDistX, maxDistX;
    public float minDistY, maxDistY;

    private PlatformMakerModel model;
	private EnemyMaker enemyMaker;
    private ItemMaker itemMaker;

    void Start()
    {
		
        playerRigid = player.GetComponent<Rigidbody2D>();
		enemyMaker = FindObjectOfType<EnemyMaker>();
        itemMaker = FindObjectOfType<ItemMaker>();
        model = new PlatformMakerModel(playerRigid.gravityScale * 9.81f, player.horizontalSpeed, player.jumpSpeed);

        //we'll start with the initial platform, and make 4 more
        makeRandomPlatformInRange();
        makeRandomPlatformInRange();
        makeRandomPlatformInRange();
        makeRandomPlatformInRange();
    }

    public void makeRandomPlatformInRange()
    {
        Vector2 pos = model.getPositionInRange(maxDistX, minDistX, maxDistY, minDistY);

        //make the platform, using the generated values offset from the right side of the platform
        makeRandomPlatform(new Vector2(newestPlatform.rightSide.position.x + pos.x,  newestPlatform.rightSide.position.y + pos.y));
    }

    //selects a random platform from the list and places it
	void makeRandomPlatform(Vector2 pos)
    {
        makePlatform(platformOptions[(int)Random.Range(0, platformOptions.Length)], pos);
    }

    void makePlatform(Platform plat, Vector2 pos)
    {
        Vector2 offsetPos = pos - (Vector2)plat.leftSide.position + (Vector2)plat.transform.position;//the set position, offset by the distance between the left side and center. Because localPosition wont give me that. Also offset up one unit so we dont spawn them in the platform
        newestPlatform = Instantiate(plat, 
            offsetPos
            , Quaternion.identity) as Platform;
		enemyMaker.makeRandomEnemy (plat, pos);
        itemMaker.maybeSpawnItem(offsetPos + new Vector2(0,1));
    }
}
