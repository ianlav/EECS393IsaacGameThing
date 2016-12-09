using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public Text text;

	void Awake()
	{
		Screen.SetResolution( 1024, 440, false );
	}

    void Update()
    {
        if(Input.GetKey(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0))
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

    public void displayPopUpText(string str, Vector3 pos, int fontSize = 20)
    {
        Text t = Instantiate(text, transform) as Text;
        t.text = str;
        t.transform.position = pos + new Vector3(0, 0.5f, 0);
        t.transform.localScale = new Vector3(1, 1, 1);
        t.fontSize = fontSize;
        t.color = Color.green;
        Time.timeScale = 0;
        Destroy(t.gameObject, 1);
    }
}
