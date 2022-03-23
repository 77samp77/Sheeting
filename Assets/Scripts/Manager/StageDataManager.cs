using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class StageDataManager : MonoBehaviour
{   
    bool alreadyRead;
    public TextAsset baseCSV, initCSV;

    public List<string[]> baseData = new List<string[]>();
    int bd_colLength;
    [System.NonSerialized] public string[] base_quotaType, base_speed;
    [System.NonSerialized] public int[] base_quotaNum, base_limit, base_life, base_status, base_hiscore;

    List<string[]> initData = new List<string[]>();
    int id_colLength;
    [System.NonSerialized] public int[] init_level, init_progress, init_pos, init_length;
    [System.NonSerialized] public string[] init_tag, init_type;
    [System.NonSerialized] public float[] init_speed;
    [System.NonSerialized] public bool[] init_isTop;

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
        ReadBaseCSV();
        ReadInitCSV();
        alreadyRead = true;
    }

    void ReadBaseCSV(){
        StringReader baseReader = new StringReader(baseCSV.text);
        while(baseReader.Peek() != -1){
            string line = baseReader.ReadLine();
            baseData.Add(line.Split(','));
        }
        bd_colLength = baseData[0].GetLength(0);

        base_quotaType = new string[baseData.Count];
        base_quotaNum = new int[baseData.Count];
        base_limit = new int[baseData.Count];
        base_speed = new string[baseData.Count];
        base_life = new int[baseData.Count];
        base_status = new int[baseData.Count];
        base_hiscore = new int[baseData.Count];

        for(int row = 1; row < baseData.Count; row++){
            base_quotaType[row] = baseData[row][1];
            base_quotaNum[row] = int.Parse(baseData[row][2]);
            base_limit[row] = int.Parse(baseData[row][3]);
            base_speed[row] = baseData[row][4];
            base_life[row] = int.Parse(baseData[row][5]);
            base_status[row] = int.Parse(baseData[row][6]);
            base_hiscore[row] = int.Parse(baseData[row][7]);
        }
    }

    void ReadInitCSV(){
        StringReader initReader = new StringReader(initCSV.text);
        while(initReader.Peek() != -1){
            string line = initReader.ReadLine();
            initData.Add(line.Split(','));
        }
        id_colLength = initData[0].GetLength(0);

        init_level = new int[initData.Count];
        init_progress = new int[initData.Count];
        init_tag = new string[initData.Count];
        init_type = new string[initData.Count];
        init_pos = new int[initData.Count];
        init_speed = new float[initData.Count];
        init_length = new int[initData.Count];
        init_isTop = new bool[initData.Count];
        
        for(int row = 1; row < initData.Count; row++){
            if(initData[row][0] == "") continue;
            init_level[row] = int.Parse(initData[row][0]);
            init_progress[row] = int.Parse(initData[row][1]);
            init_tag[row] = initData[row][2];
            if(initData[row][3] != "") init_type[row] = initData[row][3];
            if(initData[row][4] != "") init_pos[row] = int.Parse(initData[row][4]);
            if(initData[row][5] != "") init_speed[row] = float.Parse(initData[row][5]);
            if(initData[row][6] != "") init_length[row] = int.Parse(initData[row][6]);
            if(initData[row][7] != "") init_isTop[row] = Convert.ToBoolean(initData[row][7]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // string path_base = "Assets/CSV/StageData_Base.csv";
    // string path_base = Application.streamingAssetsPath + "/StageData_Base.csv";
    // bool isAppend = false;
    // public void WriteBaseCSV(){
        // string newData = "Level,QuotaType,QuotaNum,Limit,Speed,Life,Status,Hiscore";
        // for(int row = 1; row < baseData.Count; row++){
        //     newData += "\n" + string.Join(",", baseData[row]);
        // }
        // using(var fs = new StreamWriter(path_base, isAppend, System.Text.Encoding.GetEncoding("UTF-8"))){
        //     fs.WriteLine(newData);
        // }
    // }
}
