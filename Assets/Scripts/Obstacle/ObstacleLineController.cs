using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLineController : ObstacleController
{
    Vector2 sprite_scl;
    
    public override void BeSetObstacle(int pos){
        transform.localPosition = new Vector2(137, 90);
    }

    public override void Update()
    {
        base.Update();
        sprite_scl = sprite.transform.localScale;
        sprite_scl.y = StaticManager.map(scs.pos.y, scs.bottom, scs.top, 1, 0);
        sprite.transform.localScale = sprite_scl;
    }

    public override void Move()
    {
        pos.x -= gms.gameSpeed;
        transform.localPosition = pos;
    }

    public override void OnTriggerStay2D(Collider2D collider){
        GameObject colObject = collider.gameObject;
        if(colObject.tag == "Player"){
            PlayerController pcs = colObject.GetComponent<PlayerController>();
            if(pcs.pos.y <= scs.pos.y) return;
            pcs.Damage();
        }
    }
}
