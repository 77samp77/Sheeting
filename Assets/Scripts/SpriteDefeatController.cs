using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDefeatController : MonoBehaviour
{
    GameObject gameSound;
    GameSoundManager gsms;

    GameObject defeatObject;
    bool objectIsSet;
    public Sprite[] defeat_sprites = new Sprite[3];
    SpriteRenderer sprite_d_ren;
    int defeat_progress = 0;

    void Awake(){
        InitVariables();
    }

    // Start is called before the first frame update
    void Start()
    {
        gsms.PlaySE(gsms.SE_defeat);
    }

    void InitVariables(){
        gameSound = GameObject.Find("GameSound");
        gsms = gameSound.GetComponent<GameSoundManager>();
        sprite_d_ren = GetComponent<SpriteRenderer>();
    }

    public void SetObject(GameObject dObject){
        defeatObject = dObject;
        Vector2 pos = new Vector2(0, 0);
        if(dObject.name == "Player") pos = new Vector2(Random.Range(4, 30), Random.Range(-7, -2));
        else if(dObject.name == "EnemyA") pos = new Vector2(18, -4);
        else if(dObject.name == "EnemyB") pos = new Vector2(0, 0);
        else if(dObject.name == "EnemyC") pos = new Vector2(4, -4);
        sprite_d_ren.sortingLayerName = dObject.tag;
        transform.localPosition = pos;
        objectIsSet = true;
    }

    public int anim_f;
    // Update is called once per frame
    void Update()
    {
        this.gameObject.SetActive(true);
        if(objectIsSet){
            if(defeat_progress == anim_f * 4) sprite_d_ren.sprite = defeat_sprites[0];
            else if(defeat_progress == anim_f || defeat_progress == anim_f * 3) sprite_d_ren.sprite = defeat_sprites[1];
            else if(defeat_progress == anim_f * 2) sprite_d_ren.sprite = defeat_sprites[2];
            if(defeat_progress == anim_f * 5){
                Destroy(this.gameObject);
                if(defeatObject.tag == "Enemy") Destroy(defeatObject);
            }
            defeat_progress++;
        }
    }
}
