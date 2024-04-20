using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startbutton : MonoBehaviour {


    public void PressQuit()
    {
        Application.Quit();
    }
    public void GameStart()
    {
        SceneManager.LoadScene("main");
      //  GameManager.instance.SwitchScene("main");//switch scene
    }
}
