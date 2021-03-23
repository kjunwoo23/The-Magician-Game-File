using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hacking : MonoBehaviour
{
    public Slider slider;
    public Text text;
    public bool onTrigger = false;
    public float hacking;
    public float maxHacking;
    public float postPoint;

    public GameObject[] enemy;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            onTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            onTrigger = false;
            Player.instance.animator.SetBool("hacking", false);
            Player.instance.isHacking = false;
            EffectManager.instance.effectSounds[6].source.Pause();
            if (!Player.instance.animator.GetCurrentAnimatorStateInfo(0).IsName("lightoff")
                && !Player.instance.animator.GetCurrentAnimatorStateInfo(0).IsName("disguise")
                && !Player.instance.animator.GetCurrentAnimatorStateInfo(0).IsName("door"))
            {
                Player.instance.skill = false;
                //Player.instance.myRigid.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }

  

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > Player.instance.transform.position.y + 5 || transform.position.y < Player.instance.transform.position.y - 5) return;

        if (0.1f < hacking && hacking < 0.2f)
        {
            for(int i = 0; i < enemy.Length; i++)
            {
                if (enemy[i].GetComponent<Enemy>() != null)
                    if (!enemy[i].GetComponent<Enemy>().enabled)
                        enemy[i].GetComponent<Enemy>().enabled = true;
                if (enemy[i].GetComponent<Enemy2>() != null)
                    if (!enemy[i].GetComponent<Enemy2>().enabled)
                        enemy[i].GetComponent<Enemy2>().enabled = true;
                if (enemy[i].GetComponent<Enemy3>() != null)
                    if (!enemy[i].GetComponent<Enemy3>().enabled)
                        enemy[i].GetComponent<Enemy3>().enabled = true;
                if (enemy[i].GetComponent<Enemy4>() != null)
                    if (!enemy[i].GetComponent<Enemy4>().enabled)
                        enemy[i].GetComponent<Enemy4>().enabled = true;

                //if (!enemy[i].GetComponent<BoxCollider2D>().enabled)
                    //enemy[i].GetComponent<BoxCollider2D>().enabled = true;
            }
        }

        if (onTrigger == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && !Player.instance.gameover)
            {
                Player.instance.animator.SetBool("walking", false);
                Player.instance.animator.SetBool("hacking", true);
                Player.instance.isHacking = true;
                Player.instance.skill = true;
                //Player.instance.myRigid.constraints = RigidbodyConstraints2D.FreezePosition|RigidbodyConstraints2D.FreezeRotation;
                EffectManager.instance.effectSounds[6].source.Play();
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                Player.instance.animator.SetBool("hacking", false);
                Player.instance.isHacking = false;
                EffectManager.instance.effectSounds[6].source.Pause();
                if (!Player.instance.animator.GetCurrentAnimatorStateInfo(0).IsName("lightoff")
                    && !Player.instance.animator.GetCurrentAnimatorStateInfo(0).IsName("disguise")
                    && !Player.instance.animator.GetCurrentAnimatorStateInfo(0).IsName("door"))
                {
                    Player.instance.skill = false;
                    //Player.instance.myRigid.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                }
            }
        }
        else if (Player.instance.isHacking || Player.instance.animator.GetBool("hacking"))
        {
            Player.instance.animator.SetBool("hacking", false);
            Player.instance.isHacking = false;
            Player.instance.skill = false;
            //Player.instance.myRigid.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            EffectManager.instance.effectSounds[6].source.Pause();
        }
        if (Player.instance.isHacking == true)
        {
            if (Player.instance.animator.GetBool("disguise") && !Player.instance.fade.enabled)
            {
                Player.instance.StopCoroutine("Disguise");
                Player.instance.animator.SetBool("disguise", false);
                Player.instance.disguiseCur = Player.instance.disguiseCool;
                Player.instance.alpha = Player.instance.sprite.color;
                Player.instance.alpha.a = 1;
                Player.instance.sprite.color = Player.instance.alpha;
                Player.instance.isDisguise = false;
                EffectManager.instance.effectSounds[12].source.Stop();
                Dummy.instance.DestroyDummy();
            }
            if (hacking >= maxHacking)
                hacking = maxHacking;
            else if (hacking < postPoint * maxHacking || PostItManager.instance.pass)
            {
                if (!PostItManager.instance.post && !Player.instance.barricade)
                    hacking += Time.deltaTime;
            }
            else if ((hacking >= postPoint * maxHacking || !PostItManager.instance.pass) && !PostItManager.instance.consolAnimator.GetBool("consol"))
            {
                PostItManager.instance.post = true;
                hacking = (maxHacking * postPoint);
            }

        }
        if (hacking > 0 && !EffectManager.instance.effectSounds[20].source.isPlaying)
            EffectManager.instance.effectSounds[20].source.Play();
        /*
        if (hacking >= 0.5f * maxHacking && !EffectManager.instance.effectSounds[21].source.isPlaying)
        {
            EffectManager.instance.effectSounds[21].source.Play();
            EffectManager.instance.effectSounds[20].source.Stop();
        }
        */
        if (hacking == maxHacking && !EffectManager.instance.effectSounds[22].source.isPlaying)
        {
            if (!Player.instance.gameover)
                EffectManager.instance.effectSounds[22].source.Play();
            EffectManager.instance.effectSounds[20].source.Stop();
        }
        if (text.text != 100 * hacking / maxHacking + "%")
            text.text = 100 * hacking / maxHacking + "%";
        slider.value = hacking / maxHacking;
    }
}
