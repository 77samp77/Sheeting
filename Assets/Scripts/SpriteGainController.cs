using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteGainController : MonoBehaviour
{
    GameObject gainObject;
    bool objectIsSet;
    public Sprite[] gain_sprites = new Sprite[3];
    SpriteRenderer sprite_g_ren;
    int gain_progress = 0;

    void Awake(){
        InitVariables();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void InitVariables(){
        sprite_g_ren = GetComponent<SpriteRenderer>();
    }

    public void SetObject(GameObject gObject){
        gainObject = gObject;
        Vector2 pos = new Vector2(0, 0);
        if(gObject.name == "Word_Ver") pos = new Vector2(-46.5f, -16);
        else if(gObject.name == "Word_Hor") pos = new Vector2(6, -16);
        transform.localPosition = pos;
        objectIsSet = true;
    }

    public int anim_f;
    // Update is called once per frame
    void Update()
    {
        this.gameObject.SetActive(true);
        if(objectIsSet){
            if(gain_progress == anim_f * 4) sprite_g_ren.sprite = gain_sprites[0];
            else if(gain_progress == anim_f || gain_progress == anim_f * 3) sprite_g_ren.sprite = gain_sprites[1];
            else if(gain_progress == anim_f * 2) sprite_g_ren.sprite = gain_sprites[2];
            if(gain_progress == anim_f * 5){
                Destroy(this.gameObject);
                Destroy(gainObject);
            }
            gain_progress++;
        }
    }
}
