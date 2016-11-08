using UnityEngine;
//using System.Collections;

//abstract implementation of bullets
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Bullet : MonoBehaviour {
    public int damage;
    public float speed;
    public Vector2 direction;
    public float timeToExist;

    private Rigidbody2D rigid;

    protected void Start()
    {
        timeToExist = 2;
        rigid = GetComponent<Rigidbody2D>();
        Destroy(gameObject, timeToExist); //we want to destroy the bullet x seconds after we even fire it
    }

    //Keep the bullet going towards its destination. Direction can be updated to curve the bullet
    protected void Update()
    {
        //normalize the direction vector, then move the bullet in that direction. called every frame
        direction.Normalize();
        rigid.velocity = direction * speed;
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        //bullets destroy themselves on collision with anything else
        if (!col.gameObject.CompareTag(gameObject.tag) && !col.gameObject.CompareTag("Weapon") && !col.gameObject.CompareTag("Player"))
            Destroy(gameObject);
        if (col.gameObject.CompareTag("Monster"))
        {
            Enemy e = (Enemy)col.gameObject.GetComponent(typeof(Enemy));
            //col.gameObject.SendMessage("takeDamage", damage); //alternative 1
            //e.takeDamage(damage); //alternative 2
            if(e != null)
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
    public virtual void hit(Enemy enemy)
    {
        enemy.takeDamage(damage);
        Destroy(gameObject); //destroy bullet
    }

    //default platform hit: just destroy bullet
    public virtual void hit(Platform platform)
    {
        Destroy(gameObject); //destroy bullet
    }
}
