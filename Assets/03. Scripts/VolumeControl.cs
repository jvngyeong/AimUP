using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("masterVol", 0.5f);
    }
    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("masterVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("masterVol", sliderValue);
    }

}