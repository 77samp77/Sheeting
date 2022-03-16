using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WordGenerator : MonoBehaviour
{
    public GameObject gameManager;
    GameManagerScript gms;

    public TextAsset wordsCSV;
    List<string[]> wordsData = new List<string[]>();
    int[] wordLen, wordNo;
    int wordLen_max, wordNo_max;
    string[,] wordStr;

    public GameObject wordHorPrefab, wordVerPrefab, words;
    public int gen_interval;  // 生成間隔(フレーム数)
    int frame_gen;  // 生成時のフレーム(ランダム生成)

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
        ReadWordsCSV();
    }

    void InitVariables(){
        gms = gameManager.GetComponent<GameManagerScript>();
    }

    void ReadWordsCSV(){
        StringReader reader = new StringReader(wordsCSV.text);
        while (reader.Peek() != -1){
            string line = reader.ReadLine();
            wordsData.Add(line.Split(','));
        }

        wordLen = new int[wordsData.Count];
        wordNo = new int[wordsData.Count];
        for(int i = 0; i < wordsData.Count; i++){
            wordLen[i] = int.Parse(wordsData[i][1]);
            wordNo[i] = int.Parse(wordsData[i][2]);
            if(wordLen[i] > wordLen_max) wordLen_max = wordLen[i];
            if(wordNo[i] > wordNo_max) wordNo_max = wordNo[i];
        }
        wordStr = new string[wordLen_max + 1, wordNo_max + 1];
        for(int i = 0; i < wordsData.Count; i++) wordStr[wordLen[i], wordNo[i]] = wordsData[i][3];
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
        GameObject wordPrefab = new GameObject();
        if(type == "Hor") wordPrefab = wordHorPrefab;
        else if(type == "Ver") wordPrefab = wordVerPrefab;
        GenerateWord(wordPrefab, pos, speed, length, isTop);
    }

    void GenerateWord(GameObject wordPrefab, int pos, float speed, int length, bool isTop){
        GameObject word = Instantiate(wordPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        word.name = wordPrefab.name;
        word.transform.SetParent(words.transform);
        SetWord(word, pos, speed, length, Random.Range(0, wordNo_max + 1), isTop);
    }

    void SetWord(GameObject word, int pos, float speed, int len, int no, bool isTop){
        WordController wcs = word.GetComponent<WordController>();
        wcs.BeSetWord(pos, speed, wordStr[len, no], isTop);
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
        if(word.name == "Word_Hor") SetWord(word, Random.Range(3, 8), Random.Range(0, wordNo_max + 1));
        else SetWord(word, Random.Range(8, wordLen_max + 1), Random.Range(0, wordNo_max + 1));
    }
    
    void SetWord(GameObject word, int len, int no){
        WordController wcs = word.GetComponent<WordController>();
        wcs.BeSetWord(wordStr[len, no]);
    }

    // ----------------------------------------------------------
}
