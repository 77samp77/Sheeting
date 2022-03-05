using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetController : MonoBehaviour
{
    public int top, bottom, v;
    bool isCovering, toCover, toUncover;

    public Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.localPosition;
        Controll();
        Move();
    }

    void Controll(){
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            if(!isCovering && !toUncover) isCovering = toCover = true;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            if(isCovering && !toCover){
                isCovering = false;
                toUncover = true;
            }
        }
    }

    void Move(){
        if(toCover) Cover();
        if(toUncover) Uncover();
    }

    void Cover(){
        if(pos.y < top) pos.y += v;
        else{
            toCover = false;
            pos.y = top;
        }
        transform.localPosition = pos;
    }

    void Uncover(){
        if(pos.y > bottom) pos.y -= v;
        else{
            toUncover = false;
            pos.y = bottom;
        }
        transform.localPosition = pos;
    }

    /*=描画用===================================================================*/
    Vector3 cashPos;
    void LateUpdate(){
        cashPos = transform.localPosition;
        transform.localPosition = new Vector3(Mathf.RoundToInt(cashPos.x),
                                              Mathf.RoundToInt(cashPos.y),
                                              Mathf.RoundToInt(cashPos.z));
    }

    void OnRenderObject(){
        transform.localPosition = cashPos;
    }
}
