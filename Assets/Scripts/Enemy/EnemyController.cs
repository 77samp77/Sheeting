using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [System.NonSerialized] public GameObject gameManager;
    [System.NonSerialized] public GameManagerScript gms;

    [System.NonSerialized] public GameObject sheet;
    [System.NonSerialized] public SheetController scs;

    [System.NonSerialized] public Vector3 pos;

    // [System.NonSerialized] public bool isCovered;
    public GameObject sprite, sprite_f;

    public float v;
    public int score;

    [System.NonSerialized] public GameObject enemyShotPrefab, enemyShots;
    public int shoot_interval; // 連続で弾を撃つときの間隔(フレーム数) 
    [System.NonSerialized] public int frame_shot; // 弾を撃ったときのフレーム

    // Start is called before the first frame update
    public virtual void Start()
    {
        InitVariables();
        SetFirstPosition();
    }

    public virtual void InitVariables(){
        gameManager = GameObject.Find("GameManager");
        gms = gameManager.GetComponent<GameManagerScript>();
        sheet = GameObject.Find("Sheet");
        scs = sheet.GetComponent<SheetController>();
        enemyShots = GameObject.Find("EnemyShots");
    }

    public virtual void SetFirstPosition(){

    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(gms.gameIsStop) return;
        pos = transform.localPosition;
        Move();
        SwitchIsCovered();
    }

    public virtual void SwitchIsCovered(){
        if(sprite.activeSelf){
            if(pos.y < scs.pos.y){
                sprite.SetActive(false);
                sprite_f.SetActive(true);
            }
        }
        else if(pos.y > scs.pos.y){
            sprite.SetActive(true);
            sprite_f.SetActive(false);
        }
    }

    public virtual void Move(){

    }

    public virtual bool readyToShoot(){
        return false;
    }

    [System.NonSerialized] public GameObject eShot;
    public virtual void Shoot(){
        eShot = Instantiate(enemyShotPrefab, 
                                       new Vector3(pos.x, pos.y, 0), 
                                       Quaternion.identity);
        PositionShot(eShot);
        eShot.name = enemyShotPrefab.name;
        eShot.transform.SetParent(enemyShots.transform);
        frame_shot = Time.frameCount;
    }

    public virtual void PositionShot(GameObject eShot){

    }
    
    public virtual void OnTriggerStay2D(Collider2D collider){
        if(!sprite.activeSelf) return;
        GameObject colObject = collider.gameObject;
        if(colObject.tag == "Player"){
            PlayerController pcs = colObject.GetComponent<PlayerController>();
            pcs.Damage();
        }
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
