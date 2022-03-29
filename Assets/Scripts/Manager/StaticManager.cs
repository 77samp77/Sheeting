using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticManager : MonoBehaviour
{
    [System.NonSerialized] public static bool canControll;
    [System.NonSerialized] public static int screenWidth = 270, screenHeight = 180;
    [System.NonSerialized] public static int gameLevel = 1;
    [System.NonSerialized] public static int gameLevel_max = 10;
    [System.NonSerialized] public static int[] levelStatus = new int[11]; // 0/1/2...選択不可/未クリア/クリア済
    [System.NonSerialized] public static int[] hiscore = new int[11];

    [System.NonSerialized] public static bool isSigningIn;
    [System.NonSerialized] public static string userName;

    [System.NonSerialized] public static float volume_BGM = 0.5f;
    [System.NonSerialized] public static float volume_SE = 0.5f;

    void Awake(){
        GameObject temp = GameObject.Find("StaticManager");
        if(temp != null) Destroy(this.gameObject);
        else{
            DontDestroyOnLoad(this.gameObject);
            this.name = "StaticManager";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i < levelStatus.Length; i++){
            if(i <= 5) levelStatus[i] = 1;
            else levelStatus[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LateUpdate(){
        canControll = true;
    }

    public static float map(float value, float a1, float a2, float b1, float b2){
        float temp = ((Mathf.Abs(value - a2) / Mathf.Abs(a2 - a1)) * Mathf.Abs(b2 - b1));
        if(b1 < b2) return temp + b1;
        else return temp + b2;
    }
}
