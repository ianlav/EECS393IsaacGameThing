using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class WeaponTests {

	[Test]
	public void MergeTest()
	{
		//local vars
		var gameObject = new GameObject();
        Merge merge = gameObject.AddComponent<Merge>();
        merge.Start();
        Weapon[] listIn, listOut;

        //test 1->1 merging
        Assert.IsTrue(merge.addMergeRule(new[]{ typeof(StartGun) }, typeof(SpreadGun)));
        listIn = new Weapon[]{ gameObject.AddComponent<StartGun>() };
        listOut = merge.mergeIfPossible(listIn);
        Assert.AreEqual(listOut[0].GetType(), typeof(SpreadGun));

        //test 2->1 merging
        merge.clearMergeRules();
        Assert.IsTrue(merge.addMergeRule(new[]{ typeof(StartGun), typeof(SpreadGun) }, typeof(LaserGun)));
        listIn = new Weapon[]{ gameObject.AddComponent<StartGun>(), gameObject.AddComponent<SpreadGun>() };
        listOut = merge.mergeIfPossible(listIn);
        Assert.AreEqual(listOut[0].GetType(), typeof(LaserGun));

        //test invalid merge options
        merge.clearMergeRules();
        Assert.IsFalse(merge.addMergeRule(new[]{ typeof(StartGun), typeof(int) }, typeof(LaserGun)));
        Assert.IsFalse(merge.addMergeRule(new[]{ typeof(StartGun), typeof(LaserGun) }, typeof(long)));
    }
}
