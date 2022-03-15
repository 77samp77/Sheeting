using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StageDataManager : MonoBehaviour
{   
    bool alreadyRead;
    public TextAsset baseCSV, initCSV;

    string[] baseLabel;
    List<string[]> baseData = new List<string[]>();
    int bd_colLength;
    [System.NonSerialized] public string[] base_quotaType;
    [System.NonSerialized] public int[] base_quotaNum, base_limit, base_life;
    [System.NonSerialized] public float[] base_speed;

    void Awake(){
        GameObject temp = GameObject.Find("StageData");
        if(temp != null) Destroy(this.gameObject);
        else{
            DontDestroyOnLoad(this.gameObject);
            this.name = "StageData";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!alreadyRead) ReadStageDataCSV();
    }

    void ReadStageDataCSV(){
        StringReader baseReader = new StringReader(baseCSV.text);
        while(baseReader.Peek() != -1){
            string line = baseReader.ReadLine();
            baseData.Add(line.Split(','));
        }
        bd_colLength = baseData[0].GetLength(0);
        baseLabel = new string[bd_colLength];
        for(int col = 0; col < bd_colLength; col++) baseLabel[col] = baseData[0][col];

        base_quotaType = new string[baseData.Count];
        base_quotaNum = new int[baseData.Count];
        base_limit = new int[baseData.Count];
        base_speed = new float[baseData.Count];
        base_life = new int[baseData.Count];
        for(int row = 1; row < baseData.Count; row++){
            base_quotaType[row] = baseData[row][1];
            base_quotaNum[row] = int.Parse(baseData[row][2]);
            base_limit[row] = int.Parse(baseData[row][3]);
            base_speed[row] = float.Parse(baseData[row][4]);
            base_life[row] = int.Parse(baseData[row][5]);
        }


        alreadyRead = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
