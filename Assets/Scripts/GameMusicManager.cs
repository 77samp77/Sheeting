using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bgm;
    [SerializeField, Range(0f, 1f)] public float volume;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBGM(AudioClip music){
        float part_volume = 0.1f;
        audioSource.PlayOneShot(music, volume * part_volume);
    }

    public void PauseBGM(){
        audioSource.Pause();
    }

    public void UnpauseBGM(){
        audioSource.UnPause();
    }
}
