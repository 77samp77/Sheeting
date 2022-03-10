using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotController : MonoBehaviour
{
    GameObject gameManager;
    GameManagerScript gms;

    public float vx = 2f;
    Vector3 pos;

    List<GameObject> colliders = new List<GameObject>();

    void Start()
    {
        InitVariables();
    }

    void InitVariables(){
        gameManager = GameObject.Find("GameManager");
        gms = gameManager.GetComponent<GameManagerScript>();
    }

    void Update()
    {
        pos = transform.localPosition;
        Move();
        if(pos.x > Screen.width / 2) Destroy(this.gameObject);
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
            if(wcs.textObject.activeSelf) Mark(colObject, wcs);
        }
        else if(colObject.tag == "Enemy"){
            EnemyController ecs;
            // if(colObject.name == "EnemyA") ecs = colObject.GetComponent<EnemyAController>();
            // else if(colObject.name == "EnemyB") ecs = colObject.GetComponent<EnemyBController>();
            // else ecs = colObject.GetComponent<EnemyController>();
            ecs = colObject.GetComponent<EnemyController>();
            HitEnemy(colObject, ecs);
        }
    }

    void Mark(GameObject word, WordController wcs){
        wcs.isMarked = true;
        wcs.mark.SetActive(true);
        Destroy(this.gameObject);
    }
    
    void HitEnemy(GameObject enemy, EnemyController ecs){
        gms.IncreaseScore(ecs.score);
        Destroy(enemy);
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
