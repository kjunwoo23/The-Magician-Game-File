using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager2 : MonoBehaviour
{
    int bgm;
    int startLibrary;
    public int currentLibrary;
    public Transform[] savePoints;
    //public GameObject player1;
    public SoundManager soundManager;
    public EffectManager effectManager;
    public static LoadManager2 instance;
    public Animator cardUI;
    public GameObject noise;
    Coroutine changeText;
    public GameObject[] textKor;
    public GameObject[] textEng;
    public GameObject johnny;

    public void loadGame(InputField inputField)
    {
        //Debug.Log(int.Parse(inputField.text));
        if (true)
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
                            johnny.SetActive(true);
                        }
                        else
                        {
                            PlayerPrefs.SetInt("Cheat", 0);
                            inputField.placeholder.GetComponent<Text>().text = "CHEAT_OFF";
                            johnny.SetActive(false);
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
                if (library > PlayerPrefs.GetInt("MaxLibrary") && (PlayerPrefs.GetInt("Cheat") != 1))
                {
                    inputField.placeholder.GetComponent<Text>().text = "NOT_CLEARED";
                    inputField.text = "";
                    if (changeText != null)
                        StopCoroutine(changeText);
                    changeText = StartCoroutine(ChangeText(inputField));
                }
                else
                {
                    Player.instance.blur.enabled = false;
                    Time.timeScale = 1f;
                    Player.instance.cm.enabled = true;
                    Time.fixedDeltaTime = 0.02f * Time.timeScale;
                    Player.instance.paused = false;
                    PlayerPrefs.SetInt("StartLibrary", library);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        yield return new WaitForSecondsRealtime(1.5f);
        inputField.placeholder.GetComponent<Text>().text = "MOVE_F_TO...";
        yield return null;
    }

    void isLibrary()
    {
        Player.instance.library = true;
        Player.instance.light2D.enabled = false;
        Player.instance.shadow.enabled = true;
        Player.instance.animator.speed = 0.5f;
        Player.instance.playerSpeed = Player.instance.slowSpeed * 2;
        DialogueManager.instance.logButton.SetActive(true);
        cardUI.SetBool("appear", false);
    }

    void isNotLibrary()
    {
        Player.instance.light2D.enabled = true;
        Player.instance.shadow.enabled = false;
        Player.instance.animator.speed = 0.7f;
        Player.instance.playerSpeed = Player.instance.defaultSpeed;
        Player.instance.library = false;
        //if (PlayerPrefs.GetInt("StartLibrary") < 45)
            DialogueManager.instance.logButton.SetActive(false);
        cardUI.SetBool("appear", true);
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        startLibrary = PlayerPrefs.GetInt("StartLibrary");
        currentLibrary = startLibrary;
        //MapManager.instance.mapUpdate();

        if (Screen.fullScreen)
            Screen.SetResolution(1920, 1080, true);
        else
            Screen.SetResolution(1920, 1080, false);

        if (PlayerPrefs.GetInt("LangEng") == 0)
        {
            for (int i = 0; i < textKor.Length; i++)
            {
                textKor[i].SetActive(true);
                textEng[i].SetActive(false);
            }
        }
        else
            for (int i = 0; i < textKor.Length; i++)
            {
                textKor[i].SetActive(false);
                textEng[i].SetActive(true);
            }

        Player.instance.transform.position = savePoints[startLibrary].position;
        if (startLibrary == 0)
            Player.instance.floor.text = "--" + "F";
        else
            Player.instance.floor.text = startLibrary.ToString() + "F";

        if (PlayerPrefs.GetInt("Cheat") == 1)
            johnny.SetActive(true);

        //Debug.Log(savePoints[startLibrary].position.y);
        switch (startLibrary)
        {
            case 0: noise.SetActive(true); bgm = 3; break;
            case 1: isLibrary(); bgm = 2; break;
            case 2: isNotLibrary(); noise.SetActive(true); bgm = 2; break;
            case 3: isLibrary(); bgm = 2; break;
            case 4: isNotLibrary(); bgm = 5; break;
            case 5: isNotLibrary(); bgm = 4; break;
            case 6: isNotLibrary(); bgm = 4; break;
            case 7: isNotLibrary(); bgm = 4; break;
            case 8: isNotLibrary(); bgm = 4; break;
            case 9: isNotLibrary(); bgm = 4; break;
            case 10: isLibrary(); soundManager.lowPassFilter.enabled = true; bgm = 4; break;
            case 11: isNotLibrary(); bgm = 4; break;
            case 12: isNotLibrary(); bgm = 4; break;
            case 13: isNotLibrary(); soundManager.lowPassFilter.enabled = true; bgm = 4; break;
            case 14: isLibrary(); soundManager.lowPassFilter.enabled = true; bgm = 4; break;
            case 15: isLibrary(); bgm = 7; break;
            case 16: isNotLibrary(); bgm = 7; break;
            case 17: isLibrary(); bgm = 7; break;
            case 18: isNotLibrary(); bgm = 7; break;
            case 19: isNotLibrary(); bgm = 5; break;
            case 20: isNotLibrary(); bgm = 9; break;
            case 21: isNotLibrary(); bgm = 9; break;
            case 22: isLibrary(); bgm = 5; break;
            case 23: isLibrary(); bgm = 10; break;
            case 24: isNotLibrary(); bgm = 10; break;
            case 25: isNotLibrary(); bgm = 10; break;
            case 26: isNotLibrary(); bgm = 10; break;
            case 27: isLibrary(); bgm = 10; break;
            case 28: isNotLibrary(); bgm = 10; break;
            case 29: isNotLibrary(); bgm = 10; break;
            case 30: isLibrary(); bgm = 10; break;
            case 31: isLibrary(); bgm = 10; break;
            case 32: isNotLibrary(); bgm = 10; break;
            case 33: isNotLibrary(); bgm = 10; break;
            case 34: isLibrary(); bgm = 10; break;
            case 35: isNotLibrary(); bgm = 10; break;
            case 36: isNotLibrary(); bgm = 10; break;
            case 37: isNotLibrary(); bgm = 10; break;
            case 38: isLibrary(); bgm = 10; break;
            case 39: isNotLibrary(); bgm = 10; break;
            case 40: isNotLibrary(); bgm = 10; break;
            case 41: isNotLibrary(); bgm = 10; break;
            case 42: isNotLibrary(); bgm = 10; break;
            case 43: isLibrary(); bgm = 5; break;
            case 44: isLibrary(); bgm = 8; break;
            case 45: isNotLibrary(); bgm = 8; break;
            case 46: isNotLibrary(); bgm = 8; break;
            case 47: isNotLibrary(); bgm = 11; break;
            case 48: isNotLibrary(); bgm = 11; break;
            case 49: isNotLibrary(); bgm = 11; break;
            case 50: isNotLibrary(); bgm = 11; break;
            case 51: isNotLibrary(); bgm = 11; break;
            case 52: isNotLibrary(); bgm = 11; break;
            case 53: isLibrary(); bgm = 11; break;
            case 54: isNotLibrary(); bgm = 11; break;
            case 55: isNotLibrary(); bgm = 11; break;
            case 56: isNotLibrary(); bgm = 5; break;
            case 57: isLibrary(); bgm = 12; break;
            case 58: isLibrary(); bgm = 5; break;
            case 59: isNotLibrary(); bgm = 13; break;
            case 60: isNotLibrary(); bgm = 13; break;
            case 61: isNotLibrary(); bgm = 13; break;
            case 62: isNotLibrary(); bgm = 13; break;
            case 63: noise.SetActive(true); bgm = 13; break;
            case 64: isLibrary(); bgm = 14; Player.instance.floor.enabled = false; break;
        }

        soundManager.ChangeBGM(bgm);

        if (startLibrary >= 24)
        {
            Player.instance.rOpen = true;
            SwitchManager.instance.rOpen.SetActive(true);
            Player.instance.light2D.color = new Color (1, 0.92f, 0.745f);
        }
        if (startLibrary >= 45)
        {
            Player.instance.sOpen = true;
            SwitchManager.instance.sOpen.SetActive(true);
            //Player.instance.light2D.color = new Color(0.616f, 0.66f, 0.8f);
            Player.instance.light2D.color = new Color(0.55f, 0.61f, 0.81f);
        }

        if (startLibrary <= 3)
            SwitchManager.instance.cards.enabled = false;
        else if (startLibrary <= 22)
            SwitchManager.instance.cards.texture = SwitchManager.instance.threeCards[0].texture;
        else if (startLibrary == 23)
            SwitchManager.instance.cards.enabled = false;
        else if (24 <= startLibrary && startLibrary <= 43)
            SwitchManager.instance.cards.texture = SwitchManager.instance.threeCards[1].texture;
        else if (startLibrary == 44)
            SwitchManager.instance.cards.enabled = false;
        else if (45 <= startLibrary && startLibrary <= 57)
            SwitchManager.instance.cards.texture = SwitchManager.instance.threeCards[2].texture;
        else if (58 <= startLibrary)
            SwitchManager.instance.cards.enabled = false;

        /*
        if (startLibrary == 0)
        {
            soundManager.bgmPlayer.clip = soundManager.bgmSounds[0].clip;
            soundManager.bgmPlayer.Play();
        }
        if (startLibrary == 1)
        {
            soundManager.bgmPlayer.clip = soundManager.bgmSounds[1].clip;
            soundManager.bgmPlayer.Play();
            effectManager.effectSounds[11].clip = effectManager.effectSounds[14].clip;
            player1.transform.position = new Vector3(-206.59f, -58.7f,0);
        }
        if (startLibrary == 1.5)
        {
            soundManager.bgmPlayer.clip = soundManager.bgmSounds[2].clip;
            soundManager.bgmPlayer.Play();
            player1.transform.position = new Vector3(-159.87f, 1.98f, 0);
        }
        if (startLibrary == 2)
        {
            soundManager.bgmPlayer.clip = soundManager.bgmSounds[1].clip;
            soundManager.bgmPlayer.Play();
            effectManager.effectSounds[11].clip = effectManager.effectSounds[14].clip;
            player1.transform.position = new Vector3(147.4f, -57.4f, 0);
        }
        if (startLibrary == 2.5)
        {
            soundManager.bgmPlayer.clip = soundManager.bgmSounds[9].clip;
            soundManager.bgmPlayer.Play();
            player1.transform.position = new Vector3(185.6f, -0.6f, 0);
            dqwd.knife = true;
            GameObject.Find("Quad (2)").GetComponent<MeshRenderer>().materials = GameObject.Find("Quad (4)").GetComponent<MeshRenderer>().materials;
        }
        if (startLibrary == 3)
        {
            soundManager.bgmPlayer.clip = soundManager.bgmSounds[1].clip;
            soundManager.bgmPlayer.Play();
            effectManager.effectSounds[11].clip = effectManager.effectSounds[14].clip;
            player1.transform.position = new Vector3(457.9f, -59.1f, 0);
            dqwd.knife = true;
            GameObject.Find("Quad (2)").GetComponent<MeshRenderer>().materials = GameObject.Find("Quad (4)").GetComponent<MeshRenderer>().materials;
        }
        if (startLibrary == 3.5)
        {
            soundManager.bgmPlayer.clip = soundManager.bgmSounds[7].clip;
            soundManager.bgmPlayer.Play();
            player1.transform.position = new Vector3(479.24f, -0.62f, 0);
            dqwd.knife = true;
            GameObject.Find("Quad (2)").GetComponent<MeshRenderer>().materials = GameObject.Find("Quad (4)").GetComponent<MeshRenderer>().materials;
        }
        if (startLibrary == 4)
        {
            soundManager.bgmPlayer.clip = soundManager.bgmSounds[1].clip;
            soundManager.bgmPlayer.Play();
            effectManager.effectSounds[11].clip = effectManager.effectSounds[14].clip;
            player1.transform.position = new Vector3(793.72f, -59.12f, 0);
            dqwd.knife = true;
            GameObject.Find("Quad (2)").GetComponent<MeshRenderer>().materials = GameObject.Find("Quad (4)").GetComponent<MeshRenderer>().materials;
        }
        if (startLibrary == 4.5)
        {
            soundManager.bgmPlayer.clip = soundManager.bgmSounds[10].clip;
            soundManager.bgmPlayer.Play();
            player1.transform.position = new Vector3(817.6f, -0.57f, 0);
            dqwd.knife = true;
            GameObject.Find("Quad (2)").GetComponent<MeshRenderer>().materials = GameObject.Find("Quad (4)").GetComponent<MeshRenderer>().materials;
        }
        if (startLibrary == 5)
        {
            soundManager.bgmPlayer.clip = soundManager.bgmSounds[1].clip;
            soundManager.bgmPlayer.Play();
            effectManager.effectSounds[11].clip = effectManager.effectSounds[14].clip;
            player1.transform.position = new Vector3(1054.9f, -59f, 0);
            dqwd.knife = true;
            GameObject.Find("Quad (2)").GetComponent<MeshRenderer>().materials = GameObject.Find("Quad (4)").GetComponent<MeshRenderer>().materials;
        }
        if (startLibrary == 5.5)
        {
            soundManager.bgmPlayer.clip = soundManager.bgmSounds[10].clip;
            soundManager.bgmPlayer.Play();
            player1.transform.position = new Vector3(1088.5f, -0.57f, 0);
            dqwd.knife = true;
            GameObject.Find("Quad (2)").GetComponent<MeshRenderer>().materials = GameObject.Find("Quad (4)").GetComponent<MeshRenderer>().materials;
        }
        if (startLibrary == 6)
        {
            soundManager.bgmPlayer.clip = soundManager.bgmSounds[3].clip;
            soundManager.bgmPlayer.volume = 0.5f;
            soundManager.bgmPlayer.Play();
            effectManager.effectSounds[11].clip = effectManager.effectSounds[14].clip;
            player1.transform.position = new Vector3(1455.6f, -58.9f, 0);
            dqwd.knife = true;
            GameObject.Find("SwitchManager").GetComponent<SwitchManager>().switches[15].bools = true;
            GameObject.Find("Quad (2)").GetComponent<MeshRenderer>().materials = GameObject.Find("Quad (4)").GetComponent<MeshRenderer>().materials;
        }
        if (startLibrary == 6.5)
        {
            soundManager.bgmPlayer.clip = soundManager.bgmSounds[11].clip;
            soundManager.bgmPlayer.Play();
            effectManager.effectSounds[11].clip = effectManager.effectSounds[16].clip;
            player1.transform.position = new Vector3(1486, -0.57f, 0);
            dqwd.knife = true;
            GameObject.Find("SwitchManager").GetComponent<SwitchManager>().switches[16].bools = true;
            GameObject.Find("Quad (2)").GetComponent<MeshRenderer>().materials = GameObject.Find("Quad (4)").GetComponent<MeshRenderer>().materials;
        }
        if (startLibrary == 7)
        {
            soundManager.bgmPlayer.clip = soundManager.bgmSounds[9].clip;
            soundManager.bgmPlayer.Play();
            effectManager.effectSounds[11].clip = effectManager.effectSounds[14].clip;
            player1.transform.position = new Vector3(1748, -58.4f, 0);
            dqwd.knife = true;
            GameObject.Find("SwitchManager").GetComponent<SwitchManager>().switches[16].bools = true;
            GameObject.Find("Quad (2)").GetComponent<MeshRenderer>().materials = GameObject.Find("Quad (4)").GetComponent<MeshRenderer>().materials;
        }
        if (startLibrary == 7.5)
        {
            soundManager.bgmPlayer.clip = soundManager.bgmSounds[12].clip;
            soundManager.bgmPlayer.Play();
            effectManager.effectSounds[11].clip = effectManager.effectSounds[16].clip;
            player1.transform.position = new Vector3(1760, -0.57f, 0);
            dqwd.knife = true;
            GameObject.Find("SwitchManager").GetComponent<SwitchManager>().switches[16].bools = true;
            GameObject.Find("Quad (2)").GetComponent<MeshRenderer>().materials = GameObject.Find("Quad (4)").GetComponent<MeshRenderer>().materials;
        }
        if (startLibrary == 8)
        {
            soundManager.bgmPlayer.clip = soundManager.bgmSounds[9].clip;
            soundManager.bgmPlayer.Play();
            effectManager.effectSounds[11].clip = effectManager.effectSounds[14].clip;
            player1.transform.position = new Vector3(2035, -58.4f, 0);
            dqwd.knife = true;
            GameObject.Find("SwitchManager").GetComponent<SwitchManager>().switches[16].bools = true;
            GameObject.Find("Quad (2)").GetComponent<MeshRenderer>().materials = GameObject.Find("Quad (4)").GetComponent<MeshRenderer>().materials;
        }
        if (startLibrary == 8.5)
        {
            soundManager.bgmPlayer.clip = soundManager.bgmSounds[8].clip;
            soundManager.bgmPlayer.Play();
            effectManager.effectSounds[11].clip = effectManager.effectSounds[16].clip;
            player1.transform.position = new Vector3(2047, -0.57f, 0);
            dqwd.knife = true;
            GameObject.Find("SwitchManager").GetComponent<SwitchManager>().switches[16].bools = true;
            GameObject.Find("Quad (2)").GetComponent<MeshRenderer>().materials = GameObject.Find("Quad (4)").GetComponent<MeshRenderer>().materials;
        }
        if (startLibrary == 9)
        {
            soundManager.bgmPlayer.clip = soundManager.bgmSounds[8].clip;
            soundManager.bgmPlayer.Play();
            effectManager.effectSounds[11].clip = effectManager.effectSounds[16].clip;
            player1.transform.position = new Vector3(2277.75f, -0.57f, 0);
            dqwd.knife = true;
            GameObject.Find("SwitchManager").GetComponent<SwitchManager>().switches[16].bools = true;
            GameObject.Find("Quad (2)").GetComponent<MeshRenderer>().materials = GameObject.Find("Quad (4)").GetComponent<MeshRenderer>().materials;
        }
        if (startLibrary == 9.5)
        {
            soundManager.bgmPlayer.clip = soundManager.bgmSounds[8].clip;
            soundManager.bgmPlayer.Play();
            effectManager.effectSounds[11].clip = effectManager.effectSounds[16].clip;
            player1.transform.position = new Vector3(2349f, 60.4f, 0);
            dqwd.knife = true;
            GameObject.Find("SwitchManager").GetComponent<SwitchManager>().switches[16].bools = true;
            GameObject.Find("Quad (2)").GetComponent<MeshRenderer>().materials = GameObject.Find("Quad (4)").GetComponent<MeshRenderer>().materials;
        }
        if (startLibrary == 10)
        {
            //dqwd.playerSpeed = 15;
            soundManager.bgmPlayer.clip = soundManager.bgmSounds[8].clip;
            soundManager.bgmPlayer.Play();
            player1.transform.position = new Vector3(1098f, 61.83f, 0);
            dqwd.knife = true;
            GameObject.Find("SwitchManager").GetComponent<SwitchManager>().switches[16].bools = true;
            GameObject.Find("Quad (2)").GetComponent<MeshRenderer>().materials = GameObject.Find("Quad (4)").GetComponent<MeshRenderer>().materials;
        }

        */

    }
  
}
