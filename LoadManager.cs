using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public AudioSource bgm, bgm2;
    public AudioLowPassFilter audioLowPassFilter;
    //public GameObject loading;
    public int maxLibrary, startLibrary;
    //public int load;
    //public GameObject load0, load1, load2, load3, load4, load5, load6, load7, load8, load9, load10, load11, load12, load13, load14, load15, load16, load17, load18, load19;
    // Start is called before the first frame update
    public GameObject loadingScreen;
    public LightShake lightShake;
    public SpriteRenderer background;
    public SpriteRenderer[] backgrounds;
    Coroutine changeText;

    public void loadGame(InputField inputField)
    {
        //Debug.Log(int.Parse(inputField.text));
        if (inputField.text != "")
        {
            int library = -1;
            try
            {
                library = int.Parse(inputField.text);
            }
            catch (FormatException)
            {
                if (inputField.text == "JOHNNY" || inputField.text == "johnny")
                {
                    AchievementManager.instance.Unlock("NEW_ACHIEVEMENT_1_5");
                    if (PlayerPrefs.GetInt("Cheat") == 0)
                    {
                        PlayerPrefs.SetInt("Cheat", 1);
                        inputField.placeholder.GetComponent<Text>().text = "CHEAT_ON";
                    }
                    else
                    {
                        PlayerPrefs.SetInt("Cheat", 0);
                        inputField.placeholder.GetComponent<Text>().text = "CHEAT_OFF";
                    }
                    inputField.text = "";

                    if (changeText != null)
                        StopCoroutine(changeText);
                    changeText = StartCoroutine(ChangeText(inputField));
                    return;
                }
            }
            if (0 <= library && library <= 64)
            {
                if (library > maxLibrary && (PlayerPrefs.GetInt("Cheat") != 1))
                {
                    inputField.placeholder.GetComponent<Text>().text = "NOT_CLEARED";
                    inputField.text = "";
                    if (changeText != null)
                        StopCoroutine(changeText);
                    changeText = StartCoroutine(ChangeText(inputField));
                }
                else
                {
                    //bgm.Stop();
                    loadingScreen.SetActive(true);
                    audioLowPassFilter.enabled = true;
                    PlayerPrefs.SetInt("StartLibrary", library);
                    SceneManager.LoadScene("SampleScene");
                }
            }
            else
            {
                inputField.placeholder.GetComponent<Text>().text = "NOT_EXIST";
                inputField.text = "";
                if (changeText != null)
                    StopCoroutine(changeText);
                changeText = StartCoroutine(ChangeText(inputField));
            }
        }
    }
    IEnumerator ChangeText(InputField inputField)
    {
        yield return new WaitForSeconds(1.5f);
        inputField.placeholder.GetComponent<Text>().text = "MOVE_F_TO...";
        yield return null;
    }

    void Start()
    {
        startLibrary = PlayerPrefs.GetInt("StartLibrary");
        maxLibrary = PlayerPrefs.GetInt("MaxLibrary");
        if (startLibrary <= 22)
        {
            background.sprite = backgrounds[0].sprite;
            lightShake.x = 0;
            lightShake.y = -3;
        }
        else if (startLibrary <= 43)
        {
            background.sprite = backgrounds[1].sprite;
            lightShake.x = -6;
            lightShake.y = 1.25f;
        }
        else if (startLibrary <= 57)
        {
            background.sprite = backgrounds[2].sprite;
            lightShake.x = 0.9f;
            lightShake.y = 2.8f;
        }
        else if (startLibrary < 64)
        {
            background.sprite = backgrounds[2].sprite;
            lightShake.x = -1.6f;
            lightShake.y = -0.1f;
        }
        else if (startLibrary <= 64)
        {
            background.sprite = backgrounds[3].sprite;
            lightShake.x = -6;
            lightShake.y = -3.3f;
            bgm.clip = bgm2.clip;
            bgm.time = 112.4f;
            bgm.Play();
        }
        /*
        if (maxLibrary >= 0) load0.SetActive(true);
        if (maxLibrary >= 1) load1.SetActive(true);
        if (maxLibrary >= 1.5) load2.SetActive(true);
        if (maxLibrary >= 2) load3.SetActive(true);
        if (maxLibrary >= 2.5) load4.SetActive(true);
        if (maxLibrary >= 3) load5.SetActive(true);
        if (maxLibrary >= 3.5) load6.SetActive(true);
        if (maxLibrary >= 4) load7.SetActive(true);
        if (maxLibrary >= 4.5) load8.SetActive(true);
        if (maxLibrary >= 5) load9.SetActive(true);
        if (maxLibrary >= 5.5) load10.SetActive(true);
        if (maxLibrary >= 6) load11.SetActive(true);
        if (maxLibrary >= 6.5) load12.SetActive(true);
        if (maxLibrary >= 7) load13.SetActive(true);
        if (maxLibrary >= 7.5) load14.SetActive(true);
        if (maxLibrary >= 8) load15.SetActive(true);
        if (maxLibrary >= 8.5) load16.SetActive(true);
        if (maxLibrary >= 9) load17.SetActive(true);
        if (maxLibrary >= 9.5) load18.SetActive(true);
        if (maxLibrary >= 10) load19.SetActive(true);*/
    }/*
    public void mapUpdate()
    {
        load0.SetActive(false); load1.SetActive(false); load2.SetActive(false); load3.SetActive(false); load4.SetActive(false); load5.SetActive(false); load6.SetActive(false); load7.SetActive(false); load8.SetActive(false); load9.SetActive(false);
        load10.SetActive(false); load11.SetActive(false); load12.SetActive(false); load13.SetActive(false); load14.SetActive(false); load15.SetActive(false); load16.SetActive(false); load17.SetActive(false); load18.SetActive(false); load19.SetActive(false);
        maxLibrary = PlayerPrefs.GetFloat("MaxLibrary");
        if (maxLibrary >= 0) load0.SetActive(true);
        if (maxLibrary >= 1) load1.SetActive(true);
        if (maxLibrary >= 1.5) load2.SetActive(true);
        if (maxLibrary >= 2) load3.SetActive(true);
        if (maxLibrary >= 2.5) load4.SetActive(true);
        if (maxLibrary >= 3) load5.SetActive(true);
        if (maxLibrary >= 3.5) load6.SetActive(true);
        if (maxLibrary >= 4) load7.SetActive(true);
        if (maxLibrary >= 4.5) load8.SetActive(true);
        if (maxLibrary >= 5) load9.SetActive(true);
        if (maxLibrary >= 5.5) load10.SetActive(true);
        if (maxLibrary >= 6) load11.SetActive(true);
        if (maxLibrary >= 6.5) load12.SetActive(true);
        if (maxLibrary >= 7) load13.SetActive(true);
        if (maxLibrary >= 7.5) load14.SetActive(true);
        if (maxLibrary >= 8) load15.SetActive(true);
        if (maxLibrary >= 8.5) load16.SetActive(true);
        if (maxLibrary >= 9) load17.SetActive(true);
        if (maxLibrary >= 9.5) load18.SetActive(true);
        if (maxLibrary >= 10) load19.SetActive(true);
    }

    public void Load0()
    {
        loading.SetActive(true);
        bgm.Stop();
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetFloat("StartLibrary", 0);
    }
    public void Load1()
    {
        loading.SetActive(true);
        bgm.Stop();
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetFloat("StartLibrary", 1);
    }
    public void Load2()
    {
        loading.SetActive(true);
        bgm.Stop();
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetFloat("StartLibrary", 1.5f);
    }
    public void Load3()
    {
        loading.SetActive(true);
        bgm.Stop();
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetFloat("StartLibrary", 2f);
    }
    public void Load4()
    {
        loading.SetActive(true);
        bgm.Stop();
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetFloat("StartLibrary", 2.5f);
    }
    public void Load5()
    {
        loading.SetActive(true);
        bgm.Stop();
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetFloat("StartLibrary", 3f);
    }
    public void Load6()
    {
        loading.SetActive(true);
        bgm.Stop();
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetFloat("StartLibrary", 3.5f);
    }
    public void Load7()
    {
        loading.SetActive(true);
        bgm.Stop();
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetFloat("StartLibrary", 4f);
    }
    public void Load8()
    {
        loading.SetActive(true);
        bgm.Stop();
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetFloat("StartLibrary", 4.5f);
    }
    public void Load9()
    {
        loading.SetActive(true);
        bgm.Stop();
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetFloat("StartLibrary", 5f);
    }
    public void Load10()
    {
        loading.SetActive(true);
        bgm.Stop();
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetFloat("StartLibrary", 5.5f);
    }
    public void Load11()
    {
        loading.SetActive(true);
        bgm.Stop();
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetFloat("StartLibrary", 6f);
    }
    public void Load12()
    {
        loading.SetActive(true);
        bgm.Stop();
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetFloat("StartLibrary", 6.5f);
    }
    public void Load13()
    {
        loading.SetActive(true);
        bgm.Stop();
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetFloat("StartLibrary", 7f);
    }
    public void Load14()
    {
        loading.SetActive(true);
        bgm.Stop();
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetFloat("StartLibrary", 7.5f);
    }
    public void Load15()
    {
        loading.SetActive(true);
        bgm.Stop();
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetFloat("StartLibrary", 8f);
    }
    public void Load16()
    {
        loading.SetActive(true);
        bgm.Stop();
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetFloat("StartLibrary", 8.5f);
    }
    public void Load17()
    {
        loading.SetActive(true);
        bgm.Stop();
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetFloat("StartLibrary", 9f);
    }
    public void Load18()
    {
        loading.SetActive(true);
        bgm.Stop();
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetFloat("StartLibrary", 9.5f);
    }
    public void Load19()
    {
        loading.SetActive(true);
        bgm.Stop();
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetFloat("StartLibrary", 10f);
    }*/
}
