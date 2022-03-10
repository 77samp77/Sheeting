using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [System.NonSerialized] public bool gameIsStop;
    [System.NonSerialized] public bool isGameOver;
    bool isPause, isFinish;

    public GameObject UIManager;
    UIManager UIms;

    [System.NonSerialized] public int gain_words, gain_combo, score;
    public int quota_words;

    [System.NonSerialized] public int life;
    public int life_max;

    public float gameSpeed;

    public int timeLimit;    // 制限時間(フレーム)
    int progress;

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
        if(Input.GetKeyDown(KeyCode.P)) SwitchPause();
        gameIsStop = JudgeGameStop();

        if(gameIsStop) return;
        progress++;
        UIms.SetProgressBarUI(progress, timeLimit);

        if(progress == timeLimit) GameFinish();
    }

    bool JudgeGameStop(){
        if(isPause) return true;
        if(isGameOver) return true;
        if(isFinish) return true;
        return false;
    }

    void SwitchPause(){
        isPause = !isPause;
        UIms.pauseUI.SetActive(isPause);
    }

    void GameFinish(){
        isFinish = true;
        Debug.Log("GameFinish");
    }

    public void IncreaseScore(int plusScore){
        score += plusScore;
        UIms.SetScoreUI(score);
    }

    public void DecreaseLife(){
        life--;
        UIms.lifeSprites[life].SetActive(false);
    }

    public void GameOver(){
        isGameOver = true;
        Debug.Log("GameOver");
    }
}
