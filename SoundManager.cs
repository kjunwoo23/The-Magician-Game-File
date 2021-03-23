using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public Slider slider;
    public Text text;
    public Text bgmName;

    [Header("브금 플레이어")]
    [SerializeField]
    public AudioSource bgmPlayer;
    public AudioLowPassFilter lowPassFilter;

    [Header("사운드 등록")]
    [SerializeField]
    public Sound[] bgmSounds;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            bgmPlayer.volume = PlayerPrefs.GetFloat("SoundVolume");
            slider.value = bgmPlayer.volume;
        }
        else
            bgmPlayer.volume = 1;
        //bgmPlayer.clip = bgmSounds[1].clip;
        //bgmPlayer.Play();
        text.text = bgmPlayer.volume.ToString();
    }
    public void SetBgmVolume(Slider slider)
    {
        float value = Mathf.Round(slider.value * 100) * 0.01f;
        bgmPlayer.volume = value;
        PlayerPrefs.SetFloat("SoundVolume", value);
        text.text = value.ToString();
        //if (bgmPlayer.clip == bgmSounds[3].clip)
        //bgmPlayer.volume = slider.value * 0.5f;
    }
    public void ChangeBGM(int i)
    {
        switch (i)
        {
            case 0: bgmName.text = "♪\t'Undefined'\n- Undefined"; break;
            case 1: bgmName.text = "♪\t'Undefined'\n- Undefined"; break;
            case 2: bgmName.text = "♪\t'Horizons'\n- Scott Buckley"; break;
            case 3: bgmName.text = "♪\t'Descent'\n- Scott Buckley"; break;
            case 4: bgmName.text = "♪\t'Spy/action hybrid...'\n- GBR MUSIC"; break;
            case 5: bgmName.text = "♪\t-File Empty-\n- Unknown Artist"; break;
            case 6: bgmName.text = "♪\t'The Black Waltz'\n- Scott Buckley"; break;
            case 7: bgmName.text = "♪\t'Snowfall'\n- Scott Buckley"; break;
            case 8: bgmName.text = "♪\t'Filaments'\n- Scott Buckley"; break;
            case 9: bgmName.text = "♪\t'The Fury'\n- Scott Buckley"; break;
            case 10: bgmName.text = "♪\t'Tears in Rain'\n- Scott Buckley"; break;
            case 11: bgmName.text = "♪\t'This Too Shall Pass'\n- Scott Buckley"; break;
            case 12: bgmName.text = "♪\t'Emergent'\n- Scott Buckley"; break;
            case 13: bgmName.text = "♪\t'Catalyst'\n- Scott Buckley"; break;
            case 14: bgmName.text = "♪\t'Freedom'\n- Scott Buckley"; break;
        }
        bgmPlayer.clip = bgmSounds[i].clip;
        bgmPlayer.time = 0;
        bgmPlayer.Play();
    }
}


