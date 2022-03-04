using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordController : MonoBehaviour
{
    public GameObject text;
    public float v;
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.localPosition;
        Move();
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
    }

    void OnRenderObject(){
        transform.localPosition = cashPos;
    }
}
