using UnityEngine;
//using System.Collections;

public class Platform : MonoBehaviour {

    public PlatformMaker maker;
    public Transform leftSide, rightSide; //each platform needs these. They identify the left and right sides of the platform
	public EnemyMaker enemyMaker;

	// Use this for initialization
	void Start () {
        maker = FindObjectOfType<PlatformMaker>();
		enemyMaker = FindObjectOfType<EnemyMaker>();
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
			enemyMaker.makeInRange();
            Destroy(gameObject, 3);
        }
    }
}
