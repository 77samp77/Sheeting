using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject gameManager;
    GameManagerScript gms;

    public GameObject textManager;
    TextManager tms;

    public GameObject scoreUI, wordCountUI;
    public GameObject[] lifeSprites = new GameObject[6];
    public GameObject progressBar;

    public GameObject pauseUI;
    public GameObject[] pauseUI_choosing = new GameObject[3];

    public GameObject resultUI;
    public GameObject finishText, resultPaper;
    public GameObject result_score, result_sf, result_lifeBonus, result_noDamage, result_totalScore, result_newRecord;
    public GameObject result_clearBonus_dL, result_lifeBonus_dL, result_noDamage_dL;
    public GameObject[] resultUI_choosing = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
    }

    void InitVariables(){
        gms = gameManager.GetComponent<GameManagerScript>();
        tms = textManager.GetComponent<TextManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float r_vy = 3;
    public void ResultMotion(){
        if(finishText.activeSelf){
            r_vy -= 0.05f;
            Vector2 ft_pos = finishText.transform.localPosition;
            ft_pos.y -= r_vy;
            finishText.transform.localPosition = ft_pos;
            if(ft_pos.y > 110){
                finishText.SetActive(false);
                resultPaper.SetActive(true);
                r_vy = 3;
            }
        }
        else{
            Vector2 rp_pos = resultPaper.transform.localPosition;
            if(r_vy > 0){
                if(rp_pos.y < 90) r_vy -= 0.05f;
            }
            else{
                rp_pos.y = 0;
                gms.canControllUI = true;
            }
            rp_pos.y -= r_vy;
            resultPaper.transform.localPosition = rp_pos;
        }
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

    public void SetChoosingUI(GameObject[] cUI, int prev, int now){
        cUI[prev].SetActive(false);
        cUI[now].SetActive(true);
    }

    public void SetResultUI(bool isGameOver, bool isSuccess, int score,
                            int lifeBonus, bool isNoDamage, int total_score, bool isNewRecord){
        finishText.GetComponent<Text>().text = tms.finishText(isGameOver);
        result_sf.GetComponent<Text>().text = tms.resultSFText(isSuccess);
        result_score.GetComponent<Text>().text = tms.resultScoreText(score);
        result_lifeBonus.GetComponent<Text>().text = tms.resultLifeBonusText(lifeBonus);
        result_clearBonus_dL.SetActive(!isSuccess);
        result_lifeBonus_dL.SetActive(!isSuccess);
        result_noDamage_dL.SetActive(!isSuccess || !isNoDamage);
        result_newRecord.SetActive(isNewRecord);
        result_totalScore.GetComponent<Text>().text = tms.resultTotalScoreText(total_score);
        resultUI.SetActive(true);
    }

    public void ResetResultUI(){
        r_vy = 3;
        Vector2 ft_pos = finishText.transform.localPosition;
        ft_pos.y = 102;
        finishText.transform.localPosition = ft_pos;
        finishText.SetActive(true);
        Vector2 rp_pos = resultPaper.transform.localPosition;
        rp_pos.y = 170;
        resultPaper.transform.localPosition = rp_pos;
        resultPaper.SetActive(false);
        resultUI.SetActive(false);
    }
}
