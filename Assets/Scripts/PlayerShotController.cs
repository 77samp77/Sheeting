using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotController : MonoBehaviour
{
    public float vx = 2f;
    Vector3 pos;

    void Start()
    {
        
    }

    void Update()
    {
        pos = transform.localPosition;
        Move();
        if(pos.x > Screen.width / 2) Destroy(this.gameObject);
    }

    void Move(){
        pos.x += vx;
        transform.localPosition = pos;
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
