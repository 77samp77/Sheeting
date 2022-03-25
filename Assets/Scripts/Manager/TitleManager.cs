using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    // GameObject screenManager;
    public GameObject accountManager;
    AccountManager ams;

    public GameObject UIManager;
    UIManager_Title ums;

    public GameObject pressSpaceKey, titleButtons;
    GameObject backGround, systemSound;
    SystemSoundManager ssms;
    BackGroundController bgcs;

    // public GameObject[] choosingUI;
    bool buttonIsAppear;
    [System.NonSerialized] public int choosing;

    void Awake(){
        Application.targetFrameRate = 60;
        StaticManager.canControll = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
    }

    void InitVariables(){
        ams = accountManager.GetComponent<AccountManager>();
        ums = UIManager.GetComponent<UIManager_Title>();
        backGround = GameObject.Find("BackGround");
        bgcs = backGround.GetComponent<BackGroundController>();
        systemSound = GameObject.Find("SystemSound");
        ssms = systemSound.GetComponent<SystemSoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!buttonIsAppear){
            if(Time.frameCount % 100 == 0) pressSpaceKey.SetActive(true);
            else if(Time.frameCount % 100 == 50) pressSpaceKey.SetActive(false);
            if(Input.GetKeyDown(KeyCode.Space)) OpenButtons();
        }
        else if(ums.popUpUI.activeSelf){
            if(Input.GetKeyDown(KeyCode.Space)){
                ssms.PlaySE(ssms.SE_decide);
                ums.popUpUI.SetActive(false);
            }
        }
        else if(!ams.accountUI.activeSelf) ControllButtons();
    }

    void OpenButtons(){
        ssms.PlaySE(ssms.SE_decide);
        buttonIsAppear = true;
        titleButtons.SetActive(true);
    }

    int prev_bottom = 1;
    void ControllButtons(){
        if(Input.GetKeyDown(KeyCode.Space)){
            ssms.PlaySE(ssms.SE_decide);
            if(choosing == 0) SceneManager.LoadScene("LevelSelect");
            else if(choosing == 1) ams.OpenAccountUI();
        }

        int prev_choosing = choosing;
        if(Input.GetKeyDown(KeyCode.W)){
            if(choosing != 0){
                prev_bottom = choosing;
                choosing = 0;
            }
        }
        else if(Input.GetKeyDown(KeyCode.S)){
            if(choosing == 0) choosing = prev_bottom;
        }
        else if(Input.GetKeyDown(KeyCode.A)){
            if(choosing == 2) choosing = 1;
        }
        else if(Input.GetKeyDown(KeyCode.D)){
            if(choosing == 1) choosing = 2;
        }
        if(choosing != prev_choosing){
            ssms.PlaySE(ssms.SE_choose);
            ums.SetChoosingUI("title", prev_choosing, choosing);
        }
    }
}
