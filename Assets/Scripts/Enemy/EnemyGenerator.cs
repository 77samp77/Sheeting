using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject gameManager;
    GameManagerScript gms;

    public GameObject enemyAPrefab, enemyBPrefab, enemies;
    public int gen_interval;  // 生成間隔(フレーム数)
    int frame_gen;  // 生成時のフレーム

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
    }

    void InitVariables(){
        gms = gameManager.GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gms.gameIsStop) return;
        if(Time.frameCount - frame_gen > gen_interval) Generate(enemyBPrefab);
    }

    void Generate(GameObject enemyPrefab){
        GameObject enemy = Instantiate(enemyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        enemy.name = enemyPrefab.name;
        enemy.transform.SetParent(enemies.transform);
        frame_gen = Time.frameCount;
    }
}
