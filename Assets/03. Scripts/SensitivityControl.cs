using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SensitivityControl : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("Sensitivity", 0.5f);
    }
    public void SetLevel(float sliderValue)
    {
        PlayerPrefs.SetFloat("Sensitivity", sliderValue);
    }

}