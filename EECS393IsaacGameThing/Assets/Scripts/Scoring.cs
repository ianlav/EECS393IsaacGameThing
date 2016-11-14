using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;

public class Scoring : MonoBehaviour {

    string FILEPATH = "scores.sav"; //can be modified in constructor to write to appdata directory if needed
    const string DELIMITER = " -=- ";

    public class ScoreRecord {
        public string name;
        public string date;
        public double score;
        public ScoreRecord(string name, string date, double score){
            this.name = name;
            this.date = date;
            this.score = score;
        }
        public bool Equals(ScoreRecord record)
        {
            if (!record.name.Equals(name))
                return false;
            if (!record.date.Equals(date))
                return false;
            if (!record.score.Equals(score))
                return false;
            return true;
        }
        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}", name, DELIMITER, date, DELIMITER, score);
        }
    }

    List<ScoreRecord> scoreList = new List<ScoreRecord>();

	// Use this for initialization
	public void Start () {
        //FILEPATH = Application.persistentDataPath + "scores.sav"; //might need this for permissions, writes to appdata directory
        ReadScores(); //loads saved scores from file if available
	}

    public void clearRecords() {
        scoreList.Clear();
    }

    public void AddRecord(ScoreRecord record){
        scoreList.Add(record);
    }

    public void AddRecord(string name, string date, double score){
        ScoreRecord r = new ScoreRecord(name, date, score);
        scoreList.Add(r);
    }

    public void AddRecords(params ScoreRecord[] records){
        foreach (ScoreRecord s in records)
            scoreList.Add(s);
    }

    public bool ReadScores(){
        try{
            if (File.Exists(FILEPATH)){
                scoreList.Clear();
                string[] savedScores = File.ReadAllLines(FILEPATH);
                string[] tokens;
                for(int i=0; i < savedScores.Length; i++)
                {
                    tokens = savedScores[i].Split(new[] { DELIMITER }, StringSplitOptions.None);
                    if (tokens.Length == 3)
                    {
                        AddRecord(tokens[0], tokens[1], double.Parse(tokens[2],
                            System.Globalization.CultureInfo.InvariantCulture));
                    }
                }
                return true;
            }
        } catch (Exception e){
            Console.WriteLine(e.GetBaseException());
        }
        return false;
    }

    public bool WriteScores(){
        try{
            //if (!File.Exists(FILEPATH))
            //    File.Create(FILEPATH);
            string[] allLines = new string[scoreList.Count];
            for (int i= 0; i < scoreList.Count; i++)
            {
                allLines[i] = string.Format("{0}{1}{2}{3}{4}", scoreList[i].name, DELIMITER,
                    scoreList[i].date, DELIMITER, scoreList[i].score.ToString());
            }
            File.WriteAllLines(FILEPATH, allLines);
            return true;
        } catch (Exception e){
            Console.WriteLine(e.GetBaseException());
        }
        return false;
    }

    public  ReadOnlyCollection<ScoreRecord> GetScores(){
        return scoreList.AsReadOnly();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
