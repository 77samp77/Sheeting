using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject gameManager;
    GameManagerScript gms;

    public GameObject sheet;
    SheetController scs;

    public GameObject enemyAPrefab, enemyBPrefab, enemyCPrefab, enemies;
    public int gen_interval;  // 生成間隔(フレーム数)
    int frame_gen;  // 生成時のフレーム

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
    }

    void InitVariables(){
        gms = gameManager.GetComponent<GameManagerScript>();
        scs = sheet.GetComponent<SheetController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gms.gameIsStop) return;
        if(gms.progress - frame_gen > gen_interval){
            float temp = Random.value;
            Debug.Log(temp);
            if(temp < 0.3f) Generate(enemyAPrefab);
            else if(temp < 0.6f) Generate(enemyBPrefab);
            else Generate(enemyCPrefab);
        }
    }

    void Generate(GameObject enemyPrefab){
        int e_pos_x = 0, e_pos_y = 0;
        if(enemyPrefab.name == "EnemyC"){
            e_pos_x = Screen.width / 2 + 2;
            e_pos_y = Random.Range(scs.bottom + 11, Screen.height / 2 - 2);
        }
        GameObject enemy = Instantiate(enemyPrefab, new Vector3(e_pos_x, e_pos_y, 0), Quaternion.identity);
        enemy.name = enemyPrefab.name;
        enemy.transform.SetParent(enemies.transform);
        frame_gen = gms.progress;

        if(enemy.name == "EnemyB"){
            EnemyBController ebcs = enemy.GetComponent<EnemyBController>();
            if(Random.value < 0.5f) ebcs.Flip();
            else ebcs.toSetFirstPosition = true;
        }
    }

    public void ResetVariables(){
        frame_gen = 0;
    }
}