using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAController : EnemyController
{
    GameObject player;
    PlayerController pcs;
    Vector3 p_pos;

    public override void InitVariables()
    {
        base.InitVariables();
        player = GameObject.Find("Player");
        pcs = player.GetComponent<PlayerController>();
    }

    public override void Move()
    {
        p_pos = player.transform.localPosition;
        if(Mathf.Abs(pos.y - p_pos.y) < v) return;
        if(pos.y > p_pos.y) pos.y -= v;
        else pos.y += v;
        transform.localPosition = pos;
    }
}
