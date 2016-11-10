using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreController : MonoBehaviour {

	UIController controller;
	Text scoreDisplay;

	// Use this for initialization
	void Start () {
		scoreDisplay = GetComponent<Text>();
		scoreDisplay.text = "Score: "; //+ controller.getCurrentScore();
	}
	
	// Update is called once per frame
	void Update () {
		scoreDisplay.text = "Score: "; //+ controller.getCurrentScore();
	}
}
