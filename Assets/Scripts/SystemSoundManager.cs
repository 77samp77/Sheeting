using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemSoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip SE_decide, SE_choose, SE_back;

    void Awake(){
        GameObject temp = GameObject.Find("SystemSound");
        if(temp != null) Destroy(this.gameObject);
        else{
            DontDestroyOnLoad(this.gameObject);
            this.name = "SystemSound";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySE(AudioClip sound){
        if(sound == SE_back) audioSource.PlayOneShot(sound, 0.5f);
        else if(sound == SE_choose) audioSource.PlayOneShot(sound, 0.4f);
        else audioSource.PlayOneShot(sound);
    }
}
