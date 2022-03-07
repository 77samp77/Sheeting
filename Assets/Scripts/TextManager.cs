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
}
