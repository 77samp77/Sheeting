using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public GameObject canvas, textObject, mark, mark_c;
    public float v;
    Vector3 pos;
    int colWidth;
    public bool isMarked, isCovered;

    GameObject sheet;
    SheetController scs;

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
        canvas.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    
    void InitVariables(){
        sheet = GameObject.Find("Sheet");
        scs = sheet.GetComponent<SheetController>();
    }

    public void BeSetWord(string wordStr){
        textObject.GetComponent<Text>().text = wordStr;
        colWidth = 5 * wordStr.Length;
        BoxCollider2D bc2d = GetComponent<BoxCollider2D>();
        bc2d.offset = new Vector2(colWidth / 2 - 1, -2.5f);
        bc2d.size = new Vector2(colWidth, 8);
        mark.GetComponent<RectTransform>().sizeDelta = new Vector2(colWidth, 7);
        mark_c.GetComponent<RectTransform>().sizeDelta = new Vector2(colWidth, 7);
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.localPosition;
        Move();
        if(pos.y < scs.pos.y && isMarked) BeCovered();

        if(pos.y < scs.bottom) Destroy(this.gameObject);
        if(pos.x + colWidth < -Screen.width / 2) Destroy(this.gameObject);
    }

    void Move(){
        pos.x += v;
        transform.localPosition = pos;
    }

    void BeCovered(){
        isCovered = true;
        mark_c.SetActive(true);
        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }

    /*=描画用===================================================================*/
    Vector3 cashPos;
    void LateUpdate(){
        cashPos = transform.localPosition;
        transform.localPosition = new Vector3(Mathf.RoundToInt(cashPos.x),
                                              Mathf.RoundToInt(cashPos.y),
                                              Mathf.RoundToInt(cashPos.z));
        pos = transform.localPosition;
        textObject.transform.localPosition = new Vector3(pos.x + 79, pos.y - 12, pos.z);
        if(isMarked) mark.transform.localPosition = new Vector3(pos.x - 1, pos.y - 6, pos.z);
        if(isCovered) mark_c.transform.localPosition = new Vector3(pos.x - 1, pos.y - 6, pos.z);
    }

    void OnRenderObject(){
        transform.localPosition = cashPos;
    }
}
