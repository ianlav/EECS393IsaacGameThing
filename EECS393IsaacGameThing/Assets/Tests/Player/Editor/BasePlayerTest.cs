using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class BasePlayerTest {

	[Test]
	public void EditorTest()
	{
        CharacterModel mdl = new CharacterModel();
        mdl.takeDamage(10);
        Assert.AreEqual(90, mdl.hp);
        mdl.takeDamage(100);
        Assert.AreEqual(0, mdl.hp);
	}
}
