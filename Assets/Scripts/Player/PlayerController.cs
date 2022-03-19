using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject gameManager;
    GameManagerScript gms;
    public GameObject gameMusic, gameSound;
    GameMusicManager gmms;
    GameSoundManager gsms;

    Sprite spr;
    public GameObject playerShotPrefab, playerShots;
    public GameObject sheet;
    SheetController scs;

    [System.NonSerialized] public Vector3 pos;
    [System.NonSerialized] public Vector2 spr_scl;

    [System.NonSerialized] public bool readyToStart;

    float v;
    public float v_default, v_dash;
    public int shoot_interval; // 連続で弾を撃つときの間隔(フレーム数) 
    int frame_shot; // 弾を撃ったときのフレーム

    public int damage_interval; // ダメージ後の無敵時間(フレーム数) 
    int frame_damaged = -1000, frame_gameOver = 0;
    public GameObject spriteDefeatPrefab;

    int SW, SH;

    void Start()
    {
        InitVariables();
        ResetVariables();
    }

    void InitVariables(){
        gms = gameManager.GetComponent<GameManagerScript>();
        gmms = gameMusic.GetComponent<GameMusicManager>();
        gsms = gameSound.GetComponent<GameSoundManager>();
        spr = GetComponent<SpriteRenderer>().sprite;
        spr_scl = spr.bounds.size;
        scs = sheet.GetComponent<SheetController>();
        SW = StaticManager.screenWidth;
        SH = StaticManager.screenHeight;
        v = v_dash;
    }

    void Update()
    {
        if(!readyToStart) StartMotion();

        if(gms.gameIsStop) return;
        if(gms.progress - frame_damaged <= damage_interval) DamageAnimation();
        pos = transform.localPosition;
        Move();
        // if(Input.GetKey(KeyCode.Space)){
            if(gms.isStart && gms.progress - frame_shot > shoot_interval && pos.y > scs.pos.y) Shoot();
        // }
    }

    void StartMotion(){
        pos = transform.localPosition;
        if(pos.x < -110) pos.x += 0.5f;
        else{
            gmms.PlayBGM(gmms.bgm);
            pos.x = -110;
            readyToStart = true;
        }
        transform.localPosition = pos;
    }

    void DamageAnimation(){
        int eFrame = gms.progress - frame_damaged;
        Color sprite_color = GetComponent<SpriteRenderer>().color;
        if(eFrame == damage_interval) sprite_color.a = 1;
        else if(eFrame % 10 == 0){
            if(eFrame % 20 == 0) sprite_color.a = 0.5f;
            else sprite_color.a = 1;
        }
        GetComponent<SpriteRenderer>().color = sprite_color;
    }

    void Move(){
        if(Input.GetKeyDown(KeyCode.LeftShift)) v = v_dash;
        else if(Input.GetKeyUp(KeyCode.LeftShift)) v = v_default;
        
        if(Input.GetKey(KeyCode.W)){
            if(pos.y >= SH / 2) pos.y = SH / 2;
            else pos.y += v;
        }
        if(Input.GetKey(KeyCode.A)){
            if(pos.x <= -SW / 2) pos.x = -SW / 2;
            else pos.x -= v;
        }
        if(Input.GetKey(KeyCode.S)){
            if(pos.y - spr_scl.y <= scs.bottom) pos.y = scs.bottom + spr_scl.y;
            else pos.y -= v;
        }
        if(Input.GetKey(KeyCode.D)){
            if(pos.x + spr_scl.x >= SW / 2) pos.x = SW / 2 - spr_scl.x;
            else pos.x += v;
        }
        transform.localPosition = pos;
    }

    void Shoot(){
        // gsms.PlaySE(gsms.SE_shot_player);
        GameObject pShot = Instantiate(playerShotPrefab, 
                                       new Vector3(pos.x + spr_scl.x * 0.8f, pos.y - spr_scl.y / 2, 0), 
                                       Quaternion.identity);
        pShot.name = playerShotPrefab.name;
        pShot.transform.SetParent(playerShots.transform);

        frame_shot = gms.progress;
    }

    public void Damage(){
        if(gms.progress - frame_damaged < damage_interval) return;
        gsms.PlaySE(gsms.SE_damage);
        gms.DecreaseLife();
        if(gms.life == 0) GameOver();
        else frame_damaged = gms.progress;
    }

    void GameOver(){
        gms.GameOver();
    }

    int sprite_d_interval = 15;
    public void GameOverAnimation(){
        if(frame_gameOver == sprite_d_interval * 4 + 10){
            this.gameObject.SetActive(false);
            gms.GameFinish();
        }
        else if(frame_gameOver % sprite_d_interval == 0 && frame_gameOver < sprite_d_interval * 4){
            GameObject sprite_d = Instantiate(spriteDefeatPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            sprite_d.transform.SetParent(this.transform);
            SpriteDefeatController sdcs = sprite_d.GetComponent<SpriteDefeatController>();
            sdcs.SetObject(this.gameObject);
        }
        frame_gameOver++;
    }

    public void ResetVariables(){
        this.gameObject.SetActive(true);
        pos.x = -170;
        pos.y = 20;
        v = v_default;
        transform.localPosition = pos;
        readyToStart = false;
        frame_shot = -1000;
        frame_damaged = -1000;
        frame_gameOver = 0;
        Color sprite_color = GetComponent<SpriteRenderer>().color;
        sprite_color.a = 1;
        GetComponent<SpriteRenderer>().color = sprite_color;
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
