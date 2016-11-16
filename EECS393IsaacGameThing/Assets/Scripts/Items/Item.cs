using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour {

    private UIController ui;

	// Use this for initialization
	protected virtual void Start () {
        gameObject.tag = "Item";
        ui = FindObjectOfType<UIController>();
    }
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            ui.displayPopUpText("This is temporary place holder text", transform.position);
        }
    }
}
