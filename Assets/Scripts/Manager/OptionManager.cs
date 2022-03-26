using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public GameObject optionUI;
    GameObject systemSound;
    SystemSoundManager ssms;
    public Slider BGMSlider, SESlider, systemSlider;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void OpenOptionUI(){
        if(systemSound == null){
            systemSound = GameObject.Find("SystemSound");
            ssms = systemSound.GetComponent<SystemSoundManager>();
        }

        BGMSlider.value = StaticManager.volume_BGM;
        SESlider.value = StaticManager.volume_SE;

        optionUI.SetActive(true);
    }

    bool prev_optionUI;
    // Update is called once per frame
    void Update()
    {
        if(prev_optionUI && Input.GetKeyDown(KeyCode.Space)) CloseOptionUI();
        prev_optionUI = optionUI.activeSelf;
    }

    void CloseOptionUI(){
        StaticManager.volume_BGM = BGMSlider.value;
        StaticManager.volume_SE = SESlider.value;
        ssms.volume = systemSlider.value;
        ssms.PlaySE(ssms.SE_back);
        optionUI.SetActive(false);
    }
}
