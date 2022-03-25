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

    public GameObject headText;
    public GameObject[] levelButtonUI, levelButtonNum, choosingUI;
    public Sprite[] levelButtonSprite, barLevelSprite;
    
    public GameObject switchButton;
    public Sprite[] switchButtonSprite;

    public GameObject[] lifeSprites;
    public GameObject bar, bar_level, bar_level_num, bar_quotaText, bar_speedText, bar_hiscoreText;

    public GameObject rankingUI, rankingHeadText;
    public GameObject[] rankingText;

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
            levelButtonNum[i].GetComponent<Text>().color = new Color32(255, 255, 255, 255);
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

    public void SwitchUI(int status){
        string temp_ht = "LEVEL SELECT";
        if(status == 1) temp_ht = "RANKING";
        headText.GetComponent<Text>().text = temp_ht;
        headText.transform.Find("Shadow").GetComponent<Text>().text = temp_ht;

        Image sb_image = switchButton.GetComponent<Image>();
        sb_image.sprite = switchButtonSprite[status];

        if(status == 0) SetLevelButtonsColor();
        else{
            for(int n = 1; n <= 10; n++){
                levelButtonUI[n].GetComponent<Image>().sprite = levelButtonSprite[3];
                levelButtonNum[n].GetComponent<Text>().color = new Color32(140, 70, 0, 255);
                levelButtonNum[n].SetActive(true);
            }
        }

        bar.SetActive(status == 0);
    }

    public void SetRankingUI(int level, string[] nameList, string[] scoreList){
        rankingHeadText.GetComponent<Text>().text = "LEVEL " + level;

        for(int n = 0; n < 5; n++){
            Text nameText = rankingText[n].transform.Find("UserName").GetComponent<Text>();
            nameText.text = nameList[n];
            Text scoreText = rankingText[n].transform.Find("Score").GetComponent<Text>();
            scoreText.text = scoreList[n];
        }
        rankingUI.SetActive(true);
    }
}
