using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    void Awake(){
        StaticManager.canControll = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
        UIms.SetLevelButtonsColor();
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
        bgcs.v = setGameSpeed(sdms.base_speed[choosing]);
    }

    // Update is called once per frame
    void Update()
    {
        prev_choosing = choosing;

        if(Input.GetKeyDown(KeyCode.W)){
            if(choosing_row > 0) ChooseButton((choosing_row - 1) * ccs + choosing_column + 1);
        }
        if(Input.GetKeyDown(KeyCode.A)){
            if(choosing_column > 0) ChooseButton(choosing_row * ccs + (choosing_column - 1) + 1);
        }
        if(Input.GetKeyDown(KeyCode.S)){
            if(choosing_row < 1) ChooseButton((choosing_row + 1) * ccs + choosing_column + 1);
        }
        if(Input.GetKeyDown(KeyCode.D)){
            if(choosing_column < ccs - 1) ChooseButton(choosing_row * ccs + (choosing_column + 1) + 1);
        }

        if(prev_choosing != choosing){
            ssms.PlaySE(ssms.SE_choose);
            if(choosingLevelButton(choosing)) StaticManager.gameLevel = choosing;
        }

        if(Input.GetKeyDown(KeyCode.Z)){
            ssms.PlaySE(ssms.SE_back);
            SceneManager.LoadScene("Title");
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            ssms.PlaySE(ssms.SE_decide);
            SceneManager.LoadScene("Game");
        }
    }

    void ChooseButton(int next){
        if(!canChoose(next)) return;
        UIms.SetChoosingUI(choosing, next);
        choosing = next;
        if(choosingLevelButton(choosing)){
            choosing_row = (int)((choosing - 1) / 5);
            choosing_column = (choosing - 1) % 5;
            bgcs.v = setGameSpeed(sdms.base_speed[choosing]);
            UIms.SetUIBar(choosing);
        }
    }

    bool canChoose(int next){
        return !choosingLevelButton(next) || StaticManager.levelStatus[next] != 0;
    }

    bool choosingLevelButton(int choosing){
        return 1 <= choosing && choosing <= 10;
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
