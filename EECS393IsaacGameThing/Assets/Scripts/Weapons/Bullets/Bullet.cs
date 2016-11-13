using UnityEngine;
//using System.Collections;

//abstract implementation of bullets
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Bullet : MonoBehaviour {
    public int damage;
    public float speed;
    public Vector2 direction;
    public float timeToExist;
    public bool enemyBullet;

    private Rigidbody2D rigid;

    public virtual void Start()
    {
        gameObject.tag = "Bullet";
        gameObject.layer = LayerMask.NameToLayer("Bullet");
        if (timeToExist == 0)
            timeToExist = 1;
        rigid = GetComponent<Rigidbody2D>();
        Destroy(gameObject, timeToExist); //we want to destroy the bullet x seconds after we even fire it
    }

    //Keep the bullet going towards its destination. Direction can be updated to curve the bullet
    public virtual void Update()
    {
        //normalize the direction vector, then move the bullet in that direction. called every frame
        direction.Normalize();
        rigid.velocity = direction * speed;
    }

    //protected virtual void OnCollisionEnter2D(Collision2D col)
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (enemyMode) {
            if (col.gameObject.CompareTag("Player"))
            {
                PlayerMovement player = (PlayerMovement)col.gameObject.GetComponent(typeof(PlayerMovement));
                if(player != null)
                    hit(player);
            }
        }
        else {
            if (col.gameObject.CompareTag("Enemy"))
            {
                Enemy e = (Enemy)col.gameObject.GetComponent(typeof(Enemy));
                if(e != null)
                    hit(e);
            }
        }
        else if (col.gameObject.CompareTag("Platform"))
        {
            Platform p = (Platform)col.gameObject.GetComponent(typeof(Platform));
            if (p != null)
                hit(p); 
        }
    }

    public virtual void hit(PlayerMovement player)
    {
        player.takeDamage(damage);
        Destroy(gameObject); //destroy bullet
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

