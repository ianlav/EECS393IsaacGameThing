using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreController : MonoBehaviour {
    
	Text scoreDisplay;
    static int currentScore;
    public static string currentName;
    public static string currentDate;

	// Use this for initialization
	void Start () {
		scoreDisplay = GetComponent<Text>();
		scoreDisplay.text = "Score: " + currentScore;
	}
	
	// Update is called once per frame
	void Update () {
		scoreDisplay.text = "Score: " + currentScore;
	}
    
    public static void setCurrentScore(int newScore)
    {
        currentScore = newScore;
    }

    public static int getCurrentScore()
    {
        return currentScore;
    }

    public static void saveCurrentScore()
    {
        currentDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        Scoring.AddRecord(currentName, currentDate, currentScore);
        if (Scoring.WriteScores())
            currentScore = 0;
    }
}
