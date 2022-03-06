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
        textObject.SetActive(true);
    }

    public override void Move(){
        pos.x += v;
        transform.localPosition = pos;
    }

    public override bool readyToDestroy()
    {
        return pos.x + colWidth < -Screen.width / 2;
    }
}
