using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [System.NonSerialized] public GameObject mainCamera;
    Camera mcc;
    static int screenWidth, screenHeight;
    float baseAspect = (float)180 / (float)270;

    void Awake(){
        GameObject temp = GameObject.Find("ScreenManager");
        if(temp != null) Destroy(this.gameObject);
        else{
            DontDestroyOnLoad(this.gameObject);
            this.name = "ScreenManager";
            screenWidth = Screen.width;
            screenHeight = Screen.height;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(screenWidth != Screen.width || screenHeight != Screen.height){
            float currentAspect = (float)Screen.height / (float)Screen.width;
            // if(!Mathf.Approximately(baseAspect, currentAspect)){
            if(Mathf.Abs(baseAspect - currentAspect) > 0.01f){
                Debug.Log("base:" + baseAspect + ", curr:" + currentAspect);
                mcc.orthographicSize = (int)(mcc.orthographicSize * baseAspect / currentAspect);
                Debug.Log("Size:" + mcc.orthographicSize);
                currentAspect = 0;
            }
            else{
                mcc.orthographicSize = 90;
                Debug.Log("Size:" + mcc.orthographicSize);
            }
            screenWidth = Screen.width;
            screenHeight = Screen.height;
        }
    }

    public void SetMainCamera(){
        mainCamera = GameObject.Find("Main Camera");
        mcc = mainCamera.GetComponent<Camera>();
    }
}
