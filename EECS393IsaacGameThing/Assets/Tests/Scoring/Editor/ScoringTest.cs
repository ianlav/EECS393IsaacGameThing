using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

public class ScoringTest {

	[Test]
    //test to make sure score read/write works
	public void ScoreReadWriteTest()
	{
        Scoring.clearRecords();
        ReadOnlyCollection<Scoring.ScoreRecord> scores;
        Scoring.ScoreRecord r1 = new Scoring.ScoreRecord("test 1", "9/21/2112", 1111);
        Scoring.ScoreRecord r2 = new Scoring.ScoreRecord("test 2", "9/22/2112", 2222);
        Scoring.ScoreRecord r3 = new Scoring.ScoreRecord("test 3", "9/23/2112", 3333);
        Scoring.AddRecords(r1, r2, r3); //write r1..r3 to local score list
        //check if local variables are equal
        scores = Scoring.GetScores();
        Assert.AreEqual(scores.Count, 3);
        Assert.IsTrue(scores[0].Equals(r1));
        Assert.IsTrue(scores[1].Equals(r2));
        Assert.IsTrue(scores[2].Equals(r3));
        Assert.IsTrue(Scoring.WriteScores()); //write r1..r3 to file
        Scoring.clearRecords(); //clear local variables
        Assert.IsTrue(Scoring.ReadScores()); //read records from file
        //check if local variables are equal
        scores = Scoring.GetScores(); 
        Assert.AreEqual(scores.Count, 3);
        Assert.IsTrue(scores[0].Equals(r1));
        Assert.IsTrue(scores[1].Equals(r2));
        Assert.IsTrue(scores[2].Equals(r3));
    }
}
