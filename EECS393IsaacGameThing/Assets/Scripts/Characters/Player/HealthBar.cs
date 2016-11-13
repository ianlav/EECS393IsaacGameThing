using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

    private PlayerMovement player;
    private Slider slider;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerMovement>();
        slider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        slider.value = player.getHp();
	}
}
