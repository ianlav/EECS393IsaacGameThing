using System.Collections;

public class CharacterModel {

    public int hp = 100;

    public void takeDamage(int dam)
    {
        if (dam < hp)
            hp -= dam;
        else
            hp = 0;
    }
}
