using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_LevelSelect : MonoBehaviour
{
    public GameObject levelSelectManager;
    LevelSelectManager lsms;

    public GameObject[] levelButtonUI = new GameObject[11];
    public GameObject[] levelButtonNum = new GameObject[11];
    public GameObject[] choosingUI = new GameObject[12];
    public Sprite[] levelButtonSprite = new Sprite[3];

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
    }

    void InitVariables(){
        lsms = levelSelectManager.GetComponent<LevelSelectManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetChoosingUI(int prev, int now){
        choosingUI[prev].SetActive(false);
        choosingUI[now].SetActive(true);
    }

    public void SetLevelButtonsColor(){
        int[] levelButtonStatus = StaticManager.levelStatus;
        for(int i = 1; i < levelButtonStatus.Length; i++){
            Image lb_image = levelButtonUI[i].GetComponent<Image>();
            lb_image.sprite = levelButtonSprite[levelButtonStatus[i]];
            levelButtonNum[i].SetActive(levelButtonStatus[i] != 0);
        }
    }
}
