using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Sprite spr;
    public GameObject playerShotPrefab, playerShots;
    public GameObject sheet;
    SheetController scs;

    Vector3 pos;
    Vector2 spr_scl;

    public float v = 1.0f;
    public int shoot_interval = 12; // 連続で弾を撃つときの間隔(フレーム数) 
    int frame_shot; // 弾を撃ったときのフレーム

    void Start()
    {
        InitVariables();
    }

    void InitVariables(){
        spr = GetComponent<SpriteRenderer>().sprite;
        spr_scl = spr.bounds.size;
        scs = sheet.GetComponent<SheetController>();
    }

    void Update()
    {
        pos = transform.localPosition;
        Move();
        if(Input.GetKey(KeyCode.Space)){
            if(Time.frameCount - frame_shot > shoot_interval && pos.y > scs.pos.y) Shoot();
        }
    }

    void Move(){
        if(Input.GetKey(KeyCode.W)){
            if(pos.y >= Screen.height / 2) pos.y = Screen.height / 2;
            else pos.y += v;
        }
        if(Input.GetKey(KeyCode.A)){
            if(pos.x <= -Screen.width / 2) pos.x = -Screen.width / 2;
            else pos.x -= v;
        }
        if(Input.GetKey(KeyCode.S)){
            if(pos.y - spr_scl.y <= scs.bottom) pos.y = scs.bottom + spr_scl.y;
            else pos.y -= v;
        }
        if(Input.GetKey(KeyCode.D)){
            if(pos.x + spr_scl.x >= Screen.width / 2) pos.x = Screen.width / 2 - spr_scl.x;
            else pos.x += v;
        }
        transform.localPosition = pos;
    }

    void Shoot(){
        GameObject pShot = Instantiate(playerShotPrefab, 
                                       new Vector3(pos.x + spr_scl.x * 0.8f, pos.y - spr_scl.y / 2, 0), 
                                       Quaternion.identity);
        pShot.name = playerShotPrefab.name;
        pShot.transform.SetParent(playerShots.transform);

        frame_shot = Time.frameCount;
    }

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
