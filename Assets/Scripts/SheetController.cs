using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetController : MonoBehaviour
{
    public GameObject gameManager;
    GameManagerScript gms;

    public GameObject gameSound;
    GameSoundManager gsms;

    public int top, bottom, v;
    public bool isCovering;
    public bool toCover, toUncover;

    public Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
    }

    void InitVariables(){
        gms = gameManager.GetComponent<GameManagerScript>();
        gsms = gameSound.GetComponent<GameSoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gms.gameIsStop) return;
        pos = transform.localPosition;
        Controll();
        Move();
    }

    void Controll(){
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            if(!isCovering && !toUncover){
                gsms.PlaySE(gsms.SE_sheet);
                isCovering = toCover = true;
            }
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            if(isCovering && !toCover){
                gsms.PlaySE(gsms.SE_sheet);
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
            gms.gain_combo = 0;
            pos.y = bottom;
        }
        transform.localPosition = pos;
    }

    public void ResetVariables(){
        pos.y = bottom;
        transform.localPosition = pos;
        isCovering = toCover = toUncover = false;
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
