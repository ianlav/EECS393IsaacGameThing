using System.Collections;
using UnityEngine;

public class CharacterModel : MonoBehaviour {

    public int hp = 100;

    public void takeDamage(int dam)
    {
		if (dam < hp)
			hp -= dam;
		else {
			hp = 0;
		}
    }

	public int getHp(){
		return hp;
	}
}
