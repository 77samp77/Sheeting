using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordHorController : WordController
{
    public override void Start()
    {
        base.Start();
        readyToSetPos = true;
    }

    public override void SetFirstPosition()
    {
        int fy = Random.Range(scs.bottom + 6, Screen.height / 2 - 1);
        pos.x = Screen.width / 2;
        pos.y = fy;
        transform.localPosition = pos;
        readyToSetPos = false;
    }
    
    public override void BeSetWord(string wordStr){
        base.BeSetWord(wordStr);
        BoxCollider2D bc2d = GetComponent<BoxCollider2D>();
        bc2d.offset = new Vector2(colWidth / 2 - 1, -2.5f);
        bc2d.size = new Vector2(colWidth, 8);
    }

    public override void Move(){
        base.Move();
        pos.x += v;
        transform.localPosition = pos;
    }

    public override bool readyToDestroy()
    {
        return pos.x + colWidth < -Screen.width / 2;
    }

    public override void LateUpdate(){
        base.LateUpdate();
        textObject.transform.localPosition = new Vector3(pos.x + 79, pos.y - 12, pos.z);
        if(isMarked) mark.transform.localPosition = new Vector3(pos.x - 1, pos.y - 6, pos.z);
        if(isCovered) mark_c.transform.localPosition = new Vector3(pos.x - 1, pos.y - 6, pos.z);
    }
}
