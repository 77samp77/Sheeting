using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [System.NonSerialized] public bool gameIsStop;
    [System.NonSerialized] public bool isGameOver;

    public GameObject UIManager;
    UIManager UIms;

    [System.NonSerialized] public int gain_words, gain_combo, score;
    public int quota_words;

    [System.NonSerialized] public int life;
    public int life_max;

    void Awake(){
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        InitVariables();
    }

    void InitVariables(){
        UIms = UIManager.GetComponent<UIManager>();
        life = life_max;
    }

    void Update()
    {
        gameIsStop = JudgeGameStop();
    }

    bool JudgeGameStop(){
        if(isGameOver) return true;
        return false;
    }

    public void IncreaseScore(int plusScore){
        score += plusScore;
        UIms.SetScoreUI(score);
    }

    public void DecreaseLife(){
        life--;
        UIms.lifeSprites[life].SetActive(false);
    }
}
