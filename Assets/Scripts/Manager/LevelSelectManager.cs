using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NCMB;

public class LevelSelectManager : MonoBehaviour
{
    GameObject stageData;
    StageDataManager sdms;
    GameObject systemSound;
    SystemSoundManager ssms;
    GameObject backGround;
    BackGroundController bgcs;

    public GameObject UIManager;
    UIManager_LevelSelect UIms;

    [System.NonSerialized] public int prev_choosing = 1, choosing = 1;
    int choosing_row = 0, choosing_column = 0;
    int ccs = 5; // choosing_column_size

    // List<string> ranking_name = new List<string>();
    // List<string> ranking_score = new List<string>();
    string[] ranking_name = new string[5];
    string[] ranking_score = new string[5];

    int status = 0; //01...ゲーム、ランキング

    void Awake(){
        StaticManager.canControll = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
    }

    void InitVariables(){
        stageData = GameObject.Find("StageData");
        sdms = stageData.GetComponent<StageDataManager>();
        systemSound = GameObject.Find("SystemSound");
        ssms = systemSound.GetComponent<SystemSoundManager>();
        backGround = GameObject.Find("BackGround");
        bgcs = backGround.GetComponent<BackGroundController>();
        UIms = UIManager.GetComponent<UIManager_LevelSelect>();
        prev_choosing = choosing = StaticManager.gameLevel;
        choosing_row = (int)((choosing - 1) / 5);
        choosing_column = (choosing - 1) % 5;
        bgcs.v = setGameSpeed(sdms.base_speed[choosing]);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)) ControllBack();
        else if(Input.GetKeyDown(KeyCode.Space)) ControllDecide();
        else if(!UIms.rankingUI.activeSelf) ControllChoose();
    }
    
    void ControllBack(){
        ssms.PlaySE(ssms.SE_back);
        SceneManager.LoadScene("Title");
    }

    void ControllDecide(){
        if(!UIms.rankingUI.activeSelf) ssms.PlaySE(ssms.SE_decide);
        if(choosing == 0) SwitchGameRanking(1 - status);
        else if(status == 0) SceneManager.LoadScene("Game");
        else{
            if(UIms.rankingUI.activeSelf){
                ssms.PlaySE(ssms.SE_back);
                UIms.rankingUI.SetActive(false);
            }
            else DisplayRanking(choosing);
        }
    }

    void ControllChoose(){
        prev_choosing = choosing;
        if(Input.GetKeyDown(KeyCode.W)){
            if(choosing == 5) ChooseButton(0);
            else if(choosing_row > 0) ChooseButton((choosing_row - 1) * ccs + choosing_column + 1);
        }
        else if(Input.GetKeyDown(KeyCode.A)){
            if(choosing_column > 0) ChooseButton(choosing_row * ccs + (choosing_column - 1) + 1);
        }
        else if(Input.GetKeyDown(KeyCode.S)){
            if(choosing == 0) ChooseButton(5);
            else if(choosing_row < 1) ChooseButton((choosing_row + 1) * ccs + choosing_column + 1);
        }
        else if(Input.GetKeyDown(KeyCode.D)){
            if(choosing_column < ccs - 1) ChooseButton(choosing_row * ccs + (choosing_column + 1) + 1);
        }
        if(prev_choosing != choosing){
            ssms.PlaySE(ssms.SE_choose);
            if(choosingLevelButton(choosing)) StaticManager.gameLevel = choosing;
        }
    }

    void ChooseButton(int next){
        if(!canChoose(next)) return;
        UIms.SetChoosingUI(choosing, next);
        choosing = next;
        if(choosingLevelButton(choosing)){
            choosing_row = (int)((choosing - 1) / 5);
            choosing_column = (choosing - 1) % 5;
            if(status == 0) bgcs.v = setGameSpeed(sdms.base_speed[choosing]);
            UIms.SetUIBar(choosing);
        }
    }

    bool canChoose(int next){
        return !choosingLevelButton(next) || status == 1 || StaticManager.levelStatus[next] != 0;
    }

    bool choosingLevelButton(int choosing){
        return 1 <= choosing && choosing <= 10;
    }

    void SwitchGameRanking(int nextStatus){
        status = nextStatus;
        UIms.SwitchUI(status);
    }

    void DisplayRanking(int level){
        string objectName = "Ranking_" + level;
        NCMBQuery<NCMBObject> rankingQuery = new NCMBQuery<NCMBObject>(objectName);
        // rankingQuery.WhereEqualTo("Hiscore", 6000);
        rankingQuery.Limit = 5;
        rankingQuery.OrderByDescending("Hiscore");
        rankingQuery.FindAsync((List<NCMBObject> objList, NCMBException e) =>{
            if(e == null){
                int num = 0;
                foreach(NCMBObject obj in objList){
                    if(obj == null) break;
                    ranking_name[num] = obj["UserName"].ToString();
                    int scoreInt = int.Parse(obj["Hiscore"].ToString());
                    ranking_score[num] = scoreInt.ToString("00000000");
                    num++;
                }
                for(int n = num; n < 5; n++){
                    ranking_name[n] = "----------";
                    ranking_score[n] = "--------";
                }
                UIms.SetRankingUI(level, ranking_name, ranking_score);
            }
            else Debug.Log("エラー");
        });
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
}
