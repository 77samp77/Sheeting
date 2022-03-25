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

    public GameObject accountUI, popUpUI;
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

    public void CloseAccountUI(){
        ums.SetChoosingUI("account", choosing, 2);
        accountUI.SetActive(false);
        canControll = false;
        Debug.Log("Close");
    }

    bool prev_accountUI, prev_popUpUI;
    // Update is called once per frame
    void Update()
    {
        if(!canControll){
            if(accountUI.activeSelf) canControll = true;
        }
        else if(!prev_popUpUI) ControllAccountUI();
        prev_accountUI = accountUI.activeSelf;
        prev_popUpUI = popUpUI.activeSelf;
    }

    void ControllAccountUI(){
        if(Input.GetKeyDown(KeyCode.Space)){
            ssms.PlaySE(ssms.SE_decide);
            if(choosing == 0) SignIn();
            else if(choosing == 1) SignUp();
            else if(choosing == 2) CloseAccountUI();
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

    void SignIn(){
        Debug.Log(userNameField.text + ", " + passwordField.text);

		NCMBUser user = new NCMBUser();
        NCMBUser.LogInAsync (userNameField.text, passwordField.text, (NCMBException e) => {    
			if(e != null) UnityEngine.Debug.Log ("サインイン失敗: " + e.ErrorMessage);
			else{
				StaticManager.isSigningIn = true;
				StaticManager.userName = userNameField.text;
				ReadUserData();
                ums.SetUserName(userNameField.text);
                UnityEngine.Debug.Log ("サインイン成功！");
			}
            ums.PopUp(e == null, userNameField.text);
		});
    }

    void ReadUserData(){
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("UserData");
        query.WhereEqualTo("UserName", StaticManager.userName);

        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>{
            if(e == null){
                foreach(NCMBObject obj in objList){
                    Debug.Log("LevelStatus: " + obj["LevelStatus"]);
                    string[] temp_ls = obj["LevelStatus"].ToString().Split(',');
                    for(int n = 0; n < 10; n++) StaticManager.levelStatus[n + 1] = int.Parse(temp_ls[n]);
                    
                    Debug.Log("Hiscore: " + obj["Hiscore"]);
                    string[] temp_h = obj["Hiscore"].ToString().Split(',');
                    for(int n = 0; n < 10; n++) StaticManager.hiscore[n + 1] = int.Parse(temp_h[n]);
                }
            }
            else Debug.Log("読み込み失敗");
        });
    }

    void SignUp(){
        NCMBUser user = new NCMBUser ();
		user.UserName = userNameField.text;
		user.Password = passwordField.text;
		user.SignUpAsync ((NCMBException e) => { 
			if (e != null){
                Debug.Log ("サインアップ失敗: " + e.ErrorMessage);
                ums.PopUp(false, userNameField.text);
            }
			else {
				Debug.Log ("サインアップ成功！");
				RegistUserData(userNameField.text);
				SignIn();
			}
		});
    }

    void RegistUserData(string userName){
        NCMBObject userData = new NCMBObject("UserData");
        userData["UserName"] = userName;

        int[] levelStatus = new int[11];
        for(int n = 1; n <= 10; n++){
            if(n <= 5) levelStatus[n] = 1;
            else levelStatus[n] = 0;
        }
        string text_levelStatus = "";
        for(int n = 1; n <= 10; n++){
            text_levelStatus += levelStatus[n].ToString();
            if(n != 10) text_levelStatus += ",";
        }
        userData["LevelStatus"] = text_levelStatus;

        int[] hiscore = new int[11];
        for(int n = 1; n <= 10; n++) hiscore[n] = 0;
        string text_hiscore = "";
        for(int n = 1; n <= 10; n++){
            text_hiscore += hiscore[n].ToString();
            if(n != 10) text_hiscore += ",";
        }
        userData["Hiscore"] = text_hiscore;

        userData.SaveAsync((NCMBException e) => { 
				if(e != null) UnityEngine.Debug.Log ("保存に失敗: " + e.ErrorMessage);
				else UnityEngine.Debug.Log ("保存に成功");
			}
		);
    }
}
