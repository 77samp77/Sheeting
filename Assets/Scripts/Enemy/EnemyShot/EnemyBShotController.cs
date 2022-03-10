using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBShotController : EnemyShotController
{
    int angle = 1; // 012...左中右
    bool isFlip;

    public override void Move()
    {
        pos.x -= gms.gameSpeed;
        if(angle == 0) pos.x -= v;
        else if(angle == 2) pos.x += v;
        if(isFlip) pos.y -= v;
        else pos.y += v;
        transform.localPosition = pos;
    }

    public void SetAngle(int _angle){
        sprite.SetActive(false);
        pos = transform.localPosition;
        angle = _angle;
        SpriteRenderer sprite_ren = sprite.GetComponent<SpriteRenderer>();
        if(angle == 0){
            sprite_ren.sprite = Resources.Load<Sprite>("Images/EnemyB_Shot_Left");
            pos.x--;
        }
        else if(angle == 2){
            sprite_ren.sprite = Resources.Load<Sprite>("Images/EnemyB_Shot_Right");
            pos.x++;
        }
        if(isFlip) pos.y++;
        else pos.y--;
        transform.localPosition = pos;
    }

    public void Flip(){
        sprite.GetComponent<SpriteRenderer>().flipY = isFlip = true;
    }
}
