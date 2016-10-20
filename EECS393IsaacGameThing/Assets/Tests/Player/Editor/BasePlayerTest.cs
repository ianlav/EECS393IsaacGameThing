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
	}
}
