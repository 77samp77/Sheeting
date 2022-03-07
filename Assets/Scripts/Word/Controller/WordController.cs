using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordController : MonoBehaviour
{
    [System.NonSerialized] public GameObject gameManager;
    [System.NonSerialized] public GameManagerScript gms;
    [System.NonSerialized] public bool readyToSetPos;
    
    [System.NonSerialized] public GameObject UIManager;
    [System.NonSerialized] public UIManager UIms;

    public Rigidbody2D rb2d;
    public GameObject canvas, textObject, mark, mark_c;
    public float v;
    [System.NonSerialized] public Vector3 pos;
    [System.NonSerialized] public int colWidth;
    public bool isMarked, isCovered;

    [System.NonSerialized] public GameObject sheet;
    [System.NonSerialized] public SheetController scs;

    // Start is called before the first frame update
    public virtual void Start()
    {
        textObject.SetActive(false);
        InitVariables();
        canvas.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public virtual void SetFirstPosition(){
    }
    
    public virtual void InitVariables(){
        gameManager = GameObject.Find("GameManager");
        gms = gameManager.GetComponent<GameManagerScript>();
        UIManager = GameObject.Find("UIManager");
        UIms = UIManager.GetComponent<UIManager>();
        sheet = GameObject.Find("Sheet");
        scs = sheet.GetComponent<SheetController>();
    }

    public virtual void BeSetWord(string wordStr){
        textObject.GetComponent<Text>().text = wordStr;
        colWidth = 5 * wordStr.Length;
        mark.GetComponent<RectTransform>().sizeDelta = new Vector2(colWidth, 7);
        mark_c.GetComponent<RectTransform>().sizeDelta = new Vector2(colWidth, 7);
        readyToSetPos = true;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(readyToSetPos){
            SetFirstPosition();
            textObject.SetActive(true);
        }

        pos = transform.localPosition;
        Move();
        if(readyToCover()) BeCovered();
        
        if(readyToGain()) BeGained();
        if(readyToDestroy()) Destroy(this.gameObject);
    }

    public virtual bool readyToCover(){
        if(!isMarked) return false;
        if(!scs.isCovering) return false;
        if(!scs.toCover) return true;
        if(pos.y >= scs.pos.y) return false;
        return true;
    }

    public virtual bool readyToGain(){
        if(pos.y >= scs.bottom) return false;
        if(!isCovered) return false;
        return true;
    }

    public virtual bool readyToDestroy(){
        return false;
    }

    public virtual void Move(){
        pos.x += v;
        transform.localPosition = pos;
    }

    public virtual void BeCovered(){
        isCovered = true;
        mark_c.SetActive(true);
        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }

    public virtual void BeGained(){
        gms.gain_combo++;
        gms.gain_words++;
        UIms.SetWordCountUI(gms.gain_words, gms.quota_words);
        gms.IncreaseScore(Mathf.FloorToInt(100 * Mathf.Pow(2, gms.gain_combo - 1)));
        Destroy(this.gameObject);
    }

    /*=描画用===================================================================*/
    [System.NonSerialized] public Vector3 cashPos;
    public virtual void LateUpdate(){
        cashPos = transform.localPosition;
        transform.localPosition = new Vector3(Mathf.RoundToInt(cashPos.x),
                                              Mathf.RoundToInt(cashPos.y),
                                              Mathf.RoundToInt(cashPos.z));
        pos = transform.localPosition;
    }

    public virtual void OnRenderObject(){
        transform.localPosition = cashPos;
    }
}
