using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordController : MonoBehaviour
{
    public GameObject /*canvas, */text, mark;
    public float v;
    Vector3 pos;
    public bool isMarked;

    // Start is called before the first frame update
    void Start()
    {
        // canvas = transform.Find("Canvas").gameObject;
        // canvas.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
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
        text.transform.localPosition = new Vector3(pos.x + 79, pos.y - 12, pos.z);
        if(isMarked) mark.transform.localPosition = new Vector3(pos.x - 1, pos.y + 1, pos.z);
    }

    void OnRenderObject(){
        transform.localPosition = cashPos;
    }
}
