using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [System.NonSerialized] public GameObject sheet;
    [System.NonSerialized] public SheetController scs;

    [System.NonSerialized] public Vector3 pos;

    public float v;

    // Start is called before the first frame update
    public virtual void Start()
    {
        InitVariables();
    }

    public virtual void InitVariables(){
        sheet = GameObject.Find("Sheet");
        scs = sheet.GetComponent<SheetController>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        pos = transform.localPosition;
        Move();
        Shoot();
    }

    public virtual void Move(){

    }

    public virtual void Shoot(){

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
