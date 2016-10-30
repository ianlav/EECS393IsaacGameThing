using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;

public class Scoring : MonoBehaviour {

    const string FILEPATH = "scores.sav";
    const string DELIMITER = " -=- ";

    public class ScoreRecord{
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
            if (record.name != name)
                return false;
            if (record.date != date)
                return false;
            if (record.score != score)
                return false;
            return true;
        }
    }

    List<ScoreRecord> scoreList = new List<ScoreRecord>();
    StreamReader reader;
    StreamWriter writer;

	// Use this for initialization
	void Start () {
        ReadScores(); //loads saved scores from file if available
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
                scoreList = new List<ScoreRecord>();
                reader = new StreamReader(FILEPATH);
                string[] tokens;
                while (reader.Peek() >= 0){
                    tokens = reader.ReadLine().Split(new[]{ DELIMITER }, StringSplitOptions.None);
                    if(tokens.Length == 3){
                        AddRecord(tokens[0], tokens[1], double.Parse(tokens[2], 
                            System.Globalization.CultureInfo.InvariantCulture));
                    }
                }
            }
            return true;
        } catch (Exception e){
            Console.WriteLine(e.GetBaseException());
            return false;
        }
    }

    public bool WriteScores(){
        try{
            if (File.Exists(FILEPATH)){
                scoreList = new List<ScoreRecord>();
                writer = new StreamWriter(FILEPATH);
                string line;
                foreach(ScoreRecord record in scoreList){
                    line = string.Format("%s%s%s%s%s", record.name, DELIMITER,
                        record.date, DELIMITER, record.score.ToString());
                    writer.WriteLine(line);
                }
                writer.Close();
            }
            return true;
        } catch (Exception e){
            Console.WriteLine(e.GetBaseException());
            return false;
        }
    }

    public  ReadOnlyCollection<ScoreRecord> GetScores(){
        return scoreList.AsReadOnly();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
