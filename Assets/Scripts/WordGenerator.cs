using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{
    public GameObject wordPrefab, words;
    public int gen_interval;  // 生成間隔(フレーム数)
    int frame_gen;  // 生成時のフレーム

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.frameCount - frame_gen > gen_interval) Generate();
    }

    void Generate(){
        int gen_y = Random.Range(-Screen.height / 2, Screen.height / 2);
        GameObject word = Instantiate(wordPrefab,
                                      new Vector3(Screen.width / 2, gen_y, 0),
                                      Quaternion.identity);
        word.name = wordPrefab.name;
        word.transform.SetParent(words.transform);
        frame_gen = Time.frameCount;
    }
}
