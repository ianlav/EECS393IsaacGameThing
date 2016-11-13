using System.Collections;
using UnityEngine;

public class CharacterModel : MonoBehaviour {

    public int hp = 100;

    private UIController ui;

    protected void Start()
    {
        ui = FindObjectOfType<UIController>();
    }

    public void takeDamage(int dam)
    {
		if (dam < hp)
			hp -= dam;
		else {
			hp = 0;
		}
        ui.thingTookDamage(transform.position, dam);
    }

	public int getHp(){
		return hp;
	}
}
