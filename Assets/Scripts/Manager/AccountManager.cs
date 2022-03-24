using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;

public class AccountManager : MonoBehaviour
{
    public GameObject UIManager;
    UIManager_Title ums;

    GameObject systemSound;
    SystemSoundManager ssms;

    public GameObject accountUI;
    public InputField userNameField, passwordField;

    int prev_choosing, choosing;
    int prev_top;

    bool canControll;

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
    }

    void InitVariables(){
        ums = UIManager.GetComponent<UIManager_Title>();
        systemSound = GameObject.Find("SystemSound");
        ssms = systemSound.GetComponent<SystemSoundManager>();
    }

    public void OpenAccountUI(){
        accountUI.SetActive(true);
        prev_choosing = choosing = 2;
        ums.SetChoosingUI("account", prev_choosing, choosing);
        prev_top = 0;
    }

    void CloseAccountUI(){
        accountUI.SetActive(false);
        canControll = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!canControll){
            if(accountUI.activeSelf) canControll = true;
        }
        else ControllAccountUI();
    }

    void ControllAccountUI(){
        if(Input.GetKeyDown(KeyCode.Space)){
            ssms.PlaySE(ssms.SE_decide);
            if(choosing == 2) CloseAccountUI();
        }

        prev_choosing = choosing;
        if(Input.GetKeyDown(KeyCode.W)){
            if(choosing == 2) choosing = prev_top;
        }
        else if(Input.GetKeyDown(KeyCode.S)){
            if(choosing != 2){
                prev_top = choosing;
                choosing = 2;
            }
        }
        else if(Input.GetKeyDown(KeyCode.A)){
            if(choosing == 1) choosing = 0;
        }
        else if(Input.GetKeyDown(KeyCode.D)){
            if(choosing == 0) choosing = 1;
        }

        if(choosing != prev_choosing){
            ssms.PlaySE(ssms.SE_choose);
            ums.SetChoosingUI("account", prev_choosing, choosing);
        }
    }
}
