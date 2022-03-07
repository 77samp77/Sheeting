using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotController : MonoBehaviour
{
    public float vx = 2f;
    Vector3 pos;

    List<GameObject> colliders = new List<GameObject>();

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

    void OnTriggerEnter2D(Collider2D collider){
        GameObject word = collider.gameObject;
        if(word.tag == "Word"){
            WordController wcs;
            if(word.name == "Word_Hor") wcs = word.GetComponent<WordHorController>();
            else wcs = word.GetComponent<WordVerController>();
            if(wcs.textObject.activeSelf) Mark(word, wcs);
        }
    }

    void Mark(GameObject word, WordController wcs){
        wcs.isMarked = true;
        wcs.mark.SetActive(true);
        Destroy(this.gameObject);
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
