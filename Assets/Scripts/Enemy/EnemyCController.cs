using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCController : EnemyController
{
    public override void BeSetEnemy(int _pos, bool isTop)
    {
        pos.x = SW / 2 + 2;
        pos.y = _pos;
        transform.localPosition = pos;
    }

    public override void Move(){
        pos.x -= gms.gameSpeed;
        transform.localPosition = pos;
    }
}
