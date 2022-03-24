using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_Title : MonoBehaviour
{
    public GameObject[] choosingUI_title;
    public GameObject[] choosingUI_account;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetChoosingUI(string name, int prev, int now){
        GameObject[] choosingUI = choosingUI_title;
        if(name == "account") choosingUI = choosingUI_account;
        choosingUI[prev].SetActive(false);
        choosingUI[now].SetActive(true);
    }
}
