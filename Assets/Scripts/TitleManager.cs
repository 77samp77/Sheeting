using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    // GameObject screenManager;
    public GameObject pressSpaceKey, backGround, systemSound;
    SystemSoundManager ssms;
    BackGroundController bgcs;

    void Awake(){
        Application.targetFrameRate = 60;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
        GameObject pskText = GameObject.Find("-PRESS SPACE KEY-");
        pskText.GetComponent<Text>().text = "w:" + Screen.width + ", h:" + Screen.height;
        Debug.Log("w:" + Screen.width + ", h:" + Screen.height);
    }

    void InitVariables(){
        // screenManager = GameObject.Find("ScreenManager");
        // screenManager.GetComponent<ScreenManager>().SetMainCamera();
        bgcs = backGround.GetComponent<BackGroundController>();
        bgcs.InitTitleVariables();
        systemSound = GameObject.Find("SystemSound");
        ssms = systemSound.GetComponent<SystemSoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)){
            ssms.PlaySE(ssms.SE_decide);
            SceneManager.LoadScene("LevelSelect");
        }

        if(Time.frameCount % 100 == 0) pressSpaceKey.SetActive(true);
        else if(Time.frameCount % 100 == 50) pressSpaceKey.SetActive(false);
    }
}
