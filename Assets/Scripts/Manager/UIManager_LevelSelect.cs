using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_LevelSelect : MonoBehaviour
{
    public GameObject levelSelectManager;
    LevelSelectManager lsms;

    GameObject stageData;
    StageDataManager sdms;

    public GameObject[] levelButtonUI = new GameObject[11];
    public GameObject[] levelButtonNum = new GameObject[11];
    public GameObject[] choosingUI = new GameObject[12];
    public Sprite[] levelButtonSprite = new Sprite[3];
    public Sprite[] barLevelSprite = new Sprite[3];

    public GameObject[] lifeSprites = new GameObject[6];
    public GameObject bar_level, bar_level_num, bar_quotaText, bar_speedText, bar_hiscoreText;

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
        SetLevelButtonsColor();
        SetChoosingUI(lsms.prev_choosing, lsms.choosing);
        SetUIBar(lsms.choosing);
    }

    void InitVariables(){
        lsms = levelSelectManager.GetComponent<LevelSelectManager>();
        stageData = GameObject.Find("StageData");
        sdms = stageData.GetComponent<StageDataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetChoosingUI(int prev, int now){
        choosingUI[prev].SetActive(false);
        choosingUI[now].SetActive(true);
    }

    public void SetLevelButtonsColor(){
        int[] levelButtonStatus = StaticManager.levelStatus;
        for(int i = 1; i < levelButtonStatus.Length; i++){
            Image lb_image = levelButtonUI[i].GetComponent<Image>();
            lb_image.sprite = levelButtonSprite[levelButtonStatus[i]];
            levelButtonNum[i].SetActive(levelButtonStatus[i] != 0);
        }
    }

    public void SetUIBar(int level){
        Image bl_image = bar_level.GetComponent<Image>();
        bl_image.sprite = barLevelSprite[StaticManager.levelStatus[level]];
        bar_level_num.GetComponent<Text>().text = level.ToString();
        for(int i = 0; i < 6; i++) lifeSprites[i].SetActive(i < sdms.base_life[level]);
        bar_quotaText.GetComponent<Text>().text = sdms.base_quotaNum[level] + " words";
        bar_speedText.GetComponent<Text>().text = sdms.base_speed[level];
        string temp_hiscoreText = "--------";
        if(StaticManager.levelStatus[level] == 2) temp_hiscoreText = StaticManager.hiscore[level].ToString("00000000");
        bar_hiscoreText.GetComponent<Text>().text = temp_hiscoreText;
    }
}
