using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour {

    protected UIController ui;
    protected string popupText;

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
           // ui.displayPopUpText(popupText, transform.position);
        }
    }
}
