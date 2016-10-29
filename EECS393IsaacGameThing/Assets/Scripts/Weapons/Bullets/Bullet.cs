using UnityEngine;
using System.Collections;

//abstract implementation of bullets
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Bullet : MonoBehaviour {
    public int damage;
    public float speed;
    public Vector2 direction;
    public float timeToExist;

    private Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Destroy(gameObject, timeToExist); //we want to destroy the bullet x seconds after we even fire it
    }

    //Keep the bullet going towards its destination. Direction can be updated to curve the bullet
    void Update()
    {
        //normalize the direction vector, then move the bullet in that direction. called every frame
        direction.Normalize();
        rigid.velocity = direction * speed;
    }

    public abstract void hit(Enemy other);
}
