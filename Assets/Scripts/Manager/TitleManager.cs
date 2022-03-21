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
        StaticManager.canControll = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
    }

    void InitVariables(){
        bgcs = backGround.GetComponent<BackGroundController>();
        systemSound = GameObject.Find("SystemSound");
        ssms = systemSound.GetComponent<SystemSoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            ssms.PlaySE(ssms.SE_decide);
            SceneManager.LoadScene("LevelSelect");
        }

        if(Time.frameCount % 100 == 0) pressSpaceKey.SetActive(true);
        else if(Time.frameCount % 100 == 50) pressSpaceKey.SetActive(false);
    }
}
