using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class WeaponTests {

	[Test]
	public void MergeTests()
	{
		//local vars
		var gameObject = new GameObject();
        Merge merge = gameObject.AddComponent<Merge>();
        merge.Start();
        Weapon[] listIn, listOut;

        //test for 1->1 merging
        Assert.IsTrue(merge.addMergeRule(new[]{ typeof(StartGun) }, typeof(SpreadGun)));
        listIn = new Weapon[]{ gameObject.AddComponent<StartGun>() };
        listOut = merge.mergeIfPossible(listIn);
        Assert.AreEqual(listOut[0].GetType(), typeof(SpreadGun));
        Assert.AreEqual(listOut.Length, listIn.Length);

        //test for 2->1 merging
        merge.clearMergeRules();
        Assert.IsTrue(merge.addMergeRule(new[]{ typeof(StartGun), typeof(SpreadGun) }, typeof(LaserGun)));
        listIn = new Weapon[]{ gameObject.AddComponent<StartGun>(), gameObject.AddComponent<SpreadGun>() };
        listOut = merge.mergeIfPossible(listIn);
        Assert.AreEqual(listOut[0].GetType(), typeof(LaserGun));
        Assert.AreNotEqual(listOut.Length, listIn.Length);

        //test for repeated & unordered 5->1 merging
        merge.clearMergeRules();
        Assert.IsTrue(merge.addMergeRule(new[] { typeof(StartGun), typeof(StartGun), typeof(SpreadGun), typeof(StartGun), typeof(SpreadGun) }, typeof(LaserGun)));
        listIn = new Weapon[] { gameObject.AddComponent<SpreadGun>(), gameObject.AddComponent<SpreadGun>(), gameObject.AddComponent<StartGun>(), gameObject.AddComponent<StartGun>(), gameObject.AddComponent<StartGun>(), };
        listOut = merge.mergeIfPossible(listIn);
        Assert.AreEqual(listOut[0].GetType(), typeof(LaserGun));
        Assert.AreNotEqual(listOut.Length, listIn.Length);

        //test for repeated & unordered 5->1 merging (where 1 repeated value is missing, verifies no double-spending)
        merge.clearMergeRules();
        Assert.IsTrue(merge.addMergeRule(new[] { typeof(StartGun), typeof(StartGun), typeof(SpreadGun), typeof(StartGun), typeof(SpreadGun) }, typeof(LaserGun)));
        listIn = new Weapon[] { gameObject.AddComponent<SpreadGun>(), gameObject.AddComponent<SpreadGun>(), gameObject.AddComponent<StartGun>(), gameObject.AddComponent<StartGun>(), };
        listOut = merge.mergeIfPossible(listIn);
        Assert.AreNotEqual(listOut[0].GetType(), typeof(LaserGun));
        Assert.AreEqual(listOut.Length, listIn.Length);

        //test to make sure null's for double-spending don't break merge rules by accident (retest valid 5->1)
        listIn = new Weapon[] { gameObject.AddComponent<SpreadGun>(), gameObject.AddComponent<SpreadGun>(), gameObject.AddComponent<StartGun>(), gameObject.AddComponent<StartGun>(), gameObject.AddComponent<StartGun>(), };
        listOut = merge.mergeIfPossible(listIn);
        Assert.AreEqual(listOut[0].GetType(), typeof(LaserGun));
        Assert.AreNotEqual(listOut.Length, listIn.Length);

        //test invalid merge options
        merge.clearMergeRules();
        Assert.IsFalse(merge.addMergeRule(new[]{ typeof(StartGun), typeof(int) }, typeof(LaserGun)));
        Assert.IsFalse(merge.addMergeRule(new[]{ typeof(StartGun), typeof(LaserGun) }, typeof(long)));

        //test to make sure superclasses and subclasses are correctly handled
        merge.clearMergeRules();
        Assert.IsTrue(merge.addMergeRule(new[] { typeof(BurstGun), typeof(SpreadGun) }, typeof(BurstSpreadGun)));
        listIn = new Weapon[] { gameObject.AddComponent<BurstGun>(), gameObject.AddComponent<SpreadGun>() };
        listOut = merge.mergeIfPossible(listIn);
        Assert.AreEqual(listOut[0].GetType(), typeof(BurstSpreadGun));
        Assert.AreNotEqual(listOut.Length, listIn.Length);
        //make sure BurstSpreadGun and MachineSpreadGun are not treated as SpreadGun (they derive from SpreadGun)
        listIn = new Weapon[] { gameObject.AddComponent<BurstGun>(), gameObject.AddComponent<BurstSpreadGun>(), gameObject.AddComponent<MachineSpreadGun>() };
        listOut = merge.mergeIfPossible(listIn);
        Assert.AreNotEqual(listOut[0].GetType(), typeof(BurstSpreadGun));
        Assert.AreEqual(listOut.Length, listIn.Length);
        //make sure SpreadGun is not treated as BurstSpreadGun (it derives from SpreadGun)
        merge.clearMergeRules();
        Assert.IsTrue(merge.addMergeRule(new[] { typeof(BurstGun), typeof(BurstSpreadGun) }, typeof(StartGun)));
        listIn = new Weapon[] { gameObject.AddComponent<BurstGun>(), gameObject.AddComponent<SpreadGun>() };
        listOut = merge.mergeIfPossible(listIn);
        Assert.AreNotEqual(listOut[0].GetType(), typeof(StartGun));
        Assert.AreEqual(listOut.Length, listIn.Length);
    }

    [Test]
    public void WeaponsFireTest()
    {
        //somehow test that each weapon correctly fires
    }
}
