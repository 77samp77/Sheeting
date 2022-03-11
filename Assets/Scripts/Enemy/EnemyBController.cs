using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBController : EnemyController
{
    public GameObject sprite_b, sprite_all;
    bool isFlip;
    [System.NonSerialized] public bool toSetFirstPosition;
    GameObject player;
    PlayerController pcs;
    Vector3 p_pos;

    int angle = 1;  // 012...左中右
    SpriteRenderer sprite_ren, sprite_f_ren;
    public Sprite[] sprites = new Sprite[3];
    public Sprite[] sprites_f = new Sprite[3];

    public override void Start()
    {
        InitVariables();
    }

    public override void InitVariables()
    {
        base.InitVariables();
        player = GameObject.Find("Player");
        pcs = player.GetComponent<PlayerController>();
        enemyShotPrefab = (GameObject)Resources.Load("Prefabs/Enemy/EnemyB_Shot");
        sprite_ren = sprite.GetComponent<SpriteRenderer>();
        sprite_f_ren = sprite_f.GetComponent<SpriteRenderer>();
    }

    public void Flip(){
        sprite.GetComponent<SpriteRenderer>().flipY = true;
        sprite_f.GetComponent<SpriteRenderer>().flipY = true;
        sprite_b.SetActive(false);
        isFlip = toSetFirstPosition = true;
    }

    public override void Update(){
        base.Update();
        if(toSetFirstPosition) SetFirstPosition();
        if(!isDefeated){
            if(readyToShoot()) Shoot();
            SwitchAngle();
            if(pos.x + 6 < -Screen.width / 2) Destroy(this.gameObject);
        }
    }

    public override void SetFirstPosition()
    {
        float f_pos_y = -44;
        if(isFlip) f_pos_y = Screen.height / 2 - 6;
        transform.localPosition = new Vector3(Screen.width / 2 + 6, f_pos_y, 0);
        toSetFirstPosition = false;
        sprite_all.SetActive(true);
    }

    public override void Move()
    {
        p_pos = player.transform.localPosition;
        pos.x -= gms.gameSpeed;
        transform.localPosition = pos;
    }
    
    public override bool readyToShoot()
    {
        return gms.progress - frame_shot > shoot_interval;
    }

    void SwitchAngle(){
        p_pos = player.transform.localPosition;
        float dx = p_pos.x + 17 - pos.x;
        float dy = p_pos.y + 4 - pos.y;
        float deg = Mathf.Abs(Mathf.Atan2(dy, dx) * Mathf.Rad2Deg);

        int pre_angle = angle;
        if(112.5f < deg) angle = 0;
        else if(67.5f < deg && deg < 112.5f) angle = 1;
        else angle = 2;
        
        if(angle != pre_angle){
            sprite_ren.sprite = sprites[angle];
            sprite_f_ren.sprite = sprites_f[angle];
        }
    }

    public override void Shoot(){
        base.Shoot();
        EnemyBShotController ebscs = eShot.GetComponent<EnemyBShotController>();
        if(isFlip) ebscs.Flip();
        ebscs.SetAngle(angle);
    }
}
