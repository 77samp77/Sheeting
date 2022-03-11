using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDefeatController : MonoBehaviour
{
    GameObject defeatObject;
    bool objectIsSet;

    GameObject gameManager;
    GameManagerScript gms;
    SpriteRenderer sprite_d_ren;
    int defeat_progress = 0;

    void Awake(){
        InitVariables();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void InitVariables(){
        gameManager = GameObject.Find("GameManager");
        gms = gameManager.GetComponent<GameManagerScript>();
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

    // Update is called once per frame
    void Update()
    {
        this.gameObject.SetActive(true);
        if(objectIsSet){
            if(defeat_progress == 16) sprite_d_ren.sprite = gms.defeat_sprites[0];
            else if(defeat_progress == 4 || defeat_progress == 12) sprite_d_ren.sprite = gms.defeat_sprites[1];
            else if(defeat_progress == 8) sprite_d_ren.sprite = gms.defeat_sprites[2];
            if(defeat_progress == 20){
                Destroy(this.gameObject);
                if(defeatObject.tag == "Enemy") Destroy(defeatObject);
            }
            defeat_progress++;
        }
    }
}
