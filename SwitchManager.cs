using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Experimental.Rendering.Universal;

[System.Serializable]
public class Switch
{
    public string switchName;
    public bool bools;
}
public class SwitchManager : MonoBehaviour
{
    public static SwitchManager instance;

    [Header("스위치 등록")]
    [SerializeField]
    public Switch[] switches;
    public CinemachineVirtualCamera cm;
    public Player player;

    public Line[] enableLines;

    public SpriteRenderer sprite1;
    public Door door1, door2;

    public Light2D[] spotLightClover;
    public Line[] lineClover;
    public SpriteRenderer[] clover;
    public GameObject rWall;
    public GameObject[] wall;

    public Light2D[] spotLightHeart;
    public SpriteRenderer[] heart;
    public GameObject rWall2;

    public Light2D[] spotLightSpade;
    public Line[] lineSpade;
    public SpriteRenderer[] spade;
    public GameObject rWall1;


    public GameObject rOpen;
    public GameObject sOpen;

    public RawImage cards;
    public RawImage[] threeCards;

    public GameObject ending;
    public GameObject endingCredit;
    public float speed;
    /*
    public int A, B;
    public RawImage[] twoSideCards;
    public bool clicked;
    */
    private void Start()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (switches[1].bools == true)
        {
            player.animator.speed = 0.7f;
            player.StartCoroutine(player.LightOff());
            enableLines[0].enabled = false;
            Invoke("func1", 5);
            switches[1].bools = false;
        }
        if (switches[2].bools == true)
        {
            player.animator.speed = 0.7f;
            door1.StartCoroutine(door1.DoorOpen(player.GetComponent<Collider2D>()));
            enableLines[1].enabled = false;
            switches[2].bools = false;
        }
        if (switches[3].bools == true)
        {
            SoundManager.instance.ChangeBGM(4);
            switches[3].bools = false;
        }
        if (switches[4].bools == true)
        {
            SoundManager.instance.ChangeBGM(5);
            EffectManager.instance.effectSounds[8].source.Play();
            switches[4].bools = false;
        }
        if (switches[5].bools == true)
        {
            SoundManager.instance.lowPassFilter.enabled = false;
            SoundManager.instance.ChangeBGM(7);
            switches[5].bools = false;
        }
        if (switches[6].bools == true)
        {
            SoundManager.instance.lowPassFilter.enabled = true;
            switches[6].bools = false;
        }
        if (switches[7].bools == true)
        {
            SoundManager.instance.lowPassFilter.enabled = false;
            switches[7].bools = false;
        }
        if (switches[8].bools == true)
        {
            SoundManager.instance.ChangeBGM(9);
            switches[8].bools = false;
        }
        if (switches[9].bools == true)
        {
            StartCoroutine(SpotLightOn(0));
            switches[9].bools = false;
        }
        if (switches[10].bools == true)
        {
            for (int i = 0; i < 1; i++)
            {
                spotLightClover[i].enabled = false;
                clover[i].enabled = false;
            }
            SoundManager.instance.ChangeBGM(6);
            EffectManager.instance.effectSounds[48].source.Play();
            StartCoroutine(SpotLightOn(1));
            switches[10].bools = false;
        }
        if (switches[11].bools == true)
        {
            for (int i = 0; i < 2; i++)
            {
                spotLightClover[i].enabled = false;
                clover[i].enabled = false;
            }
            EffectManager.instance.effectSounds[48].source.Play();
            StartCoroutine(SpotLightOn(2));
            switches[11].bools = false;
        }
        if (switches[12].bools == true)
        {
            for (int i = 0; i < 3; i++)
            {
                spotLightClover[i].enabled = false;
                clover[i].enabled = false;
            }
            EffectManager.instance.effectSounds[48].source.Play();
            StartCoroutine(SpotLightOn(3));
            switches[12].bools = false;
        }
        if (switches[13].bools == true)
        {
            for (int i = 0; i < 4; i++)
            {
                spotLightClover[i].enabled = false;
                clover[i].enabled = false;
            }
            EffectManager.instance.effectSounds[48].source.Play();
            StartCoroutine(SpotLightOn(4));
            switches[13].bools = false;
        }
        if (switches[14].bools == true)
        {
            AchievementManager.instance.Unlock("NEW_ACHIEVEMENT_1_1");
            for (int i = 0; i < 5; i++)
            {
                spotLightClover[i].enabled = false;
                clover[i].enabled = false;
            }
            cards.enabled = false;
            EffectManager.instance.effectSounds[48].source.Play();
            StartCoroutine(SpotLightOn(5));
            switches[14].bools = false;
        }
        if (switches[15].bools == true)
        {
            for (int i = 0; i < 6; i++)
            {
                spotLightClover[i].enabled = false;
                clover[i].enabled = false;
            }
            EffectManager.instance.effectSounds[48].source.Play();
            StartCoroutine(SpotLightOn(6));
            StartCoroutine(ComboSound());

            //SoundManager.instance.bgmPlayer.clip = SoundManager.instance.bgmSounds[5].clip;
            //SoundManager.instance.bgmPlayer.Play();

            switches[15].bools = false;
        }
        if (switches[16].bools == true)
        {
            for (int i = 0; i < 7; i++)
            {
                spotLightClover[i].enabled = false;
                if (i != 6)
                    clover[i].enabled = false;
            }
            EffectManager.instance.effectSounds[48].source.Play();
            SoundManager.instance.ChangeBGM(10);
            switches[16].bools = false;
        }
        if (switches[17].bools == true)
        {
            Player.instance.rOpen = true;
            Player.instance.light2D.color = new Color(1, 0.92f, 0.745f);
            cards.texture = threeCards[1].texture;
            cards.enabled = true;
            rOpen.SetActive(true);
            switches[17].bools = false;
        }
        if (switches[18].bools == true)
        {
            StartCoroutine(SpotLightOnHeart());
            switches[18].bools = false;
        }
        if (switches[19].bools == true)
        {
            cards.texture = threeCards[0].texture;
            cards.enabled = true;
            switches[19].bools = false;
        }
        if (switches[20].bools == true)
        {
            cards.texture = threeCards[2].texture;
            cards.enabled = true;
            player.sOpen = true;
            sOpen.SetActive(true);
            //Player.instance.light2D.color = new Color(0.616f, 0.66f, 0.8f);
            player.light2D.color = new Color(0.55f, 0.61f, 0.81f);
            switches[20].bools = false;
        }
        if (switches[21].bools == true)
        {
            SoundManager.instance.ChangeBGM(12);
            switches[21].bools = false;
        }
        if (switches[22].bools == true)
        {
            StartCoroutine(SpotLightOnSpade(0));
            //SoundManager.instance.ChangeBGM(12);
            //SoundManager.instance.bgmPlayer.time = 140;
            switches[22].bools = false;
        }
        if (switches[23].bools == true)
        {
            for (int i = 0; i < 1; i++)
            {
                spotLightSpade[i].enabled = false;
                spade[i].enabled = false;
            }
            EffectManager.instance.effectSounds[48].source.Play();
            StartCoroutine(SpotLightOnSpade(1));
            switches[23].bools = false;
        }
        if (switches[24].bools == true)
        {
            for (int i = 0; i < 2; i++)
            {
                spotLightSpade[i].enabled = false;
                spade[i].enabled = false;
            }
            EffectManager.instance.effectSounds[48].source.Play();
            StartCoroutine(SpotLightOnSpade(2));
            switches[24].bools = false;
        }
        if (switches[25].bools == true)
        {
            for (int i = 0; i < 3; i++)
            {
                spotLightSpade[i].enabled = false;
                spade[i].enabled = false;
            }
            EffectManager.instance.effectSounds[48].source.Play();
            StartCoroutine(SpotLightOnSpade(3));
            switches[25].bools = false;
        }
        if (switches[26].bools == true)
        {
            for (int i = 0; i < 4; i++)
            {
                spotLightSpade[i].enabled = false;
                spade[i].enabled = false;
            }
            EffectManager.instance.effectSounds[48].source.Play();
            StartCoroutine(SpotLightOnSpade(4));
            switches[26].bools = false;
        }
        if (switches[27].bools == true)
        {
            AchievementManager.instance.Unlock("NEW_ACHIEVEMENT_1_3");
            for (int i = 0; i < 5; i++)
            {
                spotLightSpade[i].enabled = false;
                spade[i].enabled = false;
            }
            cards.enabled = false;
            EffectManager.instance.effectSounds[48].source.Play();
            StartCoroutine(SpotLightOnSpade(5));
            switches[27].bools = false;
        }
        if (switches[28].bools == true)
        {
            for (int i = 0; i < 5; i++)
            {
                spotLightSpade[i].enabled = false;
                spade[i].enabled = false;
            }
            spotLightSpade[5].enabled = false;
            EffectManager.instance.effectSounds[48].source.Play();
            SoundManager.instance.ChangeBGM(5);
            switches[28].bools = false;
        }
        if (switches[29].bools == true)
        {
            SoundManager.instance.ChangeBGM(13);
            switches[29].bools = false;
        }
        if (switches[30].bools == true)
        {
            player.animator.speed = 0.7f;
            door2.StartCoroutine(door2.DoorOpen(player.GetComponent<Collider2D>()));
            switches[30].bools = false;
        }
        if (switches[31].bools == true)
        {
            player.library = true;
            player.animator.speed = 0.5f;
            player.playerSpeed = player.slowSpeed * 2;
            LoadManager2.instance.cardUI.SetBool("appear", false);
            LoadManager2.instance.noise.SetActive(true);
            if (player.reloadImage1.enabled)
            {
                player.reloadImage1.enabled = false;
                player.reloadImage2.enabled = false;
            }
            if (EffectManager.instance.effectSounds[7].source.isPlaying)
                EffectManager.instance.effectSounds[7].source.Stop();
            switches[31].bools = false;
        }
        if (switches[32].bools == true)
        {
            LoadManager2.instance.noise.SetActive(false);
            player.floor.enabled = false;
            switches[32].bools = false;
        }
        if (switches[33].bools == true)
        {
            AchievementManager.instance.Unlock("NEW_ACHIEVEMENT_1_4");
            StartCoroutine(EndingCredit());
            SoundManager.instance.ChangeBGM(14);
            SoundManager.instance.bgmPlayer.time = 112.4f;
            SoundManager.instance.bgmPlayer.loop = false;
            switches[33].bools = false;
        }
        if (switches[34].bools == true)
        {
            AchievementManager.instance.Unlock("NEW_ACHIEVEMENT_1_2");
            heart[0].enabled = false;
            heart[1].enabled = true;
            cards.enabled = false;

            switches[34].bools = false;
        }
        if (switches[35].bools == true)
        {
            EffectManager.instance.effectSounds[22].source.volume = 0;
            switches[35].bools = false;
        }
        if (switches[36].bools == true)
        {
            if (PlayerPrefs.HasKey("EffectVolume"))
                EffectManager.instance.effectSounds[22].source.volume = PlayerPrefs.GetFloat("EffectVolume") * 0.3f;
            else
                EffectManager.instance.effectSounds[22].source.volume = 0.5f * 0.3f;
            switches[36].bools = false;
        }
        if (switches[37].bools == true)
        {
            AchievementManager.instance.Unlock("NEW_ACHIEVEMENT_1_0");
            switches[37].bools = false;
        }
    }

    IEnumerator SpotLightOn(int i)
    {
        if (i != 6)
            yield return new WaitForSeconds(2);
        else
        {
            wall[0].SetActive(true);
            wall[1].SetActive(true);
            yield return new WaitForSeconds(14);
            wall[0].SetActive(false);
            wall[1].SetActive(false);
        }
        EffectManager.instance.effectSounds[47].source.Play();
        spotLightClover[i].enabled = true;
        lineClover[i].enabled = true;
        if (i != 6)
            clover[i].enabled = true;
        else
            rWall.SetActive(false);

        yield return null;
    }
    IEnumerator SpotLightOnHeart()
    {
        yield return new WaitForSeconds(2);
        EffectManager.instance.effectSounds[47].source.Play();
        spotLightHeart[0].enabled = true;
        yield return new WaitForSeconds(2);
        EffectManager.instance.effectSounds[48].source.Play();
        spotLightHeart[0].enabled = false;
        yield return new WaitForSeconds(2);
        EffectManager.instance.effectSounds[47].source.Play();
        spotLightHeart[1].enabled = true;
        SoundManager.instance.ChangeBGM(8);
        rWall2.SetActive(false);
        //lineClover[i].enabled = true;

        yield return null;
    }
    IEnumerator SpotLightOnSpade(int i)
    {
        if (i == 0)
        {
            yield return new WaitForSeconds(2);
            rWall1.SetActive(false);
        }
        else
            yield return new WaitForSeconds(2);
        EffectManager.instance.effectSounds[47].source.Play();
        spotLightSpade[i].enabled = true;
        lineSpade[i].enabled = true;
        if (i != 5)
            spade[i].enabled = true;
        yield return null;
    }
    IEnumerator ComboSound()
    {
        yield return new WaitForSeconds(0.4f);
        EffectManager.instance.effectSounds[1].source.Play();
        EffectManager.instance.effectSounds[2].source.Play();
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < 4; i++)
        {
            EffectManager.instance.effectSounds[10].source.Play();
            EffectManager.instance.effectSounds[2].source.Play();
            yield return new WaitForSeconds(0.13f);
            if (i == 1)
                yield return new WaitForSeconds(0.08f);
        }
        yield return new WaitForSeconds(1);
        EffectManager.instance.effectSounds[1].source.Play();
        EffectManager.instance.effectSounds[2].source.Play();


        SoundManager.instance.ChangeBGM(5);

        yield return new WaitForSeconds(2);

        EffectManager.instance.effectSounds[49].source.Play();
        yield return new WaitForSeconds(0.3f);
        EffectManager.instance.effectSounds[50].source.Play();
        yield return new WaitForSeconds(0.8f);

        EffectManager.instance.effectSounds[51].source.Play();
        yield return new WaitForSeconds(0.3f);
        EffectManager.instance.effectSounds[52].source.Play();
        yield return new WaitForSeconds(0.3f);
        EffectManager.instance.effectSounds[53].source.Play();
        yield return new WaitForSeconds(0.9f);

        EffectManager.instance.effectSounds[54].source.Play();
        yield return new WaitForSeconds(0.3f);
        EffectManager.instance.effectSounds[55].source.Play();
        yield return new WaitForSeconds(1.5f);
        EffectManager.instance.effectSounds[56].source.Play();
        yield return new WaitForSeconds(2);
        EffectManager.instance.effectSounds[57].source.Play();

    }

    IEnumerator EndingCredit()
    {
        RawImage rawImage = ending.GetComponent<RawImage>();
        ending.SetActive(true);
        float vol = SoundManager.instance.bgmPlayer.volume;
        SoundManager.instance.bgmPlayer.volume = 0;
        float div = vol * Time.deltaTime;
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            SoundManager.instance.bgmPlayer.volume += div;
            yield return null;
        }
        SoundManager.instance.bgmPlayer.volume = vol;
        //yield return new WaitForSeconds(1f);
        endingCredit.SetActive(true);
        RectTransform rt = endingCredit.transform as RectTransform;
        while (rt.anchoredPosition.y < 1323)
        {
            rt.anchoredPosition += new Vector2(0, speed * Time.deltaTime);
            yield return null;
        }
        while (SoundManager.instance.bgmPlayer.time < 242)
            yield return null;
        yield return new WaitForSeconds(1f);
        while (rawImage.color.r > 0)
        {
            Color tmp = rawImage.color;
            tmp -= new Color(Time.deltaTime * 0.3f, Time.deltaTime * 0.3f, Time.deltaTime * 0.3f, 0);
            rawImage.color = tmp;
            yield return null;
        }
        yield return new WaitForSeconds(3);
        player.OnClickHome();
    }
    void func1()
    {
        sprite1.enabled = true;
    }






    /*
    public void ShowCardA(int A)
    {
        //twoSideCards[A].GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, twoSideCards[A].GetComponent<Transform>().rotation.z * 360);
        twoSideCards[A].GetComponent<RawImage>().enabled = true;
        twoSideCards[A].GetComponent<Button>().enabled = true;

    }
    
    public void OnClick()
    {
        StartCoroutine(ChangeCardAtoB());
    }

    public IEnumerator ChangeCardAtoB()
    {
        twoSideCards[A].GetComponent<Button>().enabled = false;
        EffectManager.instance.effectSounds[25].source.Play();
        twoSideCards[A].GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
        while (twoSideCards[A].GetComponent<Transform>().rotation.y <= 0.5)
        {
            twoSideCards[A].GetComponent<Transform>().Rotate(0, 720 * Time.deltaTime, 0);
            yield return null;
        }
        twoSideCards[A].enabled = false;
        twoSideCards[B].GetComponent<Transform>().rotation = Quaternion.Euler(0, 90, 0);
        twoSideCards[B].GetComponent<Button>().enabled = false;
        twoSideCards[B].enabled = true;

        while (twoSideCards[B].GetComponent<Transform>().rotation.y >= 0)
        {
            twoSideCards[B].GetComponent<Transform>().Rotate(0, -720 * Time.deltaTime, 0);
            yield return null;
        }
        yield return new WaitForSeconds(0.7f);
        if (B == 0) EffectManager.instance.effectSounds[37].source.Play();
        else if (B == 1) EffectManager.instance.effectSounds[36].source.Play();
        yield return new WaitForSeconds(2.3f);
        //twoSideCards[B].GetComponent<Button>().enabled = true;
        clicked = true;
    }
    */
}