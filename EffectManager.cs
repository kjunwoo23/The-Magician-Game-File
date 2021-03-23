using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Effect
{
    public string soundName;
    public AudioClip clip;
    public AudioSource source;
}
public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;
    public Slider slider;
    public Text text;

    [Header("효과음 등록")]
    [SerializeField]
    public Effect[] effectSounds;

    void Start()
    {
        instance = this;
        //bgm이랑 bgs랑 다름!!!

        if (effectSounds.Length == 5)
        {
            for (int i = 0; i < effectSounds.Length; i++)
            {
                effectSounds[i].source = gameObject.AddComponent<AudioSource>();
                effectSounds[i].source.clip = effectSounds[i].clip;
                effectSounds[i].source.loop = false;
            }
            return;
        }

        float tmp = 0;
        if (PlayerPrefs.HasKey("EffectVolume"))
        {
            //Debug.Log(PlayerPrefs.GetFloat("EffectVolume"));
            //tmp = 0.5f;
            tmp = PlayerPrefs.GetFloat("EffectVolume");
            //slider.value = tmp;
        }
        else
            tmp = 0.5f;

        for (int i = 0; i < effectSounds.Length; i++)
        {
            effectSounds[i].source = gameObject.AddComponent<AudioSource>();
            effectSounds[i].source.clip = effectSounds[i].clip;
            effectSounds[i].source.loop = false;
            if (!(49 <= i && i <= 57))
                effectSounds[i].source.volume = tmp;
            else
                effectSounds[i].source.volume = 1;

            if (i == 1 || i == 2 || i == 40 || i == 41)
                effectSounds[i].source.volume *= 0.5f;
            if (i == 20 || i == 22)
                effectSounds[i].source.volume *= 0.3f;
            if (i == 21)
                effectSounds[i].source.volume *= 0.1f;
            if (i == 6 || i == 7 || i == 12 || i == 14 || i == 20 || i == 21 || i == 22)
                effectSounds[i].source.loop = true;
        }
        slider.value = tmp;
        text.text = tmp.ToString();
    }
    
    public void SetEffectVolume(Slider slider)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            float value = Mathf.Round(slider.value * 100) * 0.01f;
            effectSounds[i].source.volume = value;
            if (i == 1 || i == 2 || i == 40 || i == 41)
                effectSounds[i].source.volume *= 0.5f;
            if (i == 20 || i == 22)
                effectSounds[i].source.volume *= 0.3f;
            if (i == 21)
                effectSounds[i].source.volume *= 0.1f;
            if (49 <= i && i <= 57)
                effectSounds[i].source.volume = 1;
            PlayerPrefs.SetFloat("EffectVolume", value);
            text.text = value.ToString();
        }
        if (Time.timeScale == 0f)
            effectSounds[1].source.Play();
    }
}
