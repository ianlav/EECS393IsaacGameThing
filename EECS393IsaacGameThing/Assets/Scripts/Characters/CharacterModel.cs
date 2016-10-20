using System.Collections;

public class CharacterModel {

    public int hp = 100;

    public void takeDamage(int dam)
    {
        hp -= dam;
    }
}
