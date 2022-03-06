using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordVerController : WordController
{
    bool isUp;

    public override void SetFirstPosition()
    {
        isUp = Random.value < 0.5f;
        int fx = Random.Range(Screen.width / 4, Screen.width / 2 - 8);
        pos.x = fx;
        if(isUp) pos.y = scs.bottom + 20;
        else pos.y = 110 + colWidth;
        transform.localPosition = pos;
        readyToSetPos = false;
        textObject.SetActive(true);
    }

    public override void BeSetWord(string wordStr){
        textObject.GetComponent<Text>().text = wordStr;
        colWidth = 5 * wordStr.Length;
        BoxCollider2D bc2d = GetComponent<BoxCollider2D>();
        bc2d.offset = new Vector2(-46.5f, -22 - colWidth / 2);
        bc2d.size = new Vector2(7, colWidth);
        mark.GetComponent<RectTransform>().sizeDelta = new Vector2(colWidth, 7);
        mark_c.GetComponent<RectTransform>().sizeDelta = new Vector2(colWidth, 7);
        readyToSetPos = true;
    }

    public override void Move(){
        if(isUp) pos.y += v;
        else pos.y -= v;
        transform.localPosition = pos;
    }

    public override bool readyToGain(){
        if(pos.y >= scs.bottom + 20) return false;
        if(!isCovered) return false;
        return true;
    }

    public override bool readyToDestroy()
    {
        return (pos.y < scs.bottom + 20) || (pos.y > 110 + colWidth);
    }

    public override void LateUpdate(){
        cashPos = transform.localPosition;
        transform.localPosition = new Vector3(Mathf.RoundToInt(cashPos.x),
                                              Mathf.RoundToInt(cashPos.y),
                                              Mathf.RoundToInt(cashPos.z));
        pos = transform.localPosition;
        textObject.transform.localPosition = new Vector3(pos.x - 41, pos.y - 22, pos.z);
        if(isMarked) mark.transform.localPosition = new Vector3(pos.x - 50, pos.y - 22, pos.z);
        if(isCovered) mark_c.transform.localPosition = new Vector3(pos.x - 50, pos.y - 22, pos.z);
    }
}
