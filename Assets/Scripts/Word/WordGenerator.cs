using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WordGenerator : MonoBehaviour
{
    public GameObject gameManager;
    GameManagerScript gms;

    GameObject wordData;
    WordDataManager wdms;

    public GameObject wordHorPrefab, wordVerPrefab, words;
    public int gen_interval;  // 生成間隔(フレーム数)
    int frame_gen;  // 生成時のフレーム(ランダム生成)

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
    }

    void InitVariables(){
        gms = gameManager.GetComponent<GameManagerScript>();
        wordData = GameObject.Find("WordData");
        wdms = wordData.GetComponent<WordDataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // ----------ランダム生成-----------------------------
        // if(gms.gameIsStop) return;
        // if(gms.progress - frame_gen > gen_interval){
        //     if(Random.value < 0.5f) Generate(wordHorPrefab);
        //     else Generate(wordVerPrefab);
        // }
        // ---------------------------------------------------
    }

    public void Generate(string type, int pos, float speed, int length, bool isTop){
        GameObject wordPrefab = wordHorPrefab;
        if(type == "Ver") wordPrefab = wordVerPrefab;
        GenerateWord(wordPrefab, pos, speed, length, isTop);
    }

    void GenerateWord(GameObject wordPrefab, int pos, float speed, int length, bool isTop){
        GameObject word = Instantiate(wordPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        word.name = wordPrefab.name;
        word.transform.SetParent(words.transform);
        SetWord(word, pos, speed, length, Random.Range(0, wdms.wordNo_max + 1), isTop);
    }

    void SetWord(GameObject word, int pos, float speed, int len, int no, bool isTop){
        WordController wcs = word.GetComponent<WordController>();
        wcs.BeSetWord(pos, speed, wdms.wordStr[len, no], isTop);
    }

    public void ResetVariables(){
        frame_gen = 0;
    }



    // ----------------ランダム生成------------------------------

    void Generate(GameObject wordPrefab){
        GameObject word = Instantiate(wordPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        word.name = wordPrefab.name;
        word.transform.SetParent(words.transform);
        frame_gen = gms.progress;
        if(word.name == "Word_Hor") SetWord(word, Random.Range(3, 8), Random.Range(0, wdms.wordNo_max + 1));
        else SetWord(word, Random.Range(8, wdms.wordLen_max + 1), Random.Range(0, wdms.wordNo_max + 1));
    }
    
    void SetWord(GameObject word, int len, int no){
        WordController wcs = word.GetComponent<WordController>();
        wcs.BeSetWord(wdms.wordStr[len, no]);
    }

    // ----------------------------------------------------------
}
