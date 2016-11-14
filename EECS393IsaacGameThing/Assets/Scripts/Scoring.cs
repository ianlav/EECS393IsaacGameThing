using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;

public static class Scoring {

    static string FILEPATH = "scores.sav"; //can be modified in constructor to write to appdata directory if needed
    const string DELIMITER = " -=- ";
    static List<ScoreRecord> scoreList = new List<ScoreRecord>();

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

    public static void clearRecords() {
        scoreList.Clear();
    }

    public static void AddRecord(ScoreRecord record){
        scoreList.Add(record);
    }

    public static void AddRecord(string name, string date, double score){
        ScoreRecord r = new ScoreRecord(name, date, score);
        scoreList.Add(r);
    }

    public static void AddRecords(params ScoreRecord[] records){
        for(int i=0; i < records.Length; i++)
            scoreList.Add(records[i]);
    }

    public static bool ReadScores(){
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

    public static bool WriteScores(){
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

    public static ReadOnlyCollection<ScoreRecord> GetScores(){
        return scoreList.AsReadOnly();
    }
}
