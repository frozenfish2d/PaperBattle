using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider sfxSlider;
    
    MusicPlayer musicManager;
    LevelManager levelManager;

    private void Start()
    {
        musicManager = FindObjectOfType<MusicPlayer>();
        levelManager = FindObjectOfType<LevelManager>();
        volumeSlider.value = DataManager.GetMasterVolume();
        sfxSlider.value = DataManager.GetSfxVolume();
    }

    private void Update()
    {
        musicManager.SetVolume(volumeSlider.value);

    }

    public void SaveAndExit()
    {
        DataManager.SetMasterVolume(volumeSlider.value);
        DataManager.SetSfxVolume(sfxSlider.value);
        levelManager.LoadStartMenu();
    }

}
