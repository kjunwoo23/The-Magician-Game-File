using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy2 : MonoBehaviour
{
    public int hp;
    int fullHp;
    public bool dmg;
    public float slowSpeed;
    public float fastSpeed;
    public float confSpeed;
    public SpriteRenderer sprite;
    public Animator animator;
    bool flag = false;
    //public Transform target;
    Dummy dummy;
    public float restTime;
    public float dmgTime;
    public RawImage rawImage;
    public Image image;
    public Text text;
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        fullHp = hp;
        animator.SetTrigger("awake");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.instance.transform.position.y > transform.position.y + 5 || Player.instance.transform.position.y < transform.position.y - 5) return;

        if (dummy == null)
            dummy = Player.instance.dummy;

        animator.speed = 1 / Player.instance.BWJoker;

        if (Player.instance.guideOn == true)
        {
            if (canvas.enabled == false)
                canvas.enabled = true;
        }
        else if (canvas.enabled == true)
            canvas.enabled = false;

        text.text = hp.ToString();
        if (hp <= 0) text.enabled = false;
        else text.enabled = true;

        if (Player.instance.alternative && Player.instance.isDisguise && flag == false)
        {
            StartCoroutine("Confusion");
            animator.SetBool("panic", true);
        }
        else if (Player.instance.alternative && !Player.instance.isDisguise)
        {
            StopCoroutine("Confusion");
            animator.SetBool("panic", false);
        }
    }
    private void FixedUpdate()
    {
        if (Player.instance.transform.position.y > transform.position.y + 5 || Player.instance.transform.position.y < transform.position.y - 5) return;
        if (dummy == null)
            dummy = Player.instance.dummy;

        if (!animator.GetBool("panic") && dmg)
        {
            if (dummy.enabled == false)
                dummy.transform.position = Player.instance.transform.position;
            if (hp > 0)
            {
                if (transform.position.x < dummy.transform.position.x)
                {
                    transform.localScale = new Vector3(-0.9f, 0.9f, 0);
                    transform.position += new Vector3(fastSpeed * Time.fixedDeltaTime / Player.instance.BWJoker, 0, 0);
                    text.transform.localScale = new Vector3(-0.01f, 0.01f, 0);
                }
                if (transform.position.x > dummy.transform.position.x)
                {
                    transform.localScale = new Vector3(0.9f, 0.9f, 0);
                    transform.position -= new Vector3(fastSpeed * Time.fixedDeltaTime / Player.instance.BWJoker, 0, 0);
                    text.transform.localScale = new Vector3(0.01f, 0.01f, 0);
                }
            }
            else
                StartCoroutine(Rest());
        }
    }
    IEnumerator Rest()
    {
        Color a = sprite.color;
        rawImage.enabled = true;
        image.enabled = true;
        animator.SetBool("back", true);
        sprite.color = new Color(a.r, a.g, a.b, 0.7f);
        dmgTime = restTime;
        dmg = false;
        for (; dmgTime > 0; dmgTime -= Time.deltaTime / Player.instance.BWJoker)
        {
            image.fillAmount = (restTime - dmgTime) / restTime;
            yield return null;
        }
        hp = fullHp;
        dmg = true;
        sprite.color = new Color(a.r, a.g, a.b, 1);
        animator.SetBool("back", false);
        rawImage.enabled = false;
        image.enabled = false;
    }


    IEnumerator Confusion()
    {
        flag = true;
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            transform.position -= new Vector3(confSpeed * 3 * Time.deltaTime, 0, 0);
            yield return new WaitForSeconds(0.02f);
            transform.position += new Vector3(confSpeed * 3 * Time.deltaTime, 0, 0);
        }
    }
}
