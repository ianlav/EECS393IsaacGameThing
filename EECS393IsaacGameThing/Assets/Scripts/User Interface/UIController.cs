using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
	
    public void ChangeScene (string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
