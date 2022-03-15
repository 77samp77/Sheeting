using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    GameObject systemSound;
    SystemSoundManager ssms;
    GameObject backGround;
    BackGroundController bgcs;

    void Awake(){
        StaticManager.canControll = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
    }

    void InitVariables(){
        systemSound = GameObject.Find("SystemSound");
        ssms = systemSound.GetComponent<SystemSoundManager>();
        backGround = GameObject.Find("BackGround");
        bgcs = backGround.GetComponent<BackGroundController>();
        bgcs.v = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)){
            ssms.PlaySE(ssms.SE_back);
            SceneManager.LoadScene("Title");
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            ssms.PlaySE(ssms.SE_decide);
            SceneManager.LoadScene("Game");
        }
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            StaticManager.gameLevel = 1;
            ssms.PlaySE(ssms.SE_choose);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            StaticManager.gameLevel = 2;
            ssms.PlaySE(ssms.SE_choose);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            StaticManager.gameLevel = 3;
            ssms.PlaySE(ssms.SE_choose);
        }
    }
}
