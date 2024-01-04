using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public AudioMixer soundMixer;

    public Slider musicSlider;
    public Slider soundSlider;

    private void Start() {
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("MusicVolume", 1.0f));
        soundMixer.SetFloat("volumeSounds", PlayerPrefs.GetFloat("SoundVolume", 1.0f));

        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 1.0f);

        Screen.fullScreen = true;
        
    }

    public void SetVolume(float value) {
        //Debug.Log(value);
        //audioMixer.SetFloat("volume", value);
        audioMixer.SetFloat("volume", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SetVolumeSounds(float value) {
        //Debug.Log(value);
        //audioMixer.SetFloat("volume", value);
        soundMixer.SetFloat("volumeSounds", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("SoundVolume", value);
    }

    public void SetFullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }

}
