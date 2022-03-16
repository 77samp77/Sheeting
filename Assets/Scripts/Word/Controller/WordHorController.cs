using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordHorController : WordController
{
    public override void Start()
    {
        base.Start();
        // readyToSetPos = true;
    }

    public override void SetFirstPosition(int _pos, float speed, bool isTop){
        pos.x = SW / 2;
        pos.y = _pos;
        transform.localPosition = pos;
        v = speed;
        textObject.SetActive(true);
    }
    
    public override void BeSetWord(string wordStr){
        base.BeSetWord(wordStr);
        BoxCollider2D bc2d = GetComponent<BoxCollider2D>();
        bc2d.offset = new Vector2(colWidth / 2 - 1, -2.5f);
        bc2d.size = new Vector2(colWidth, 8);
    }

    public override void Move(){
        base.Move();
        pos.x -= v + gms.gameSpeed;
        transform.localPosition = pos;
    }

    public override bool readyToDestroy()
    {
        return pos.x + colWidth < -SW / 2;
    }

    public override void LateUpdate(){
        base.LateUpdate();
        textObject.transform.localPosition = new Vector3(pos.x + 79, pos.y - 12, pos.z);
        if(isMarked) mark.transform.localPosition = new Vector3(pos.x - 1, pos.y - 6, pos.z);
        if(isCovered) mark_c.transform.localPosition = new Vector3(pos.x - 1, pos.y - 6, pos.z);
    }



    // -------------ランダム生成--------------------------------
    public override void SetFirstPosition()
    {
        int fy = Random.Range(scs.bottom + 6, SH / 2 - 1);
        pos.x = SW / 2;
        pos.y = fy;
        transform.localPosition = pos;
        v = Random.Range(0.2f, 1.2f);
        readyToSetPos = false;
    }
    // -------------------------------------------------------
}
