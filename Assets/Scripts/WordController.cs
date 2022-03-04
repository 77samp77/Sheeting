using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordController : MonoBehaviour
{
    public GameObject textObject, mark;
    public float v;
    Vector3 pos;
    public bool isMarked;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void BeSetWord(string wordStr){
        textObject.GetComponent<Text>().text = wordStr;
        int colWidth = 5 * wordStr.Length;
        GetComponent<BoxCollider2D>().size = new Vector2(colWidth, 8);
        mark.GetComponent<RectTransform>().sizeDelta = new Vector2(colWidth, 7);
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.localPosition;
        Move();
        if(pos.x < -Screen.width / 2) Destroy(this.gameObject);
    }

    void Move(){
        pos.x += v;
        transform.localPosition = pos;
    }

    Vector3 cashPos, t_cashPos;
    void LateUpdate(){
        cashPos = transform.localPosition;
        transform.localPosition = new Vector3(Mathf.RoundToInt(cashPos.x),
                                              Mathf.RoundToInt(cashPos.y),
                                              Mathf.RoundToInt(cashPos.z));
        pos = transform.localPosition;
        textObject.transform.localPosition = new Vector3(pos.x + 79, pos.y - 12, pos.z);
        if(isMarked) mark.transform.localPosition = new Vector3(pos.x - 1, pos.y + 1, pos.z);
    }

    void OnRenderObject(){
        transform.localPosition = cashPos;
    }
}
