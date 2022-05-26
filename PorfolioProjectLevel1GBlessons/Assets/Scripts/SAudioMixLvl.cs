using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SAudioMixLvl : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider VolumeSlider;
    public Slider SfxSlider;
    public Slider MusicSlider;
    float volumeValue, sfxValue, musicValue;
    private void Update()
    {
        masterMixer.GetFloat("VolumeLvl",out volumeValue);
        masterMixer.GetFloat("MusicLvl", out musicValue);
        masterMixer.GetFloat("SfxLvl", out sfxValue);

        VolumeSlider.value = volumeValue;
        MusicSlider.value = musicValue;
        SfxSlider.value = sfxValue;
    }
    public void SetSfxLvl(float value)
    {
        masterMixer.SetFloat("SfxLvl", value);
    }
    public void SetMusicLvl(float value)
    {
        masterMixer.SetFloat("MusicLvl", value);
    }
    public void SetVolumeLvl(float value)
    {
        masterMixer.SetFloat("VolumeLvl", value);
    }
}
