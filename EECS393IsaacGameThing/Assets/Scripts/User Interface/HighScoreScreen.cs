using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;

public class HighScoreScreen : MonoBehaviour {

    public Text highScores;
    public int maxRecords = 10; //only show top N scores

	// Use this for initialization
	void Start () {
        Scoring.ReadScores(); //load high scores from file
        ReadOnlyCollection<Scoring.ScoreRecord> scores = Scoring.GetScores();
        int max = maxRecords > scores.Count ? scores.Count : maxRecords;
        for (int i = 0; i < max; i++)
        {
            print(scores[i].name);
            highScores.text += string.Format("{0}  {1}  ({2})\n", 
                scores[i].score.ToString().PadLeft(scores[i].date.Length), //centering added to padding
                scores[i].name.PadRight(25), 
                scores[i].date);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
