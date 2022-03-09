using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBController : EnemyController
{
    GameObject player;
    PlayerController pcs;
    Vector3 p_pos;

    int angle = 1;  // 012...左中右
    SpriteRenderer sprite_ren, sprite_f_ren;
    public Sprite[] sprites = new Sprite[3];
    public Sprite[] sprites_f = new Sprite[3];

    public override void InitVariables()
    {
        base.InitVariables();
        player = GameObject.Find("Player");
        pcs = player.GetComponent<PlayerController>();
        sprite_ren = sprite.GetComponent<SpriteRenderer>();
        sprite_f_ren = sprite_f.GetComponent<SpriteRenderer>();
    }

    public override void Update(){
        base.Update();
        // if(readyToShoot()) Shoot();
        SwitchAngle();
    }

    void SwitchAngle(){
        p_pos = player.transform.localPosition;
        float dx = p_pos.x + 17 - pos.x;
        float dy = p_pos.y + 4 - pos.y;
        float deg = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        int pre_angle = angle;
        if(112.5f < deg) angle = 0;
        else if(67.5f < deg && deg < 112.5f) angle = 1;
        else angle = 2;
        
        if(angle != pre_angle){
            sprite_ren.sprite = sprites[angle];
            sprite_f_ren.sprite = sprites_f[angle];
        }

    }
}
