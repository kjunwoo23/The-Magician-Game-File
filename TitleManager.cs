using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public AudioSource bgm;
/*    public GameObject[] card;
    public Animator textCard;
    public Text[] texts;
    public Animator cards;
    public Animator photo;
    public Animator map;
    public Animator start;
    public Animator load;
    public Animator tutorial;
    public Animator note;
    public Animator quit;
    public Animator gun;*/
    //public GameObject block;
    /*
    public GameObject notice;
    public RawImage text;
    public RawImage newStart;
    //public RawImage load;
    public RawImage report;
    public RawImage reportError;
    public RawImage reportOpen;
    public RawImage levelHard;
    public RawImage levelNormal;
    public RawImage levelError;
    public RawImage exit;
    public GameObject hard;
    public GameObject loadOn;
    public Button mapButton;
    public GameObject fade;
    public GameObject loading;
    public RawImage[] noteImages;
    public RawImage shadow;
    public RawImage mapImage;*/
    public GameObject glass;
    public Animator buttons;
    public RawImage shadow;
    public RawImage buildingImage;
    public Text buildingText;
    public GameObject[] title;

    public AudioLowPassFilter audioLowPassFilter;
    public GameObject loadingScreen;
    Coroutine appear;

 //   public bool hardMode;
 //   public bool normalClear;

    // Start is called before the first frame update
    void Start()
    {
        if (Screen.fullScreen)
            Screen.SetResolution(1920, 1080, true);
        else
            Screen.SetResolution(1920, 1080, false);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        if (PlayerPrefs.GetInt("LangEng") == 0)
        {
            title[0].SetActive(true);
            title[1].SetActive(false);
        }
        else
        {
            title[0].SetActive(false);
            title[1].SetActive(true);
        }
        bgm.time = 13;
    }

    // Update is called once per frame
    void Update()
    {
        if (bgm.volume < 1) bgm.volume += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Escape) && glass.activeSelf)
            OnClickGlass();
    }

    public void OnClickUI()
    {
        appear = StartCoroutine(OnClickUI2());
    }
    public IEnumerator OnClickUI2()
    {
        glass.SetActive(true);
        shadow.enabled = true;
        //EffectManager.instance.effectSounds[0].source.Play();
        //yield return new WaitForSeconds(0.3f);
        buttons.SetBool("appear", true);
        EffectManager.instance.effectSounds[1].source.Play();
        yield return new WaitForSeconds(0.3f);
        EffectManager.instance.effectSounds[2].source.Play();
    }
    
    public void OnClickGlass()
    {
        if (appear != null)
            StopCoroutine(appear);
        glass.SetActive(false);
        shadow.enabled = false;
        buttons.SetBool("appear", false);
        buildingImage.enabled = false;
        buildingText.enabled = false;
    }

    public void OnClickStartNew()
    {
        //bgm.Stop();
        //glass.SetActive(false);
        loadingScreen.SetActive(true);
        //notice.SetActive(false);
        //PlayerPrefs.SetFloat("StartLibrary", 0);
        PlayerPrefs.SetInt("StartLibrary", 0);
        audioLowPassFilter.enabled = true;
        Invoke("LoadScene", 2);
        //SceneManager.LoadScene("SampleScene");
    }
    public void OnClickContinue()
    {
        //bgm.Stop();
        //glass.SetActive(false);
        loadingScreen.SetActive(true);
        //notice.SetActive(false);
        //PlayerPrefs.SetFloat("StartLibrary", 0);
        //Invoke("LoadScene", 1);
        audioLowPassFilter.enabled = true;
        Invoke("LoadScene", 2);
        //SceneManager.LoadScene("SampleScene");
    }

    public void OnClickExit()
    {
        StartCoroutine(Wait());
    }
    public void OnClickLang()
    {
        if (PlayerPrefs.GetInt("LangEng") == 0)
            PlayerPrefs.SetInt("LangEng", 1);
        else
            PlayerPrefs.SetInt("LangEng", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnClickKor()
    {
        if (PlayerPrefs.GetInt("LangEng") != 0)
        {
            PlayerPrefs.SetInt("LangEng", 0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void OnClickEng()
    {
        if (PlayerPrefs.GetInt("LangEng") == 0)
        {
            PlayerPrefs.SetInt("LangEng", 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void OnClickNotice()
    {
      /*  if (start.GetBool("Appear"))
        {
            loading.SetActive(true);
            bgm.Stop();
            start.SetBool("Appear", false);
            block.SetActive(false);
            notice.SetActive(false);
            PlayerPrefs.SetFloat("StartLibrary", 0);
            SceneManager.LoadScene("SampleScene");
        }
        if (note.GetBool("Appear"))
        {
            if (PlayerPrefs.GetFloat("MaxLibrary") == 10)
            {
                bgm.Stop();
                EffectManager.instance.effectSounds[6].source.Play();
                notice.SetActive(false);
                block.SetActive(true);
                shadow.enabled = false;
                mapImage.enabled = false;
                noteImages[0].enabled = true;
                noteImages[0].texture = noteImages[1].texture;
                return;
            }
            else
            {
                EffectManager.instance.effectSounds[3].source.Play();
                text.texture = reportError.texture;
                return;
            }/*
            note.SetBool("Appear", false);
            block.SetActive(false);
            notice.SetActive(false);
        }
        if (photo.GetBool("Appear"))
        {
            PlayerPrefs.SetFloat("MaxLibrary", 0);
            GameObject.Find("LoadManager(Title)").GetComponent<LoadManager>().mapUpdate();
            EffectManager.instance.effectSounds[3].source.Play();
            hard.SetActive(false);
            photo.SetBool("Appear", false);
            block.SetActive(false);
            notice.SetActive(false);
        }
        if (map.GetBool("Appear"))
        {
            mapButton.enabled = false;
            loadOn.SetActive(true);
            notice.SetActive(false);
        }
        if (gun.GetBool("Appear"))
        {
            EffectManager.instance.effectSounds[5].source.Play();
            fade.SetActive(true);
            StartCoroutine("Wait");
        }*/
    }

    public void OnClickStart()
    {/*
        EffectManager.instance.effectSounds[0].source.Play();
        textCard.SetBool("appear", true);
        texts[5].text = texts[0].text;
        start.SetBool("click", true);
        for (int i = 0; i < 5; i++)
            card[i].SetActive(false);
        Invoke("OnClickStart2", 0.25f);*/
    }
    public void OnClickStart2()
    {/*
        card[0].SetActive(true);
        card[0].GetComponent<Button>().enabled = true;
        cards.SetBool("appear", true);/*
        EffectManager.instance.effectSounds[0].source.Play();
        text.texture = newStart.texture;
        notice.SetActive(true);
        block.SetActive(true);*/
    }
    public void OnClickStart3()
    {
        EffectManager.instance.effectSounds[1].source.Play();
        //cards.SetBool("appear", false);
        //mapButton.enabled = true;
        //start.SetBool("click", false);
        //loading.SetActive(true);
        bgm.Stop();
        glass.SetActive(false);
        loadingScreen.SetActive(true);
        //notice.SetActive(false);
        //PlayerPrefs.SetFloat("StartLibrary", 0);
        //Invoke("LoadScene", 1);
        PlayerPrefs.SetInt("StartLibrary", 0);
        Invoke("LoadScene", 2);
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void OnClickLoad()
    {/*
        EffectManager.instance.effectSounds[0].source.Play();
        textCard.SetBool("appear", true);
        texts[5].text = texts[1].text;
        load.SetBool("click", true);
        for (int i = 0; i < 5; i++)
            card[i].SetActive(false);
        Invoke("OnClickLoad2", 0.25f);*/
    }
    public void OnClickLoad2()
    {/*
        card[1].SetActive(true);
        card[1].GetComponent<Button>().enabled = true;
        cards.SetBool("appear", true);/*
        EffectManager.instance.effectSounds[0].source.Play();
        text.texture = newStart.texture;
        notice.SetActive(true);
        block.SetActive(true);*/
    }
    public void OnClickLoad3()
    {
        EffectManager.instance.effectSounds[1].source.Play();
        //cards.SetBool("appear", false);
        //mapButton.enabled = true;
        //load.SetBool("click", false);
        //loading.SetActive(true);
        bgm.Stop();
        glass.SetActive(false);
        loadingScreen.SetActive(true);
        //notice.SetActive(false);
        //PlayerPrefs.SetFloat("StartLibrary", 0);
        //Invoke("LoadScene", 1);
        Invoke("LoadScene", 2);
    }
    public void OnClickTutorial()
    {/*
        EffectManager.instance.effectSounds[0].source.Play();
        textCard.SetBool("appear", true);
        texts[5].text = texts[2].text;
        tutorial.SetBool("click", true);
        for (int i = 0; i < 5; i++)
            card[i].SetActive(false);
        Invoke("OnClickTutorial2", 0.25f);*/
    }
    public void OnClickTutorial2()
    {/*
        card[2].SetActive(true);
        card[2].GetComponent<Button>().enabled = true;
        cards.SetBool("appear", true);/*
        EffectManager.instance.effectSounds[0].source.Play();
        text.texture = newStart.texture;
        notice.SetActive(true);
        block.SetActive(true);*/
    }
    public void OnClickNote()
    {/*
        EffectManager.instance.effectSounds[0].source.Play();
        textCard.SetBool("appear", true);
        texts[5].text = texts[3].text;
        note.SetBool("click", true);
        for (int i = 0; i < 5; i++)
            card[i].SetActive(false);
        Invoke("OnClickNote2", 0.25f);*/
    }
    public void OnClickNote2()
    {/*
        card[3].SetActive(true);
        card[3].GetComponent<Button>().enabled = true;
        cards.SetBool("appear", true);/*
        EffectManager.instance.effectSounds[0].source.Play();
        text.texture = newStart.texture;
        notice.SetActive(true);
        block.SetActive(true);*/
    }
    public void OnClickQuit()
    {/*
        EffectManager.instance.effectSounds[0].source.Play();
        textCard.SetBool("appear", true);
        texts[5].text = texts[4].text;
        quit.SetBool("click", true);
        for (int i = 0; i < 5; i++)
            card[i].SetActive(false);
        Invoke("OnClickQuit2", 0.25f);*/
    }
    public void OnClickQuit2()
    {/*
        card[4].SetActive(true);
        card[4].GetComponent<Button>().enabled = true;
        cards.SetBool("appear", true);/*
        EffectManager.instance.effectSounds[0].source.Play();
        text.texture = newStart.texture;
        notice.SetActive(true);
        block.SetActive(true);*/
    }
    public void OnClickQuit3()
    {/*
        EffectManager.instance.effectSounds[1].source.Play();
        cards.SetBool("appear", false);
        //mapButton.enabled = true;
        quit.SetBool("click", false);
        //loading.SetActive(true);
        bgm.Stop();
        block.SetActive(false);
        //notice.SetActive(false);
        //PlayerPrefs.SetFloat("StartLibrary", 0);
        Invoke("OnApplicationQuit", 1);*/
    }
    public void OnClickElse()
    {/*
        if (noteImages[0].enabled == true)
        {
            for (int i = 8; i >= 0; i--)
            {
                if (noteImages[0].texture == noteImages[8].texture)
                {
                    bgm.Play();
                    note.SetBool("Appear", false);
                    noteImages[0].enabled = false;
                    block.SetActive(false);
                    shadow.enabled = true;
                    mapImage.enabled = true;
                    return;
                }
                if (noteImages[0].texture == noteImages[i].texture)
                {
                    EffectManager.instance.effectSounds[1].source.Play();
                    noteImages[0].texture = noteImages[i + 1].texture;
                    return;
                }
            }
        }*/
        EffectManager.instance.effectSounds[1].source.Play();
        //textCard.SetBool("appear", false);
        //cards.SetBool("appear", false);
        //mapButton.enabled = true;
        //start.SetBool("click", false);
        //load.SetBool("click", false);
        //tutorial.SetBool("click", false);
        //note.SetBool("click", false);
        //quit.SetBool("click", false);
        //for (int i = 0; i < 5; i++)
            //card[i].GetComponent<Button>().enabled = false;
        //loadOn.SetActive(false);
        //note.SetBool("appear", false);
        //photo.SetBool("appear", false);
        //map.SetBool("appear", false);
        //gun.SetBool("appear", false);
        //notice.SetActive(false);
        glass.SetActive(false);
    }
    public void OnApplicationQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        bgm.Stop();
        yield return new WaitForSeconds(1.5f);
        OnApplicationQuit();
    }
}
