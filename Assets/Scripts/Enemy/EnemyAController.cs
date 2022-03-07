using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAController : EnemyController
{
    GameObject player;
    PlayerController pcs;
    Vector3 p_pos;

    int shoot_con_count;
    public int shoot_con_max, shoot_interval_con;

    public override void InitVariables()
    {
        base.InitVariables();
        player = GameObject.Find("Player");
        pcs = player.GetComponent<PlayerController>();
        enemyShotPrefab = (GameObject)Resources.Load("Prefabs/Enemy/EnemyA_Shot");
    }

    public override void Move()
    {
        p_pos = player.transform.localPosition;
        if(Mathf.Abs(pos.y - p_pos.y) < v) return;
        if(pos.y > p_pos.y) pos.y -= v;
        else pos.y += v;
        transform.localPosition = pos;
    }

    public override bool readyToShoot()
    {
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
