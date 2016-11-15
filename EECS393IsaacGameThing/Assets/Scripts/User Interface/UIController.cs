using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public Text text;
	
    public void ChangeScene (string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void thingTookDamage(Vector3 enemy, float dam)
    {
        Text t = Instantiate(text, transform) as Text;
        t.text = dam.ToString();
        t.transform.position = enemy + new Vector3(0, 0.5f, 0);
        t.transform.localScale = new Vector3(1, 1, 1);
        Destroy(t.gameObject, 1);
    }
}
