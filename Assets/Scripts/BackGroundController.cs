using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackGroundController : MonoBehaviour
{
    public float v;
    public GameObject[] sprites = new GameObject[2];
    Vector2[] spr_pos = new Vector2[2];

    GameObject gameManager;
    GameManagerScript gms;

    int SW, SH;

    void Awake(){
        GameObject temp = GameObject.Find("BackGround");
        if(temp != null) Destroy(this.gameObject);
        else{
            DontDestroyOnLoad(this.gameObject);
            this.name = "BackGround";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
    }

    void InitVariables(){
        SW = StaticManager.screenWidth;
        SH = StaticManager.screenHeight;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 2; i++) spr_pos[i] = sprites[i].transform.localPosition;
        if(SceneManager.GetActiveScene().name == "Game" && gms.gameIsStop && gms.isStart) return;
        Move();
    }

    void Move(){
        for(int i = 0; i < 2; i++) spr_pos[i].x -= v;
        if(spr_pos[0].x < -SW * 1.5f){
            for(int i = 0; i < 2; i++) spr_pos[i].x += SW;
        }
        for(int i = 0; i < 2; i++) sprites[i].transform.localPosition = spr_pos[i];
    }

    // public void InitTitleVariables(){
    //     v = 0.2f;
    // }

    public void InitGameVariables(){
        gameManager = GameObject.Find("GameManager");
        gms = gameManager.GetComponent<GameManagerScript>();
        v = gms.gameSpeed;
    }
}
