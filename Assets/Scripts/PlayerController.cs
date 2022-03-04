using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Sprite spr;
    public GameObject playerShotPrefab;

    Vector3 pos;
    Vector2 spr_scl;

    public float v = 1.0f;

    void Start()
    {
        InitVariables();
        Debug.Log(Screen.width);
    }

    void InitVariables(){
        spr = GetComponent<SpriteRenderer>().sprite;
        spr_scl = spr.bounds.size;
    }

    void Update()
    {
        pos = transform.localPosition;
        Move();
        if(Input.GetKey(KeyCode.Space)) Shoot();
    }

    void Move(){
        if(Input.GetKey(KeyCode.W)) pos.y += v;
        if(Input.GetKey(KeyCode.A)) pos.x -= v;
        if(Input.GetKey(KeyCode.S)) pos.y -= v;
        if(Input.GetKey(KeyCode.D)) pos.x += v;
        transform.localPosition = pos;
    }

    void Shoot(){
        GameObject pShot = Instantiate(playerShotPrefab, 
                                       new Vector3(pos.x + spr_scl.x / 2, pos.y - spr_scl.y / 5, 0), 
                                       Quaternion.identity);
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
