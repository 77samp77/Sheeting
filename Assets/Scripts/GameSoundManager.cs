using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip SE_shot_player, SE_shot_enemyA, SE_shot_enemyB, SE_defeat,
                     SE_mark, SE_sheet, SE_gain, SE_damage;

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
        float part_volume = 0.3f;
        if(sound == SE_shot_enemyA) part_volume = 0.2f;
        else if(sound == SE_defeat) part_volume = 0.15f;
        audioSource.PlayOneShot(sound, volume * part_volume);
    }
}
