using UnityEngine;
using System.Collections;

public class PersistentBullet : Bullet {

    new void Start()
    {
        base.Start();
        damage = 10;
        speed = 20;
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Monster"))
        {
            Enemy e = (Enemy)col.gameObject.GetComponent(typeof(Enemy));
            //col.gameObject.SendMessage("takeDamage", damage); //alternative 1
            //e.takeDamage(damage); //alternative 2
            if (e != null)
                hit(e);
        }
        if (col.gameObject.CompareTag("Floor"))
        {
            Platform p = (Platform)col.gameObject.GetComponent(typeof(Platform));
            if (p != null)
                hit(p);
        }
    }

    //default enemy hit: damage enemy
    public override void hit(Enemy enemy)
    {
        enemy.takeDamage(damage);
    }

    //default platform hit: just destroy bullet
    public override void hit(Platform platform)
    {
    }
}
