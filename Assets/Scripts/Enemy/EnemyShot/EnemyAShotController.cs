using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAShotController : EnemyShotController
{
    public override void Move()
    {
        pos.x -= v + gms.gameSpeed;
        transform.localPosition = pos;
    }

    public override bool readyToDestroy()
    {
        return pos.x < -Screen.width / 2;
    }
}
