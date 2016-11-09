using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour {

	// Use this for initialization
	protected virtual void Start () {
        gameObject.tag = "Item";
        gameObject.layer = LayerMask.NameToLayer("Item");
    }
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}
}
