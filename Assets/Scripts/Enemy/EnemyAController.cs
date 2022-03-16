using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAController : EnemyController
{
    GameObject player;
    PlayerController pcs;
    Vector3 p_pos;

    bool isShootMode, isStartShoot;
    int frame_startedShoot;
    public int stayFrame;

    int shoot_con_count;
    public int shoot_con_max, shoot_interval_con;

    public override void InitVariables()
    {
        base.InitVariables();
        player = GameObject.Find("Player");
        pcs = player.GetComponent<PlayerController>();
        enemyShotPrefab = (GameObject)Resources.Load("Prefabs/Enemy/EnemyA_Shot");
        isShootMode = false;
    }

    public override void BeSetEnemy(int _pos, bool isTop)
    {
        transform.localPosition = new Vector2(SW / 2, _pos);
    }
    
    public override void Update(){
        base.Update();
        if(!isDefeated){
            if(readyToShoot()) Shoot();
            isShootMode = isStartShoot && gms.progress - frame_startedShoot < stayFrame;
        }
    }

    public override void Move()
    {
        p_pos = player.transform.localPosition;
        if(!isShootMode) AppearMotion();
        else{
            if(Mathf.Abs(pos.y - p_pos.y) < v) return;
            if(pos.y > p_pos.y) pos.y -= v;
            else pos.y += v;
        }
        transform.localPosition = pos;
    }

    void AppearMotion(){
        if(!isStartShoot){
            if(pos.x > 90) pos.x -= 1;
            else{
                pos.x = 90;
                isShootMode = isStartShoot = true;
                shoot_con_count = shoot_con_max;
                frame_startedShoot = frame_shot = gms.progress;
            }
        }
        else{
            if(pos.x < SW / 2) pos.x += 0.4f;
            else Destroy(this.gameObject);
        }
    }

    public override bool readyToShoot()
    {
        if(!isShootMode) return false;
        if(shoot_con_count < shoot_con_max) return gms.progress - frame_shot > shoot_interval_con;
        else return gms.progress - frame_shot > shoot_interval;
    }

    public override void Shoot()
    {
        base.Shoot();
        if(shoot_con_count == shoot_con_max) shoot_con_count = 1;
        else shoot_con_count++;
        gsms.PlaySE(gsms.SE_shot_enemyA);
    }

    public override void PositionShot(GameObject eShot){
        Vector3 e_pos = eShot.transform.localPosition;
        e_pos.y -= 3;
        eShot.transform.localPosition = e_pos;
    }



    // --------ランダム生成用-----------------------
    public override void SetFirstPosition(){
        transform.localPosition = new Vector3(SW / 2, 0, 0);
    }
    // ---------------------------------------------
}
