using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string scoreText(int score){
        return "SCORE: " + score.ToString("00000000");
    }

    public string wordCountText(int gain, int quota){
        string gain_color = "";
        if(gain >= quota) gain_color = "#328719";
        else gain_color = "#C80000";
        return "<color=" + gain_color + ">" + gain + "</color>/" + quota;
    }

    public string finishText(bool isGameOver){
        if(isGameOver) return "GAME OVER...";
        else return "FINISH!!";
    }

    public string resultSFText(bool isSuccess){
        string SF_color = "";
        if(isSuccess){
            SF_color = "#FF6464";
            return "<color=" + SF_color + ">SUCCESS!</color>";
        }
        else{
            SF_color = "#6464FF";
            return "<color=" + SF_color + ">FAILURE..</color>";
        }
    }

    public string resultScoreText(int score){
        string scoreText = "SCORE ";
        string scoreToString = score.ToString();
        int digitCount = scoreToString.Length;
        for(int i = 0; i < 8 - digitCount; i++) scoreText += " ";
        scoreText += scoreToString;
        return scoreText;
    }
    
    public string resultLifeBonusText(int lifeBonus){
        string lifeBonusText = "LIFE  +  ";
        lifeBonus = Mathf.Max(lifeBonus, 0);
        string lifeBonusToString = lifeBonus.ToString();
        int digitCount = lifeBonusToString.Length;
        for(int i = 0; i < 4 - digitCount; i++) lifeBonusText += " ";
        lifeBonusText += lifeBonusToString;
        return lifeBonusText;
    }

    public string resultTotalScoreText(int total_score){
        return total_score.ToString("00000000");
    }
}
