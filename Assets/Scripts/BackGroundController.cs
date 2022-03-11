using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackGroundController : MonoBehaviour
{
    public float v;
    public GameObject[] sprites = new GameObject[2];
    Vector2[] spr_pos = new Vector2[2];

    void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "SampleScene"){
            GameObject gameManager = GameObject.Find("GameManager");
            GameManagerScript gms = gameManager.GetComponent<GameManagerScript>();
            v = gms.gameSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 2; i++) spr_pos[i] = sprites[i].transform.localPosition;
        Move();
    }

    void Move(){
        for(int i = 0; i < 2; i++) spr_pos[i].x -= v;
        if(spr_pos[0].x < -Screen.width * 1.5f){
            for(int i = 0; i < 2; i++) spr_pos[i].x += Screen.width;
        }
        for(int i = 0; i < 2; i++) sprites[i].transform.localPosition = spr_pos[i];
    }
}
