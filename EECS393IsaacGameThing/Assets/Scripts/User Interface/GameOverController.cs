using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameOverController : UIController {

    public Toggle saveCheckbox;
    public InputField characterName;
    
	//initialization
	void Start ()
    {
        characterName = gameObject.GetComponentInChildren<InputField>();
        if (ScoreController.currentName != null && ScoreController.currentName != "")
            characterName.text = ScoreController.currentName;
        saveCheckbox = gameObject.GetComponentInChildren<Toggle>();
	}
	
	//update is called once per frame
	void Update ()
    {
        if (characterName.text != ScoreController.currentName)
            ScoreController.currentName = characterName.text;
	}

    //called when the scene is exited ?
    void OnDestroy()
    {
        if (saveCheckbox.enabled) {
            try {
                if (ScoreController.currentName == null || ScoreController.currentName == "")
                    ScoreController.currentName = ((Text)characterName.placeholder).text;
                ScoreController.saveCurrentScore();
                print("GameOverController: saved score!");
            } catch (Exception e) {
                print("Error saving score (GameOverController)");
                print(e.StackTrace);
            }
        }
    }
}
