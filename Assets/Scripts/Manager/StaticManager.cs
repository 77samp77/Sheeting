using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticManager : MonoBehaviour
{
    [System.NonSerialized] public static bool canControll;
    [System.NonSerialized] public static int screenWidth = 270, screenHeight = 180;
    [System.NonSerialized] public static int gameLevel = 1;

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
