using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    PlayerMovement player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerMovement>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = Vector2.Lerp((Vector2)player.transform.position, (Vector2)transform.position, 0.95f);
        transform.position = new Vector3(transform.position.x, transform.position.y, -2);
	}
}
