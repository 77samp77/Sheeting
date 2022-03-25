using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_Title : MonoBehaviour
{
    public GameObject titleManager;
    TitleManager tms;

    public GameObject accountManager;
    AccountManager ams;

    public GameObject accountText;

    public GameObject[] choosingUI_title;
    public GameObject[] choosingUI_account;

    public GameObject accountUI;
    public GameObject popUpUI, popUp_backGround, popUp_statusText, popUp_messageText;

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
        if(StaticManager.isSigningIn) SetUserName(StaticManager.userName);
    }

    void InitVariables(){
        tms = titleManager.GetComponent<TitleManager>();
        ams = accountManager.GetComponent<AccountManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetChoosingUI(string name, int prev, int now){
        GameObject[] choosingUI = choosingUI_title;
        if(name == "account") choosingUI = choosingUI_account;
        choosingUI[prev].SetActive(false);
        choosingUI[now].SetActive(true);
    }

    public void SetUserName(string name){
        accountText.GetComponent<Text>().text = "User: " + name;
    }

    public void PopUp(bool isSuccess, string name){
        if(isSuccess){
            ams.CloseAccountUI();
            SetChoosingUI("title", tms.choosing, 0);
            tms.choosing = 0;
        }
        popUpUI.SetActive(true);
        popUp_backGround.SetActive(isSuccess);
        Text stt = popUp_statusText.GetComponent<Text>();
        if(isSuccess) stt.text = "SUCCESS!";
        else stt.text = "FAILURE..";
        Text mtt = popUp_messageText.GetComponent<Text>();
        if(isSuccess){
            stt.text = "SUCCESS!";
            mtt.text = "Welcome <color=#1E9600>" + name + "</color>.";
        }
        else{
            stt.text = "FAILURE...";
            mtt.text = "Please Try Again.";
        }
    }
}
