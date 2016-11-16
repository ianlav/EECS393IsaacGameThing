using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public Text text;

    void Update()
    {
        if(Input.GetKey(KeyCode.Return))
        {
            Time.timeScale = 1;
        }
    }
	
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

    public void displayPopUpText(string str, Vector3 pos)
    {
        Text t = Instantiate(text, transform) as Text;
        t.text = str;
        t.transform.position = pos + new Vector3(0, 0.5f, 0);
        t.transform.localScale = new Vector3(1, 1, 1);
        t.fontSize = 20;
        t.color = Color.green;
        Time.timeScale = 0;
        Destroy(t.gameObject, 1);
    }
}
