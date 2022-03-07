using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject UIManager;
    UIManager UIms;

    public int gain_words, gain_combo, score;
    public int quota_words;

    void Awake(){
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        InitVariables();
    }

    void InitVariables(){
        UIms = UIManager.GetComponent<UIManager>();
    }

    void Update()
    {
        
    }

    public void IncreaseScore(int plusScore){
        score += plusScore;
        UIms.SetScoreUI(score);
    }
}
