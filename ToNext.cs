using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToNext : MonoBehaviour
{
    public Hacking hacking;
    public Animator animator;
    public Animator cardUI;
    public GameObject dummy;
    public Text num;
    public int elvTime;
    public float y;
    bool next = false;
    bool open = false;
    public int type;
    public int bgm;
    //public bool npc;
    public bool mistake;
    public bool libraryIn;
    public bool libraryOut;
    public float fadeOutTime;
    public float fadeInTime;
    public bool noise;

    public int[] setTrue = { 0, 0, 0 };
    public int[] setFalse = { 0, 0, 0 };

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && open && !Player.instance.skill && ((!Player.instance.isDisguise || EffectManager.instance.effectSounds[12].source.isPlaying) || Player.instance.quitable == true))
        {
            if (Player.instance.isDisguise && !Player.instance.fade.enabled)
                Player.instance.StopDisguise();
            Player.instance.quitable = false;
            //collision.transform.position += new Vector3(0, y, 0);
            Player.instance.alternative = false;
            //if (npc) Player.instance.noR = true;
            //else Player.instance.noR = false;
            if (libraryIn)
            {
                if (bgm != -1)
                    if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmSounds[bgm].clip)
                        SoundManager.instance.bgmPlayer.Stop();
                Player.instance.library = true;
                cardUI.SetBool("appear", false);
                StartCoroutine(EnterLibrary());
            }
            else if (libraryOut)
            {
                if (bgm != -1)
                    if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmSounds[bgm].clip)
                        SoundManager.instance.bgmPlayer.Stop();
                //if (PlayerPrefs.GetInt("StartLibrary") < 45)
                    DialogueManager.instance.logButton.SetActive(false);
                StartCoroutine(QuitLibrary());
            }
            else
            {
                if (bgm != -1)
                    //if (!Player.instance.jokerOn)
                    //{
                    if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmSounds[bgm].clip)
                    {
                        SoundManager.instance.ChangeBGM(bgm);
                    }
                    //}
                    /*else
                    {
                        if (SoundManager.instance.bgmSounds[0].clip != SoundManager.instance.bgmSounds[bgm].clip)
                        {
                            SoundManager.instance.bgmSounds[0].clip = SoundManager.instance.bgmSounds[bgm].clip;
                            Player.instance.bgmTime = 0;
                        }
                    }*/
                if (noise == false && LoadManager2.instance.noise.activeSelf == true)
                    LoadManager2.instance.noise.SetActive(false);
                else if (noise == true && LoadManager2.instance.noise.activeSelf == false)
                    LoadManager2.instance.noise.SetActive(true);
                Player.instance.transform.position += new Vector3(0, y, 0);
                PlayerPrefs.SetInt("StartLibrary", PlayerPrefs.GetInt("StartLibrary") + 1);
                Player.instance.floor.text = PlayerPrefs.GetInt("StartLibrary").ToString() + "F";
                if (PlayerPrefs.GetInt("StartLibrary") > PlayerPrefs.GetInt("MaxLibrary"))
                    PlayerPrefs.SetInt("MaxLibrary", PlayerPrefs.GetInt("StartLibrary"));
            }

            if (type == 0) hacking.enabled = false;
            if (Player.instance.deck != 52)
            {
                if (Player.instance.deck == 0)
                    if (3 < PlayerPrefs.GetInt("StartLibrary") && PlayerPrefs.GetInt("StartLibrary") < 64)
                        AchievementManager.instance.Unlock("NEW_ACHIEVEMENT_1_8");
                Player.instance.deck = 52;
                EffectManager.instance.effectSounds[30].source.Play();
            }
            EffectManager.instance.effectSounds[20].source.Stop();
            EffectManager.instance.effectSounds[21].source.Stop();
            EffectManager.instance.effectSounds[22].source.Stop();
            PostItManager.instance.pass = false;

            DialogueManager.instance.log.text = "";

            for (int i = 0; i < 3; i++)
            {
                SwitchManager.instance.switches[setTrue[i]].bools = true;
                SwitchManager.instance.switches[setFalse[i]].bools = false;
            }

            /*if (bgm != -1)
                if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmSounds[bgm].clip)
                {
                    SoundManager.instance.bgmPlayer.clip = SoundManager.instance.bgmSounds[bgm].clip;
                    SoundManager.instance.bgmPlayer.Play();
                }*/
            Player.instance.toNext = true;
            open = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (type == 0)
        {
            if (hacking.hacking == hacking.maxHacking && !next)
            {
                next = true;
                if (elvTime == 0)
                {
                    open = true;
                    return;
                }
                StartCoroutine(Elevator());
            }
        }
        else if (type == 1)
        {
            open = true;
        }
    }

    IEnumerator Elevator()
    {
        if (mistake) Player.instance.deck /= 2;
        EffectManager.instance.effectSounds[8].source.Play();
        num.text = elvTime.ToString();
        animator.SetBool("elevator", true);
        yield return new WaitForSeconds(1);
        for (int i = elvTime; i >= 0; i--)
        {
            num.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        EffectManager.instance.effectSounds[29].source.Play();
        open = true;
        animator.SetBool("elevator", false);
    }

    IEnumerator EnterLibrary()
    {
        if (Player.instance.reloadImage1.enabled)
        {
            Player.instance.reloadImage1.enabled = false;
            Player.instance.reloadImage2.enabled = false;
        }
        if (EffectManager.instance.effectSounds[7].source.isPlaying)
            EffectManager.instance.effectSounds[7].source.Stop();

        Player.instance.skill = true;
        Player.instance.animator.SetBool("walking", false);

        if (fadeOutTime != 0)
            while (FadeManager.instance.fade2.color.a < 1 || Player.instance.jokerOn)
            {
                FadeManager.instance.fade2.color += new Color(0, 0, 0, Time.deltaTime / fadeOutTime);
                yield return null;
            }

        if (bgm != -1)
            if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmSounds[bgm].clip)
            {
                SoundManager.instance.ChangeBGM(bgm);
            }
        if (noise == false && LoadManager2.instance.noise.activeSelf == true)
            LoadManager2.instance.noise.SetActive(false);
        else if (noise == true && LoadManager2.instance.noise.activeSelf == false)
            LoadManager2.instance.noise.SetActive(true);
        Player.instance.transform.position += new Vector3(0, y, 0);
        PlayerPrefs.SetInt("StartLibrary", PlayerPrefs.GetInt("StartLibrary") + 1);
        Player.instance.floor.text = PlayerPrefs.GetInt("StartLibrary").ToString() + "F";
        if (PlayerPrefs.GetInt("StartLibrary") > PlayerPrefs.GetInt("MaxLibrary"))
            PlayerPrefs.SetInt("MaxLibrary", PlayerPrefs.GetInt("StartLibrary"));
        Player.instance.light2D.enabled = false;
        Player.instance.shadow.enabled = true;
        Player.instance.animator.speed = 0.5f;
        Player.instance.playerSpeed = Player.instance.slowSpeed * 2;

        DialogueManager.instance.logButton.SetActive(true);
        if (fadeInTime != 0)
            while (FadeManager.instance.fade2.color.a > 0)
            {
                FadeManager.instance.fade2.color -= new Color(0, 0, 0, Time.deltaTime / fadeInTime);
                yield return null;
            }

        yield return null;

        Player.instance.skill = false;
    }

    IEnumerator QuitLibrary()
    {
        if (DialogueManager.instance.logAnimator.GetBool("appear"))
            DialogueManager.instance.logAnimator.SetBool("appear", false);
        Player.instance.skill = true;
        Player.instance.animator.SetBool("walking", false);
        while (FadeManager.instance.fade2.color.a < 1 || Player.instance.jokerOn)
        {
            FadeManager.instance.fade2.color += new Color(0, 0, 0, Time.deltaTime / fadeOutTime);
            yield return null;
        }

        if (bgm != -1)
            if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmSounds[bgm].clip)
            {
                SoundManager.instance.ChangeBGM(bgm);
            }
        if (noise == false && LoadManager2.instance.noise.activeSelf == true)
            LoadManager2.instance.noise.SetActive(false);
        else if (noise == true && LoadManager2.instance.noise.activeSelf == false)
            LoadManager2.instance.noise.SetActive(true);
        Player.instance.transform.position += new Vector3(0, y, 0);
        PlayerPrefs.SetInt("StartLibrary", PlayerPrefs.GetInt("StartLibrary") + 1);
        Player.instance.floor.text = PlayerPrefs.GetInt("StartLibrary").ToString() + "F";
        if (PlayerPrefs.GetInt("StartLibrary") > PlayerPrefs.GetInt("MaxLibrary"))
            PlayerPrefs.SetInt("MaxLibrary", PlayerPrefs.GetInt("StartLibrary"));
        Player.instance.light2D.enabled = true;
        Player.instance.shadow.enabled = false;
        Player.instance.animator.speed = 0.7f;
        Player.instance.playerSpeed = Player.instance.defaultSpeed;
        if (Player.instance.reload == true)
        {
            Player.instance.reload = false;
            Player.instance.reloadImage1.enabled = false;
            Player.instance.reloadImage2.enabled = false;
        }

        Player.instance.library = false;
        cardUI.SetBool("appear", true);

        while (FadeManager.instance.fade2.color.a > 0)
        {
            FadeManager.instance.fade2.color -= new Color(0, 0, 0, Time.deltaTime / fadeInTime);
            yield return null;
        }

        yield return null;

        Player.instance.skill = false;
    }
}
