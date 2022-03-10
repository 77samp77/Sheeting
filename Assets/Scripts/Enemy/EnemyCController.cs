using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCController : EnemyController
{
    public override void Move(){
        pos.x -= gms.gameSpeed;
        transform.localPosition = pos;
    }
}
