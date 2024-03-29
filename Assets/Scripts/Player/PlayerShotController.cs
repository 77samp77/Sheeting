using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotController : MonoBehaviour
{
    GameObject gameManager;
    GameManagerScript gms;

    GameObject gameSound;
    GameSoundManager gsms;

    public float vx = 2f;
    Vector3 pos;

    List<GameObject> colliders = new List<GameObject>();

    int SW;

    void Start()
    {
        InitVariables();
    }

    void InitVariables(){
        gameManager = GameObject.Find("GameManager");
        gms = gameManager.GetComponent<GameManagerScript>();
        gameSound = GameObject.Find("GameSound");
        gsms = gameSound.GetComponent<GameSoundManager>();
        SW = StaticManager.screenWidth;
    }

    void Update()
    {
        if(gms.gameIsStop) return;
        pos = transform.localPosition;
        Move();
        if(pos.x > SW / 2) Destroy(this.gameObject);
    }

    void Move(){
        pos.x += vx;
        transform.localPosition = pos;
    }

    void OnTriggerEnter2D(Collider2D collider){
        GameObject colObject = collider.gameObject;
        if(colObject.tag == "Word"){
            WordController wcs;
            if(colObject.name == "Word_Hor") wcs = colObject.GetComponent<WordHorController>();
            else wcs = colObject.GetComponent<WordVerController>();

            if(wcs.textObject.activeSelf){
                if(!wcs.isMarked) Mark(colObject, wcs);
                else Destroy(this.gameObject);
            }
        }
        else if(colObject.tag == "Enemy"){
            EnemyController ecs = colObject.GetComponent<EnemyController>();
            HitEnemy(colObject, ecs);
        }
    }

    void Mark(GameObject word, WordController wcs){
        gsms.PlaySE(gsms.SE_mark);
        wcs.isMarked = true;
        wcs.mark.SetActive(true);
        Destroy(this.gameObject);
    }
    
    void HitEnemy(GameObject enemy, EnemyController ecs){
        gms.IncreaseScore(ecs.score);
        ecs.Defeat();
        Destroy(this.gameObject);
    }

    /*=描画用===================================================================*/
    Vector3 cashPos;
    void LateUpdate(){
        cashPos = transform.localPosition;
        transform.localPosition = new Vector3(Mathf.RoundToInt(cashPos.x),
                                              Mathf.RoundToInt(cashPos.y),
                                              Mathf.RoundToInt(cashPos.z));
    }

    void OnRenderObject(){
        transform.localPosition = cashPos;
    }
}
