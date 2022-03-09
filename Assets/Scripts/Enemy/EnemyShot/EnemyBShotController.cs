using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBShotController : EnemyShotController
{
    int angle = 1; // 012...左中右

    public override void Move()
    {
        pos.x -= gms.gameSpeed;
        if(angle == 0) pos.x -= v;
        else if(angle == 2) pos.x += v;
        pos.y += v;
        transform.localPosition = pos;
    }

    public void SetAngle(int _angle){
        pos = transform.localPosition;
        angle = _angle;
        SpriteRenderer sprite_ren = sprite.GetComponent<SpriteRenderer>();
        if(angle == 0){
            sprite_ren.sprite = Resources.Load<Sprite>("Images/EnemyB_Shot_Left");
            pos.x--;
            pos.y--;
        }
        else if(angle == 2){
            sprite_ren.sprite = Resources.Load<Sprite>("Images/EnemyB_Shot_Right");
            pos.x++;
            pos.y--;
        }
        transform.localPosition = pos;
        sprite.SetActive(true);
    }
}
