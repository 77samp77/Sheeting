using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject textManager;
    TextManager tms;

    public GameObject scoreUI, wordCountUI;
    public GameObject[] lifeSprites = new GameObject[6];
    public GameObject progressBar;

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
    }

    void InitVariables(){
        tms = textManager.GetComponent<TextManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScoreUI(int score){
        scoreUI.GetComponent<Text>().text = tms.scoreText(score);
    }

    public void SetWordCountUI(int gain, int quota){
        wordCountUI.GetComponent<Text>().text = tms.wordCountText(gain, quota);
    }

    public void SetProgressBarUI(int progress, int limit){
        Vector3 bar_scl = progressBar.transform.localScale;
        bar_scl.x = progress / (float)limit;
        progressBar.transform.localScale = bar_scl;
    }
}
