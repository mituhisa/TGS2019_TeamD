using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    private GameObject Button;
    private GameObject tutorialB;
    private GameObject menuB;
    private GameObject mainB;

	void Start () {
        tutorialB = GameObject.Find("tutorial_Button");	
        menuB = GameObject.Find("menu_Button");
        mainB = GameObject.Find("main_Button");

	}

	void Update () {
        Button = EventSystem.current.currentSelectedGameObject;
        
    }

    public void OnClick() {
        if(Button == tutorialB) {
            SceneManager.LoadScene("");
        }
        if(Button == mainB) {
            SceneManager.LoadScene("Ingame");
        }
        if(Button == menuB) {
            SceneManager.LoadScene("Menu");
        }
    }
}

