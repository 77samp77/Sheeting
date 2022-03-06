using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WordGenerator : MonoBehaviour
{
    public TextAsset wordsCSV;
    List<string[]> wordsData = new List<string[]>();
    int[] wordLen, wordNo;
    int wordLen_max, wordNo_max;
    string[,] wordStr;

    public GameObject wordPrefab, words;
    public int gen_interval;  // 生成間隔(フレーム数)
    int frame_gen;  // 生成時のフレーム

    public GameObject sheet;
    SheetController scs;

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
        ReadWordsCSV();
    }

    void InitVariables(){
        scs = sheet.GetComponent<SheetController>();
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
        if(Time.frameCount - frame_gen > gen_interval) Generate();
    }

    void Generate(){
        GameObject word = Instantiate(wordPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        word.name = wordPrefab.name;
        word.transform.SetParent(words.transform);
        frame_gen = Time.frameCount;
        SetWord(word, Random.Range(3, wordLen_max + 1), Random.Range(0, wordNo_max + 1));
    }

    void SetWord(GameObject word, int len, int no){
        WordController wcs = word.GetComponent<WordController>();
        wcs.BeSetWord(wordStr[len, no]);
    }
}
