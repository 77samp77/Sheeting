using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WordDataManager : MonoBehaviour
{
    public TextAsset wordsCSV;
    List<string[]> wordsData = new List<string[]>();
    int[] wordLen, wordNo;
    [System.NonSerialized] public int wordLen_max, wordNo_max;
    [System.NonSerialized] public string[,] wordStr;

    void Awake(){
        GameObject temp = GameObject.Find("WordData");
        if(temp != null) Destroy(this.gameObject);
        else{
            DontDestroyOnLoad(this.gameObject);
            this.name = "WordData";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ReadWordsCSV();
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
        
    }
}
