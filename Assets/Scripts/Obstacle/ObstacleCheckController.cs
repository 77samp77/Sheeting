using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCheckController : ObstacleController
{
    public override void BeSetObstacle(int pos){
        transform.localPosition = new Vector2(SW / 2 + 2, pos);
    }

    public override void Move()
    {
        pos.x -= gms.gameSpeed;
        transform.localPosition = pos;
    }

    public override void OnTriggerStay2D(Collider2D collider){
        if(!sprite.activeSelf) return;
        GameObject colObject = collider.gameObject;
        if(colObject.tag == "Player"){
            PlayerController pcs = colObject.GetComponent<PlayerController>();
            pcs.Damage();
        }
    }
}
