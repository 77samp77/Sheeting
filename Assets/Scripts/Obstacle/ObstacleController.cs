using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : EnemyShotController
{
    [System.NonSerialized] public int SW;

    public virtual void Awake(){
        SW = StaticManager.screenWidth;
    }

    public virtual void BeSetObstacle(int pos){

    }

    public override bool readyToDestroy(){
        if(pos.x < -SW / 2 - 10) return true;
        return false;
    }
}
