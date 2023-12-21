using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeController : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundSlider;

    [SerializeField] AudioSource soundNoMoney;
    [SerializeField] AudioSource soundBuy;
    [SerializeField] AudioSource soundExplosion;

    void Start()
    {
        musicSlider.value = SaveLoadData.DATA.musicVolume;
        soundSlider.value = SaveLoadData.DATA.soundVolume;
    }

    public void ShooseMusicVolume()
    {
        SaveLoadData.DATA.musicVolume = musicSlider.value;
        MusicController.music.GetComponent<AudioSource>().volume = musicSlider.value;
    }

    public void ShooseSoundVolume()
    {
        SaveLoadData.DATA.soundVolume = soundSlider.value;
        soundNoMoney.volume = soundSlider.value;
        soundBuy.volume = soundSlider.value;
        soundExplosion.volume = soundSlider.value;
    }
}
