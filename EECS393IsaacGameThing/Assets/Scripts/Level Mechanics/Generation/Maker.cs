using UnityEngine;
using System.Collections;

public abstract class Maker : MonoBehaviour {

	public PlayerMovement player;
	public Rigidbody2D playerRigid;
	public float minDistX, maxDistX;
	public float minDistY, maxDistY;

	public abstract bool makeInRange();

	void Start()
	{

	}


}