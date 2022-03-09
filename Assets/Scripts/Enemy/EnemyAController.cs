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

    public override void SetFirstPosition(){
        transform.localPosition = new Vector3(Screen.width / 2, 0, 0);
    }
    
    public override void Update(){
        base.Update();
        if(readyToShoot()) Shoot();
        isShootMode = isStartShoot && Time.frameCount - frame_startedShoot < stayFrame;
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
            if(pos.x > 90) pos.x -= 0.4f;
            else{
                pos.x = 90;
                isShootMode = isStartShoot = true;
                shoot_con_count = shoot_con_max;
                frame_startedShoot = frame_shot = Time.frameCount;
            }
        }
        else{
            if(pos.x < Screen.width / 2) pos.x += 0.4f;
            else Destroy(this.gameObject);
        }
    }

    public override bool readyToShoot()
    {
        if(!isShootMode) return false;
        if(shoot_con_count < shoot_con_max) return Time.frameCount - frame_shot > shoot_interval_con;
        else return Time.frameCount - frame_shot > shoot_interval;
    }

    public override void Shoot()
    {
        base.Shoot();
        if(shoot_con_count == shoot_con_max) shoot_con_count = 1;
        else shoot_con_count++;
    }

    public override void PositionShot(GameObject eShot){
        Vector3 e_pos = eShot.transform.localPosition;
        e_pos.y -= 3;
        eShot.transform.localPosition = e_pos;
    }
}
