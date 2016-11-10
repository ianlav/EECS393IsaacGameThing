using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

	int currentScore = 0;
	
    public void ChangeScene (string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

	public void setCurrentScore(int newScore){
		this.currentScore = newScore;
	}

	public int getCurrentScore(){
		return currentScore;
	}
}
