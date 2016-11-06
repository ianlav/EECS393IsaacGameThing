﻿using UnityEngine;
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
        GameObject go = new GameObject();
        Scoring scoring = go.AddComponent<Scoring>();

        ReadOnlyCollection<Scoring.ScoreRecord> scores;
        Scoring.ScoreRecord r1 = new Scoring.ScoreRecord("test 1", "9/21/2112", 1111.111);
        Scoring.ScoreRecord r2 = new Scoring.ScoreRecord("test 2", "9/22/2112", 2222.222);
        Scoring.ScoreRecord r3 = new Scoring.ScoreRecord("test 3", "9/23/2112", 3333.333);
        scoring.AddRecords(r1, r2, r3); //write r1..r3 to local score list
        //check if local variables are equal
        scores = scoring.GetScores();
        Assert.AreEqual(scores.Count, 3);
        Assert.AreEqual(scores[0], r1);
        Assert.AreEqual(scores[1], r2);
        Assert.AreEqual(scores[2], r3);
        Assert.IsTrue(scoring.WriteScores()); //write r1..r3 to file
        Assert.IsTrue(scoring.ReadScores()); //read records from file
        //check if local variables are equal
        scores = scoring.GetScores(); 
        Assert.AreEqual(scores.Count, 3);
        Assert.AreEqual(scores[0], r1);
        Assert.AreEqual(scores[1], r2);
        Assert.AreEqual(scores[2], r3);
	}
}