using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotController : MonoBehaviour
{
    [System.NonSerialized] public GameObject gameManager;
    [System.NonSerialized] public GameManagerScript gms;

    [System.NonSerialized] public GameObject sheet;
    [System.NonSerialized] public SheetController scs;
    public GameObject sprite;

    public float v;
    [System.NonSerialized] public Vector3 pos;

    // Start is called before the first frame update
    public virtual void Start()
    {
        InitVariables();
    }

    public virtual void InitVariables(){
        gameManager = GameObject.Find("GameManager");
        gms = gameManager.GetComponent<GameManagerScript>();
        sheet = GameObject.Find("Sheet");
        scs = sheet.GetComponent<SheetController>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(gms.gameIsStop) return;
        pos = transform.localPosition;
        Move();
        IsCoveredSwitch();
        if(readyToDestroy()) Destroy(this.gameObject);
    }

    public virtual void Move(){
    }

    public virtual void IsCoveredSwitch(){
        if(sprite.activeSelf){
            if(pos.y < scs.pos.y) sprite.SetActive(false);
        }
        else if(pos.y > scs.pos.y) sprite.SetActive(true);
    }

    public virtual bool readyToDestroy(){
        return false;
    }

    public virtual void OnTriggerStay2D(Collider2D collider){
        if(!sprite.activeSelf) return;
        GameObject colObject = collider.gameObject;
        if(colObject.tag == "Player"){
            PlayerController pcs = colObject.GetComponent<PlayerController>();
            pcs.Damage();
            Destroy(this.gameObject);
        }
    }

    /*=描画用===================================================================*/
    [System.NonSerialized] public Vector3 cashPos;
    public virtual void LateUpdate(){
        cashPos = transform.localPosition;
        transform.localPosition = new Vector3(Mathf.RoundToInt(cashPos.x),
                                              Mathf.RoundToInt(cashPos.y),
                                              Mathf.RoundToInt(cashPos.z));
    }

    public virtual void OnRenderObject(){
        transform.localPosition = cashPos;
    }
}
