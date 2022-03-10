using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject player, sheet, wordManager, enemyManager;
    PlayerController pcs;
    SheetController scs;
    WordGenerator wgs;
    EnemyGenerator egs;

    public GameObject words, enemyShots, enemies, playerShots;

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

    public int timeLimit, progress;    // 制限時間(フレーム)、経過フレーム数

    int choosing;

    void Awake(){
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        InitVariables();
    }

    void InitVariables(){
        pcs = player.GetComponent<PlayerController>();
        scs = sheet.GetComponent<SheetController>();
        wgs = wordManager.GetComponent<WordGenerator>();
        egs = enemyManager.GetComponent<EnemyGenerator>();
        UIms = UIManager.GetComponent<UIManager>();
        life = life_max;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) SwitchPause(!isPause);
        if(Input.GetKeyDown(KeyCode.R)) ResetGame();
        if(isPause) ControllPause();

        gameIsStop = JudgeGameStop();

        if(gameIsStop) return;
        UIms.SetProgressBarUI(progress, timeLimit);

        if(progress == timeLimit) GameFinish();

        progress++;
    }

    bool JudgeGameStop(){
        if(isPause) return true;
        if(isGameOver) return true;
        if(isFinish) return true;
        return false;
    }

    void SwitchPause(bool status){
        isPause = status;
        UIms.pauseUI.SetActive(isPause);
        if(isPause){
            UIms.SetPauseChoosingUI(choosing, 0);
            choosing = 0;
        }
    }

    void ControllPause(){
        int pre_choosing = choosing;
        if(Input.GetKeyDown(KeyCode.W) && choosing > 0) choosing--;
        if(Input.GetKeyDown(KeyCode.S) && choosing < 2) choosing++;
        if(choosing != pre_choosing) UIms.SetPauseChoosingUI(pre_choosing, choosing);

        if(Input.GetKeyDown(KeyCode.Space)){
            if(choosing == 0) SwitchPause(false);
            else if(choosing == 1) ResetGame();
        }
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

    void ResetGame(){
        ResetVariables();
        pcs.ResetVariables();
        scs.ResetVariables();
        wgs.ResetVariables();
        egs.ResetVariables();
        foreach(Transform word in words.transform) Destroy(word.gameObject);
        foreach(Transform enemyShot in enemyShots.transform) Destroy(enemyShot.gameObject);
        foreach(Transform enemy in enemies.transform) Destroy(enemy.gameObject);
        foreach(Transform playerShot in playerShots.transform) Destroy(playerShot.gameObject);
    }

    void ResetVariables(){
        life = life_max;
        for(int i = 0; i < life_max; i++) UIms.lifeSprites[i].SetActive(true);
        progress = 0;
        IncreaseScore(-score);
        gain_words = gain_combo = 0;
        SwitchPause(false);
        isGameOver = isFinish = false;
    }
}
