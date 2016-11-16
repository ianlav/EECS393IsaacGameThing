using UnityEngine;
using System.Collections;

//Temporary item spawning for the demo
public class ItemMaker : MonoBehaviour {

    public int inverseItemSpawnRate;
    public int lowRand, highRand;
    public Item[] items;

    private int numPlatformsSinceItem = 0;

    public Item madePlatform()
    {
        numPlatformsSinceItem++;
        if(numPlatformsSinceItem + (int)Random.Range(lowRand, highRand) > inverseItemSpawnRate)
        {
            return items[(int)Random.Range(0, items.Length)];
        }
        return null;
    }

    public void maybeSpawnItem(Vector2 pos)
    {
        Item item = madePlatform();
        if(item != null)
        {
            numPlatformsSinceItem = 0;
            Instantiate(item, pos, Quaternion.identity);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
