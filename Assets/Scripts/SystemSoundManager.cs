using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemSoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip SE_decide, SE_choose, SE_back, SE_pause_open, SE_pause_close;

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

    [SerializeField, Range(0f, 1f)] public float volume;
    public void PlaySE(AudioClip sound){
        float part_volume = 1;
        if(sound == SE_back) part_volume = 0.5f;
        else if(sound == SE_choose) part_volume = 0.4f;
        else if(sound == SE_pause_open || sound == SE_pause_close) part_volume = 0.6f;
        audioSource.PlayOneShot(sound, volume * part_volume);
    }
}
