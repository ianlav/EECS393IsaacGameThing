using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;

public class HighScoreScreen : MonoBehaviour {

    public Text highScores;

	// Use this for initialization
	void Start () {
        ReadOnlyCollection<Scoring.ScoreRecord> scores = Scoring.GetScores();
        print(scores.Count);
        for (int i = 0; i < scores.Count; i++)
        {
            print(scores[i].name);
            highScores.text += (scores[i].name + ":  " + scores[i].score + "\n");
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
