using UnityEngine;
using System.Collections;

//an abstract parent class for all weapons
public abstract class Weapon : MonoBehaviour {

    public Transform bulletOrigin;
    protected PlayerMovement player;
    public Vector2 aimVector;
    public int damage;
    public int numProjectiles;
    public Bullet[] bullets;

    protected float timeSinceFired;
    protected float timeBetweenShots;
    protected float timeSinceBulletPattenUpdate;
    protected float timeBetweenBulletPatternUpdates;

    public abstract void fire();
    public abstract string getName();

    //gets the vector pointing from origin to the mouse
    protected Vector2 getAimVector(Vector2 origin)
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 aimPos = Camera.main.ScreenToWorldPoint(mousePos);
        return aimPos - origin; 
    }

    public virtual void Start()
    {
        gameObject.tag = "Weapon";
        gameObject.layer = LayerMask.NameToLayer("Weapon");
        //gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1; //should keep gun above player
        player = (PlayerMovement)FindObjectOfType(typeof(PlayerMovement));
        bulletOrigin = transform;
    }

    protected virtual void Update()
    {
        //increments the fire rate counter(s)
        timeSinceFired += Time.deltaTime;
        timeSinceBulletPattenUpdate += Time.deltaTime;
        //grab aim vector so bullets know where to go
        aimVector = getAimVector(bulletOrigin.position);
        //rotate weapon to point at mouse
        float angle = aimVector.Angle();
        if (angle > 90 || angle < -90) //aiming backwards
            transform.rotation = Quaternion.Euler(0, 180, 180 - aimVector.Angle());
        else
            transform.rotation = Quaternion.Euler(0, 0, aimVector.Angle());
        //update bullet pattern
        if (timeSinceBulletPattenUpdate > timeBetweenBulletPatternUpdates)
        {
            updateBulletPattern();
            timeSinceBulletPattenUpdate = 0;
        }
    }

    protected virtual void updateBulletPattern()
    {
        //no pattern to shots by default
    }
}
