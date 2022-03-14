using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    // GameObject screenManager;
    GameObject systemSound;
    SystemSoundManager ssms;

    void Awake(){
        StaticManager.canControll = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
    }

    void InitVariables(){
        // screenManager = GameObject.Find("ScreenManager");
        // screenManager.GetComponent<ScreenManager>().SetMainCamera();
        systemSound = GameObject.Find("SystemSound");
        ssms = systemSound.GetComponent<SystemSoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(StaticManager.canControll){
            if(Input.GetKeyDown(KeyCode.Z)){
                ssms.PlaySE(ssms.SE_back);
                SceneManager.LoadScene("Title");
            }
            if(Input.GetKeyDown(KeyCode.Space)){
                ssms.PlaySE(ssms.SE_decide);
                SceneManager.LoadScene("Game");
            }
        // }
    }
}
