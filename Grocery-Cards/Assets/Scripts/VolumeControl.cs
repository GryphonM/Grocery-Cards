using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] AudioMixerGroup control;
    [SerializeField] float maxVolume = 0;
    [SerializeField] float minVolume = -80;
    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        control.audioMixer.SetFloat("Volume", Mathf.Lerp(minVolume, maxVolume, slider.value));
        float volume;
        control.audioMixer.GetFloat("Volume", out volume);
        Debug.Log(volume);
        if (slider.value == 0)
            control.audioMixer.SetFloat("Volume", -80);
    }
}
