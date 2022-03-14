using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [System.NonSerialized] public GameObject gameManager;
    [System.NonSerialized] public GameManagerScript gms;

    [System.NonSerialized] public GameObject gameSound;
    [System.NonSerialized] public GameSoundManager gsms;

    [System.NonSerialized] public GameObject sheet;
    [System.NonSerialized] public SheetController scs;

    [System.NonSerialized] public Vector3 pos;

    public GameObject sprite, sprite_f;
    [System.NonSerialized] public GameObject spriteDefeatPrefab;

    public float v;
    public int score;

    [System.NonSerialized] public GameObject enemyShotPrefab, enemyShots;
    public int shoot_interval; // 連続で弾を撃つときの間隔(フレーム数) 
    [System.NonSerialized] public int frame_shot, frame_defeat; // 弾を撃ったとき, 撃墜時のフレーム
    [System.NonSerialized] public bool isDefeated; // 撃墜された
    public BoxCollider2D bc2D;

    [System.NonSerialized] public int SW, SH;

    // Start is called before the first frame update
    public virtual void Start()
    {
        InitVariables();
        SetFirstPosition();
    }

    public virtual void InitVariables(){
        gameManager = GameObject.Find("GameManager");
        gms = gameManager.GetComponent<GameManagerScript>();
        gameSound = GameObject.Find("GameSound");
        gsms = gameSound.GetComponent<GameSoundManager>();
        sheet = GameObject.Find("Sheet");
        scs = sheet.GetComponent<SheetController>();
        spriteDefeatPrefab = Resources.Load<GameObject>("Prefabs/Sprite_Defeat");
        enemyShots = GameObject.Find("EnemyShots");
        frame_shot = gms.progress;
        SW = StaticManager.screenWidth;
        SH = StaticManager.screenHeight;
    }

    public virtual void SetFirstPosition(){

    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(gms.gameIsStop) return;
        pos = transform.localPosition;
        if(!isDefeated){
            Move();
            SwitchIsCovered();
        }
        // else DefeatAnimation();
    }

    public virtual void Move(){

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

    // public virtual void DefeatAnimation(){
    //     int defeat_progress = gms.progress - frame_defeat;
    //     if(defeat_progress == 16) sprite_d_ren.sprite = gms.defeat_sprites[0];
    //     else if(defeat_progress == 4 || defeat_progress == 12) sprite_d_ren.sprite = gms.defeat_sprites[1];
    //     else if(defeat_progress == 8) sprite_d_ren.sprite = gms.defeat_sprites[2];

    //     if(defeat_progress == 20) Destroy(this.gameObject);
    // }

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
        frame_shot = gms.progress;
    }

    public virtual void PositionShot(GameObject eShot){

    }

    public virtual void Defeat(){
        frame_defeat = gms.progress;
        isDefeated = true;
        sprite.SetActive(false);
        sprite_f.SetActive(false);
        GameObject sprite_d = Instantiate(spriteDefeatPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        sprite_d.transform.SetParent(this.transform);
        SpriteDefeatController sdcs = sprite_d.GetComponent<SpriteDefeatController>();
        sdcs.SetObject(this.gameObject);
        bc2D.enabled = false;
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
