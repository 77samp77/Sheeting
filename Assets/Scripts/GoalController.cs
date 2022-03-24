using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : ObstacleController
{
    public override void BeSetObstacle(int pos){
        transform.localPosition = new Vector2(137, 90);
    }

    public override void Move()
    {
        pos.x -= gms.gameSpeed;
        transform.localPosition = pos;
    }
    
    public override void IsCoveredSwitch(){
    }

    public override void OnTriggerStay2D(Collider2D collider){
        GameObject colObject = collider.gameObject;
        if(colObject.tag == "Player" && !gms.isFinish) gms.GameFinish();
    }
}
