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
        audioSource.volume = volume;
        audioSource.clip = bgm;
        audioSource.Play(0);
    }

    public void PauseBGM(){
        audioSource.Pause();
    }

    public void UnpauseBGM(){
        audioSource.UnPause();
    }

    public void StopBGM(){
        audioSource.Stop();
    }
}
