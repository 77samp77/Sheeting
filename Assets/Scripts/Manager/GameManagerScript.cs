using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameMusic;
    GameMusicManager gmms;

    GameObject systemSound;
    SystemSoundManager ssms;

    GameObject stageData;
    StageDataManager sdms;

    public GameObject player, sheet, wordManager, enemyManager, obstacleManager;
    PlayerController pcs;
    SheetController scs;
    WordGenerator wgs;
    EnemyGenerator egs;
    ObstacleGenerator ogs;
    GameObject backGround;
    BackGroundController bgcs;

    public GameObject words, obstacles, enemyShots, enemies, playerShots;

    [System.NonSerialized] public bool gameIsStop;
    [System.NonSerialized] public bool isStart, isGameOver, isSuccess;
    bool isPause, isFinish;

    public GameObject UIManager;
    UIManager UIms;

    [System.NonSerialized] public int gain_words, gain_combo, score, total_score;
    public int quota_words;

    [System.NonSerialized] public int life;
    public int life_max;

    public float gameSpeed;

    public int timeLimit, progress;    // 制限時間(フレーム)、経過フレーム数

    [System.NonSerialized] public List<int> ip = new List<int>();

    int choosing;
    [System.NonSerialized] public bool canControllUI;

    public Sprite[] defeat_sprites = new Sprite[3];
    int SW;

    void Awake(){
        Application.targetFrameRate = 60;
        SW = StaticManager.screenWidth;
    }

    void Start()
    {
        InitVariables();
        bgcs.InitGameVariables();
        LoadStageData();
    }

    void InitVariables(){
        gmms = gameMusic.GetComponent<GameMusicManager>();
        systemSound = GameObject.Find("SystemSound");
        ssms = systemSound.GetComponent<SystemSoundManager>();
        stageData = GameObject.Find("StageData");
        sdms = stageData.GetComponent<StageDataManager>();
        pcs = player.GetComponent<PlayerController>();
        scs = sheet.GetComponent<SheetController>();
        wgs = wordManager.GetComponent<WordGenerator>();
        egs = enemyManager.GetComponent<EnemyGenerator>();
        ogs = obstacleManager.GetComponent<ObstacleGenerator>();
        backGround = GameObject.Find("BackGround");
        bgcs = backGround.GetComponent<BackGroundController>();
        UIms = UIManager.GetComponent<UIManager>();
    }

    int level;
    void LoadStageData(){
        level = StaticManager.gameLevel;
        LoadStageBaseData();
        LoadStageInitPointer();
    }

    void LoadStageBaseData(){
        quota_words = sdms.base_quotaNum[level];
        UIms.SetWordCountUI(0, quota_words);
        timeLimit = sdms.base_limit[level];
        gameSpeed = setGameSpeed(sdms.base_speed[level]);
        bgcs.v = gameSpeed;
        life_max = sdms.base_life[level];
        life = life_max;
        for(int i = life_max; i < 6; i++) UIms.lifeSprites[i].SetActive(false);
    }

    float setGameSpeed(string speedText){
        switch(speedText){
            case "VERY SLOW": return 0.1f;
            case "SLOW": return 0.2f;
            case "NORMAL": return 0.4f;
            case "FAST": return 0.6f;
            case "VERY FAST": return 1.0f;
            default: return 2.0f;
        }
    }

    void LoadStageInitPointer(){
        for(int i = 0; i < sdms.init_level.Length; i++){
            if(sdms.init_level[i] == level) ip.Add(i);
            if(sdms.init_level[i] > level) return;
        }
    }

    void Update()
    {
        gameIsStop = JudgeGameStop();
        if(!isStart){
            if(pcs.readyToStart && Mathf.RoundToInt(bgcs.spr_pos[1].x + SW) % 18 == 10){
                gmms.PlayBGM(gmms.bgm);
                isStart = true;
            }
            return;
        }

        if(Input.GetKeyDown(KeyCode.P) && !isFinish) SwitchPause(!isPause);
        if(Input.GetKeyDown(KeyCode.R)) ResetGame();
        if(isPause) ControllPause();
        if(isFinish){
            if(!canControllUI) UIms.ResultMotion();
            else ControllResult();
        }
        else if(isGameOver) pcs.GameOverAnimation();

        if(gameIsStop) return;
        if(Input.GetKeyDown(KeyCode.F)) GameFinish();
        if(Input.GetKeyDown(KeyCode.G)) GameOver();
        if(readPoint < ip.Count) ReadInitData();
        UIms.SetProgressBarUI(Mathf.Min(progress, timeLimit), timeLimit);

        if(progress == timeLimit) ogs.Generate("Goal", 0);

        progress++;
    }

    bool JudgeGameStop(){
        if(!isStart) return true;
        if(isPause) return true;
        if(isGameOver) return true;
        if(isFinish) return true;
        return false;
    }

    void SwitchPause(bool status){
        isPause = status;
        if(isPause){
            gmms.PauseBGM();
            ssms.PlaySE(ssms.SE_pause_open);
        }
        else{
            if(isStart) gmms.UnpauseBGM();
            else gmms.StopBGM();
            if(!isFinish) ssms.PlaySE(ssms.SE_pause_close);
        }
        UIms.pauseUI.SetActive(isPause);
        if(isPause){
            UIms.SetChoosingUI(UIms.pauseUI_choosing, choosing, 0);
            choosing = 0;
        }
    }

    void ControllPause(){
        int pre_choosing = choosing;
        if(Input.GetKeyDown(KeyCode.W) && choosing > 0) choosing--;
        if(Input.GetKeyDown(KeyCode.S) && choosing < 2) choosing++;
        if(choosing != pre_choosing){
            ssms.PlaySE(ssms.SE_choose);
            UIms.SetChoosingUI(UIms.pauseUI_choosing, pre_choosing, choosing);
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            if(choosing == 0) SwitchPause(false);
            else if(choosing == 1){
                ssms.PlaySE(ssms.SE_decide);
                ResetGame();
            }
            else if(choosing == 2){
                ssms.PlaySE(ssms.SE_decide);
                SceneManager.LoadScene("LevelSelect");
            }
        }
    }

    void ControllResult(){
        int pre_choosing = choosing;
        if(Input.GetKeyDown(KeyCode.D) && choosing == 0) choosing++;
        if(Input.GetKeyDown(KeyCode.A) && choosing == 1) choosing--;
        if(choosing != pre_choosing){
            ssms.PlaySE(ssms.SE_choose);
            UIms.SetChoosingUI(UIms.resultUI_choosing, pre_choosing, choosing);
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            if(choosing == 0){
                ssms.PlaySE(ssms.SE_decide);
                ResetGame();
            }
            else if(choosing == 1){
                ssms.PlaySE(ssms.SE_decide);
                SceneManager.LoadScene("LevelSelect");
            }
        }
    }

    bool isNewRecord = false;
    public void GameFinish(){
        isFinish = true;
        isSuccess = judgeSuccess();
        total_score = score;
        int lifeBonus = life * 1000;
        if(isSuccess){
            total_score += 15000 + lifeBonus;
            if(life == life_max) total_score += 10000;
            if(total_score > sdms.base_hiscore[level]) isNewRecord = true;
            UpdateLevelStatus(level, total_score, isNewRecord);
        }
        UIms.SetResultUI(isGameOver, isSuccess, score, lifeBonus, life == life_max, total_score, isNewRecord);
    }

    void UpdateLevelStatus(int level, int hiscore, bool isNewRecord){
        sdms.base_status[level] = 2;
        if(isNewRecord){
            sdms.base_hiscore[level] = hiscore;
        }
        if(level == 5 && sdms.base_status[6] == 0){
            for(int pLevel = 6; pLevel <= 9; pLevel++) sdms.base_status[pLevel] = 1;
        }
        if(level == 9 && sdms.base_status[10] == 0){
            sdms.base_status[10] = 1;
        }
    }

    bool judgeSuccess(){
        if(isGameOver) return false;
        if(gain_words < quota_words) return false;
        return true;
    }

    public void IncreaseScore(int plusScore){
        score += plusScore;
        UIms.SetScoreUI(score);
    }

    public void DecreaseLife(){
        life--;
        if(life >= 0) UIms.lifeSprites[life].SetActive(false);
    }

    public void GameOver(){
        isGameOver = true;
    }

    int readPoint = 0;
    void ReadInitData(){
        if(sdms.init_progress[ip[readPoint]] != progress) return;
        while(sdms.init_progress[ip[readPoint]] == progress){
            int p = ip[readPoint];
            switch(sdms.init_tag[p]){
                case "Word":
                    wgs.Generate(sdms.init_type[p], sdms.init_pos[p], sdms.init_speed[p], 
                                 sdms.init_length[p], sdms.init_isTop[p]);
                    break;
                case "Enemy":
                    egs.Generate(sdms.init_type[p], sdms.init_pos[p], sdms.init_isTop[p]);
                    break;
                case "Gate":
                    for(int i = 0; i < 11; i++){
                        int _pos = -39 + 13 * i;
                        if(sdms.init_pos[p] <= i && i < sdms.init_pos[p] + sdms.init_length[p]){
                            if(sdms.init_type[p] == "Enemy") egs.Generate("C", _pos, false);
                        }
                        else ogs.Generate("Check", _pos);
                    }
                    break;
                case "Obstacle":
                    ogs.Generate(sdms.init_type[p], sdms.init_pos[p]);
                    break;
            }
            readPoint++;
            if(readPoint == ip.Count) return;
        }
    }

    void ResetGame(){
        ResetVariables();
        pcs.ResetVariables();
        scs.ResetVariables();
        wgs.ResetVariables();
        egs.ResetVariables();
        foreach(Transform word in words.transform) Destroy(word.gameObject);
        foreach(Transform obstacle in obstacles.transform) Destroy(obstacle.gameObject);
        foreach(Transform enemyShot in enemyShots.transform) Destroy(enemyShot.gameObject);
        foreach(Transform enemy in enemies.transform) Destroy(enemy.gameObject);
        foreach(Transform playerShot in playerShots.transform) Destroy(playerShot.gameObject);
    }

    void ResetVariables(){
        life = life_max;
        for(int i = 0; i < life_max; i++) UIms.lifeSprites[i].SetActive(true);
        progress = readPoint = 0;
        UIms.SetProgressBarUI(progress, timeLimit);
        IncreaseScore(-score);
        gain_words = gain_combo = 0;
        UIms.SetWordCountUI(gain_words, quota_words);
        isStart = isGameOver = false;
        SwitchPause(false);
        isFinish = false;
        canControllUI = false;
        UIms.ResetResultUI();
    }
}
