using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [System.NonSerialized] public GameObject sheet;
    [System.NonSerialized] public SheetController scs;

    [System.NonSerialized] public Vector3 pos;

    // [System.NonSerialized] public bool isCovered;
    public GameObject sprite, sprite_f;

    public float v;
    public int score;

    [System.NonSerialized] public GameObject enemyShotPrefab, enemyShots;
    public int shoot_interval; // 連続で弾を撃つときの間隔(フレーム数) 
    [System.NonSerialized] public int frame_shot; // 弾を撃ったときのフレーム

    // Start is called before the first frame update
    public virtual void Start()
    {
        InitVariables();
    }

    public virtual void InitVariables(){
        sheet = GameObject.Find("Sheet");
        scs = sheet.GetComponent<SheetController>();
        enemyShots = GameObject.Find("EnemyShots");
    }

    // Update is called once per frame
    public virtual void Update()
    {
        pos = transform.localPosition;
        Move();
        if(readyToShoot()) Shoot();
        IsCoveredSwitch();
    }

    public virtual void IsCoveredSwitch(){
        if(sprite.activeSelf){
            if(pos.y < scs.pos.y){
                sprite.SetActive(false);
                sprite_f.SetActive(true);
            }
        }
        else if(pos.y > scs.pos.y){
            sprite.SetActive(true);
            sprite_f.SetActive(false);
        }
    }

    public virtual void Move(){

    }

    public virtual bool readyToShoot(){
        return false;
    }

    public virtual void Shoot(){
        GameObject eShot = Instantiate(enemyShotPrefab, 
                                       new Vector3(pos.x, pos.y, 0), 
                                       Quaternion.identity);
        PositionShot(eShot);
        eShot.name = enemyShotPrefab.name;
        eShot.transform.SetParent(enemyShots.transform);
        frame_shot = Time.frameCount;
    }

    public virtual void PositionShot(GameObject eShot){
        
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
