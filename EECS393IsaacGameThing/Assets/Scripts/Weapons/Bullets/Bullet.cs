using UnityEngine;
using System.Collections;

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
        Destroy(gameObject, timeToExist);
    }

    //Keep the bullet going towards its destination. Direction can be updated to curve the bullet
    void Update()
    {
        direction.Normalize();
        rigid.velocity = direction * speed;
    }

    public abstract void hit(Rigidbody2D other); //TO-DO make other an enemy instance
}
