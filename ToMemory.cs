using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToMemory : MonoBehaviour
{
    public int type;
    public float y;
    public int bgm;
    public Animator cardUI;
    public Transform toHere;

    public Animator animator;
    public RawImage panel;
    public int cardClickMax;
    int cardClick;
    public int changeCardtypeA, changeCardtypeB;
    float time = 0f;
    float F_time = 1f;
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!enabled) return;
            if (Player.instance.isDisguise && !Player.instance.fade.enabled)
            {
                Player.instance.StopCoroutine("Disguise");
                Player.instance.disguiseCur = Player.instance.disguiseCool;
                Player.instance.alpha = Player.instance.sprite.color;
                Player.instance.alpha.a = 1;
                Player.instance.sprite.color = Player.instance.alpha;
                Player.instance.isDisguise = false;
                EffectManager.instance.effectSounds[12].source.Stop();
                Dummy.instance.DestroyDummy();
            }
            if (type == 0)
            {
                SwitchManager.instance.A = changeCardtypeA;
                SwitchManager.instance.B = changeCardtypeB;
                cardUI.SetBool("appear", false);
                Player.instance.isMemory = true;
                Player.instance.playerSpeed = Player.instance.defaultSpeed;
                Player.instance.animator.SetBool("walking", false);
                Player.instance.enabled = false;
                StartCoroutine(FadeOut());
                enabled = false;
            }
            else if (type == 1)
            {
                SwitchManager.instance.A = changeCardtypeA;
                SwitchManager.instance.B = changeCardtypeB;
                Player.instance.animator.SetBool("walking", false);
                Player.instance.enabled = false;
                StartCoroutine(FadeOut());
                //toHere.GetComponent<ToMemory>().enabled = false;

            }
        }
    }
    
    public IEnumerator FadeOut()
    {
        cardClick = 0;
        time = 0;
        Color alpha = panel.color;
        while (alpha.a < 1f) //페이드 아웃
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            panel.color = alpha;
            yield return null;
        }

        if (type == 0)
            MemoryStart();
        if (type == 1)
            MemoryEnd();
        yield return new WaitForSeconds(1f);
        animator.SetBool("Appear", true);
        if (type == 0)
        {
            EffectManager.instance.effectSounds[24].source.Play();
            if (bgm != -1)
                if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmSounds[bgm].clip)
                    SoundManager.instance.bgmPlayer.Pause();
        }
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            yield return null;
            if (SwitchManager.instance.clicked)
            {
                cardClick++;
                SwitchManager.instance.clicked = false;
            }
            if (cardClick == cardClickMax)
            {
                break;
            }
        }
        animator.SetBool("Appear", false);
        if (type == 1)
        {
            EffectManager.instance.effectSounds[24].source.Play();
            if (bgm != -1)
                if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmSounds[bgm].clip)
                    SoundManager.instance.bgmPlayer.Pause();
        }
        yield return new WaitForSeconds(1f);
        time = 0;
        if (bgm != -1)
            if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmSounds[bgm].clip)
            {
                SoundManager.instance.bgmPlayer.clip = SoundManager.instance.bgmSounds[bgm].clip;
                SoundManager.instance.bgmPlayer.Play();
            }
        while (alpha.a > 0f) //페이드 인
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            panel.color = alpha;
            yield return null;
        }
        Player.instance.enabled = true;
        if (type == 1) Player.instance.isMemory = false;
        yield return null;
    }

    public void MemoryStart()
    {
        SwitchManager.instance.ShowCardA(changeCardtypeA);
        Player.instance.GetComponent<SpriteRenderer>().enabled = false;
        Player.instance.playerSpeed = 2.5f;
        Player.instance.transform.position -= new Vector3(0, y, 0);

    }

    public void MemoryEnd()
    {
        cardUI.SetBool("appear", true);
        SwitchManager.instance.ShowCardA(changeCardtypeA);
        Player.instance.playerSpeed = 4;
        Player.instance.GetComponent<SpriteRenderer>().enabled = true;
        Player.instance.transform.position = new Vector3(toHere.position.x, Player.instance.transform.position.y + y, 0);
    }

    */




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
