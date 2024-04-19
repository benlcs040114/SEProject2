using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/// <summary>
/// Score Management Script
/// </summary>
public class ScoreManager : MonoBehaviour
{
    //score variable
    public static int score;


    Text text;//text
    public Text dieText;//death score text
   
    void Awake ()
    {
        text = GetComponent <Text> ();
        score = 0;
    }


    void Update ()
    {
        text.text = "Score: " + score;
        dieText.text = "Current Score：<size=14><color=#00FFEA>" + score + "</color></size>";
    }
}
