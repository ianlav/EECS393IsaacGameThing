﻿using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class PlatformMakerTest {

    //tests if the algorithm is able to calculate platform spawns correctly
	[Test]
	public void PlatformSpawnRangeTest()
	{
        //some default player values
        float grav = 9.81f;
        float horSpeed = 10;
        float vertSpeed = 10;
   
        PlatformMakerModel model = new PlatformMakerModel(grav, horSpeed, vertSpeed);
        Assert.AreEqual(model.getMaxYFromX(0), 0); //zero test
        Assert.LessOrEqual(model.getMaxYFromX(5) - 3.77375f, 0.005f); //calculated from wolframalpha
        Assert.LessOrEqual(model.getMaxYFromX(10) - 5.095f, 0.005f); //calculated from wolframalpha

        //tests that the platform generation 10 times
        for (int i = 0; i < 10; i++)
        {
            Vector2 plat = model.getPositionInRange(0, 20, 0, 20);
            float maxY = model.getMaxYFromX(plat.x);
            float midPointX = vertSpeed * horSpeed / (grav);
            if (!((plat.x < midPointX && plat.y < model.getMaxYFromX(midPointX)) || plat.y < maxY))
            {
                Assert.Fail();
            }
        }
	}
}
