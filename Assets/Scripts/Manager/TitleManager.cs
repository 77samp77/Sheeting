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
    public GameObject pressSpaceKey, titleButtons;
    public GameObject backGround, systemSound;
    SystemSoundManager ssms;
    BackGroundController bgcs;

    public GameObject[] choosingUI;
    bool buttonIsAppear;
    int choosing;

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
        else ControllButtons();
    }

    void OpenButtons(){
        ssms.PlaySE(ssms.SE_decide);
        buttonIsAppear = true;
        titleButtons.SetActive(true);
    }

    int prev_bottom = 1;
    void ControllButtons(){
        if(Input.GetKeyDown(KeyCode.Space)){
            if(choosing == 0){
                ssms.PlaySE(ssms.SE_decide);
                SceneManager.LoadScene("LevelSelect");
            }
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
            choosingUI[prev_choosing].SetActive(false);
            choosingUI[choosing].SetActive(true);
        }
    }
}
