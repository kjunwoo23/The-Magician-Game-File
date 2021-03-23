using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class Player : MonoBehaviour
{
    public static Player instance;
    
    public bool isMemory;
    public bool skill = false;
    public float playerSize;
    public float playerSpeed, slowSpeed, defaultSpeed;
    public bool isWalk;
    public Animator animator;
    public Rigidbody2D myRigid;
    public Vector2 MousePosition;
    public Camera cam;
    public CinemachineVirtualCamera cm;
    public Animator EscDown;
    public Animator EscUp;
    public GameObject card0, card1;
    public Dummy dummy;
    public Transform cardShoot;
    public float cardCooltime0, cardCooltime1, cardCurtime0 = 0, cardCurtime1 = 0;
    public bool comboIng = false;
    public bool isPause = false, paused = false;
    public int deck;
    public bool reload = false;
    public float reloadCool, reloadCurrent;
    public Transform atkPos;
    public Vector2 atkSize;
    public float knock;
    public float dmgcool, dmgcoolMax;
    public int hits;
    public bool isHacking = false;
    public RawImage fade;
    public Animator walking;
    public SpriteRenderer sprite;
    public Color alpha;
    public bool isDisguise = false;
    public bool alternative = false;
    //public bool noR;
    public bool library;
    public Text cardNum;
    public Slider slider;
    public float disguiseCool, disguiseCur;
    public Image disguiseCoolImage;
    Color disguiseCoolImageColor;
    public bool leftWall, rightWall;
    public RawImage blur;
    public bool jokerOn;
    public float BWJoker;
    public float bgmTime;
    public Animator joker;
    public GameObject tarotMagician;
    public bool guideOn;
    public Animator guideCard;
    public Text guideText;
    public SpriteRenderer gas;
    public Light2D light2D;
    public ShadowCaster2D shadow;
    public Volume globalVolume;
    public bool quitable;
    public Animator bluePrint;
    public bool gameover;
    public GameObject rope1, rope2, rope3, gameoverScene;
    public SpriteRenderer reloadImage1, reloadImage2;
    int half, nowhalf;
    public bool sOpen, rOpen;
    public Text floor;
    Vector2 stopMouse;
    public bool barricade;
    public bool toNext;
    bool soundFilter;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        reloadCurrent = reloadCool;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "LeftWall")
            leftWall = true;
        else if (collision.collider.tag == "RightWall")
            rightWall = true;
        if (collision.collider.tag == "Barricade")
        {
            animator.SetBool("hacking", false);
            isHacking = false;
            barricade = true;
            EffectManager.instance.effectSounds[6].source.Stop();
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "LeftWall")
            leftWall = false;

        else if (collision.collider.tag == "RightWall")
            rightWall = false;
        if (collision.collider.tag == "Barricade")
            barricade = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    { 
        if (collision.collider.tag == "Barricade")
        {
            animator.SetBool("hacking", false);
            isHacking = false;
            barricade = true;
            EffectManager.instance.effectSounds[6].source.Stop();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerPrefs.GetInt("Cheat") == 1)
            return;
        if (collision.tag == "Enemy" && isDisguise == false)
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(atkPos.position, atkSize, 0);
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.tag == "Enemy")
                {
                    if (collider.GetComponent<Enemy>() != null) if (collider.GetComponent<Enemy>().enabled == false) return;
                    if (collider.GetComponent<Enemy2>() != null) if (collider.GetComponent<Enemy2>().enabled == false) return;
                    if (collider.GetComponent<Enemy3>() != null) if (collider.GetComponent<Enemy3>().enabled == false) return;
                    if (collider.GetComponent<Enemy4>() != null) if (collider.GetComponent<Enemy4>().enabled == false) return;

                    if (collider.GetComponent<Enemy2>() != null) if (!collider.GetComponent<Enemy2>().dmg) return;
                    if (collider.GetComponent<Enemy3>() != null) if (!collider.GetComponent<Enemy3>().dmg) return;
                    //collider.GetComponent<Animator>().SetTrigger("dmg");
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("lightoff")
                        || animator.GetCurrentAnimatorStateInfo(0).IsName("disguise")
                        || animator.GetCurrentAnimatorStateInfo(0).IsName("door"))
                        return;
                    if ((isHacking || !skill) && dmgcool < 0)
                    {
                        if (deck == 0)
                        {
                            //Debug.Log("GameOver");
                            StartCoroutine(GameOver());

                        }
                        if (collider.transform.position.x > transform.position.x)
                        {
                            collider.GetComponent<Rigidbody2D>().velocity += new Vector2(knock * 2, 0);
                            StartCoroutine(Damaged(true));
                            myRigid.velocity -= new Vector2(knock, 0);
                        }
                        if (collider.transform.position.x < transform.position.x)
                        {
                            collider.GetComponent<Rigidbody2D>().velocity -= new Vector2(knock * 2, 0);
                            StartCoroutine(Damaged(false));
                            myRigid.velocity += new Vector2(knock, 0);
                        }
                        animator.SetTrigger("damaged");
                        collider.GetComponent<Animator>().SetTrigger("dmg");
                        Invoke("DamagedSound", 0.27f);
                        if (isHacking || reload)
                        {
                            animator.SetBool("hacking", false);
                            isHacking = false;
                            reload = false;
                            skill = false;
                            if (deck > 10) deck -= 10;
                            else deck = 0;
                            /*
                            if (deck > 15) deck -= 15;
                            else deck = 0;*/
                            EffectManager.instance.effectSounds[6].source.Pause();
                            EffectManager.instance.effectSounds[7].source.Stop();
                            reloadCurrent = reloadCool;
                        }
                        else
                        {
                            if (deck > 10) deck -= 10;
                            else deck = 0;
                        }
                        if (collider.GetComponent<Enemy4>() != null)
                            collider.GetComponent<Enemy4>().animator.SetTrigger("attack");
                        dmgcool = dmgcoolMax;
                    }
                    if (collider.GetComponent<Enemy>() != null)
                        collider.GetComponent<Enemy>().curTime = 0;
                    else if (collider.GetComponent<Enemy3>() != null)
                        collider.GetComponent<Enemy3>().curTime = 0;
                }
            }
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (PlayerPrefs.GetInt("Cheat") == 1)
            return;
        if (collision.tag == "Enemy" && isDisguise == false)
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(atkPos.position, atkSize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.tag == "Enemy")
                    {
                        if (collider.GetComponent<Enemy>() != null) if (collider.GetComponent<Enemy>().enabled == false) return;
                        if (collider.GetComponent<Enemy2>() != null) if (collider.GetComponent<Enemy2>().enabled == false) return;
                        if (collider.GetComponent<Enemy3>() != null) if (collider.GetComponent<Enemy3>().enabled == false) return;
                        if (collider.GetComponent<Enemy4>() != null) if (collider.GetComponent<Enemy4>().enabled == false) return;

                        if (collider.GetComponent<Enemy2>() != null) if (!collider.GetComponent<Enemy2>().dmg) return;
                        if (collider.GetComponent<Enemy3>() != null) if (!collider.GetComponent<Enemy3>().dmg) return;
                        //collider.GetComponent<Animator>().SetTrigger("dmg");
                        if (animator.GetCurrentAnimatorStateInfo(0).IsName("lightoff")
                               || animator.GetCurrentAnimatorStateInfo(0).IsName("disguise")
                               || animator.GetCurrentAnimatorStateInfo(0).IsName("door"))
                            return;
                        if ((isHacking || !skill) && dmgcool < 0)
                        {
                            if (deck == 0)
                            {
                                //Debug.Log("GameOver");
                                StartCoroutine(GameOver());

                            }
                            if (collider.transform.position.x > transform.position.x)
                            {
                                collider.GetComponent<Rigidbody2D>().velocity += new Vector2(knock * 2, 0);
                                StartCoroutine(Damaged(true));
                                myRigid.velocity -= new Vector2(knock, 0);
                            }
                            if (collider.transform.position.x < transform.position.x)
                            {
                                collider.GetComponent<Rigidbody2D>().velocity -= new Vector2(knock * 2, 0);
                                StartCoroutine(Damaged(false));
                                myRigid.velocity += new Vector2(knock, 0);
                            }
                            animator.SetTrigger("damaged");
                            collider.GetComponent<Animator>().SetTrigger("dmg");
                            Invoke("DamagedSound", 0.27f);
                            if (isHacking || reload)
                            {
                                animator.SetBool("hacking", false);
                                isHacking = false;
                                reload = false;
                                skill = false;
                                if (deck > 10) deck -= 10;
                                else deck = 0;
                                EffectManager.instance.effectSounds[6].source.Pause();
                                EffectManager.instance.effectSounds[7].source.Stop();
                                reloadCurrent = reloadCool;
                                /*
                                if (deck > 15) deck -= 15;
                                else deck = 0;*/
                            }
                            else
                            {
                                if (deck > 10) deck -= 10;
                                else deck = 0;
                            }
                            if (collider.GetComponent<Enemy4>() != null)
                                collider.GetComponent<Enemy4>().animator.SetTrigger("attack");
                            dmgcool = dmgcoolMax;
                        }
                        if (collider.GetComponent<Enemy>() != null)
                            collider.GetComponent<Enemy>().curTime = 0;
                        else if (collider.GetComponent<Enemy3>() != null)
                            collider.GetComponent<Enemy3>().curTime = 0;
                    }
                }
                //isHacking = false;
            }
    }

    IEnumerator Damaged(bool right)
    {
        for (float i = 0; i < 0.3f; i += Time.deltaTime)
        {
            if (right && transform.localScale.x < 0) sprite.flipX = true;
            if (!right && transform.localScale.x > 0) sprite.flipX = false;
            yield return null;
        }
        sprite.flipX = false;
    }

    public void DamagedSound()
    {
        EffectManager.instance.effectSounds[9].source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L)) OnClickLang();
        //Debug.Log(Time.deltaTime);
        /*if (Input.GetKeyDown(KeyCode.P))
        {
            sOpen = true;
            rOpen = true;
        }*/
        if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            if (bluePrint.GetBool("appear"))
            {
                EffectManager.instance.effectSounds[43].source.Play();
                bluePrint.SetBool("appear", false);
            }
            else
                StartCoroutine("IsPause");
        }
        if (gameover) return;
        if (Input.GetKeyDown(KeyCode.T))
            if (!bluePrint.GetBool("appear"))
                OnClickBluePrint();
            else
            {
                EffectManager.instance.effectSounds[43].source.Play();
                bluePrint.SetBool("appear", false);
            }
        if (isPause && !paused) return;

        //if (Input.GetKeyDown(KeyCode.K))
            //StartCoroutine(GameOver());

        if (fade.enabled && cardNum.color != Color.red)
            cardNum.color = Color.red;
        else if (alternative && !fade.enabled && cardNum.color != Color.gray && deck != 0)
            cardNum.color = Color.gray;
        else if (!alternative && cardNum.color != Color.white && deck != 0)
            cardNum.color = Color.white;
        else if (!fade.enabled && cardNum.color != Color.yellow && deck == 0)
            cardNum.color = Color.yellow;
        if (dmgcool >= 0)
            dmgcool -= Time.deltaTime;
        if (animator.GetBool("disguise"))
        {
            disguiseCoolImage.fillAmount += Time.deltaTime / 5.0f;
        }
        else if (disguiseCur >= 0)
        {
            disguiseCur -= Time.deltaTime;
            disguiseCoolImageColor = disguiseCoolImage.color;
            disguiseCoolImageColor.a = disguiseCur / disguiseCool;
            disguiseCoolImage.color = disguiseCoolImageColor;
        }
        MousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
  //여기
        if (!DialogueManager.instance.animator.GetBool("Window"))
        {
            if (MousePosition.x > transform.position.x + 0.5 * playerSize)
                nowhalf = 1;
            else if (MousePosition.x < transform.position.x - 0.5 * playerSize)
                nowhalf = -1;
            if (nowhalf != half || !animator.GetCurrentAnimatorStateInfo(0).IsName("New State"))
            {
                if (MousePosition.x > transform.position.x + 0.5 * playerSize)
                {
                    transform.localScale = new Vector3(1, 1, 1) * playerSize;
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("walking") && !animator.GetCurrentAnimatorStateInfo(0).IsName("DisguiseWalking"))
                        sprite.flipX = false;
                }
                if (MousePosition.x < transform.position.x - 0.5 * playerSize)
                {
                    transform.localScale = new Vector3(-1, 1, 1) * playerSize;
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("walking") && !animator.GetCurrentAnimatorStateInfo(0).IsName("DisguiseWalking"))
                        sprite.flipX = false;
                }
            }
        }
        if (sprite.flipX)
            shadow.transform.localScale = new Vector3(-1, 1, 1);
        else
            shadow.transform.localScale = new Vector3(1, 1, 1);



        if (MousePosition.x > transform.position.x + 0.5 * playerSize)
            half = 1;
        else if (MousePosition.x < transform.position.x - 0.5 * playerSize)
            half = -1;
        if (library)
        {
            if (animator.speed != 0.5f && !skill)
                animator.speed = 0.5f;
            return;
        }
        //stopMouse = Input.mousePosition;

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed = slowSpeed;
            if (isDisguise && !fade.enabled && !gameover)
            {
                StopDisguise();
                /*
                StopCoroutine("Disguise");
                animator.SetBool("disguise", false);
                disguiseCur = disguiseCool;
                alpha = sprite.color;
                alpha.a = 1;
                sprite.color = alpha;
                isDisguise = false;
                EffectManager.instance.effectSounds[12].source.Stop();
                Dummy.instance.DestroyDummy();*/
            }
            //walking.speed = 0.5f;
        }
        else
        {
            walking.speed = 0.7f;
            playerSpeed = defaultSpeed;
        }
        if (reload == true && !gameover)
        {
            if (!reloadImage1.enabled)
            {
                reloadImage1.enabled = true;
                reloadImage2.enabled = true;
            }
            if (!fade.enabled)
            {
                if (reloadCurrent < reloadCool * 0.5f)
                    reloadImage2.transform.transform.rotation = Quaternion.Euler(0, 0, reloadCurrent / reloadCool * 360 * 2);
                else
                    reloadImage2.transform.transform.rotation = Quaternion.Euler(0, 0, 0);
                if (deck < 52)
                {
                    reloadCurrent -= Time.deltaTime;
                    if (reloadCurrent < 0)
                    {
                        deck += 3;
                        PlayerPrefs.SetInt("ReloadRec", PlayerPrefs.GetInt("ReloadRec") + 3);
                        if (PlayerPrefs.GetInt("ReloadRec") >= 333)
                            AchievementManager.instance.Unlock("NEW_ACHIEVEMENT_1_6");
                        reloadCurrent = reloadCool;
                    }
                }
                else
                    reloadImage2.transform.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else if (reloadImage1.enabled)
        {
            reloadImage1.enabled = false;
            reloadImage2.enabled = false;
        }
        if (deck > 52) deck = 52;
        if (deck < 0) deck = 0;

        if (Input.GetKeyDown(KeyCode.LeftShift) && !fade.enabled)
        {
            reload = true;
            reloadCurrent = reloadCool;
            EffectManager.instance.effectSounds[7].source.Play();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            reload = false;
            EffectManager.instance.effectSounds[7].source.Stop();
        }
        if (Input.GetKeyDown(KeyCode.R) && rOpen && !skill && deck > 0 && !isHacking && !alternative && !isDisguise)
        {
            alternative = true;
            StartCoroutine("LightOff");
        }
        if (Input.GetKeyDown(KeyCode.Q) && !skill && deck > 0 && !isDisguise && !isHacking && disguiseCur < 0)
        {
            animator.SetTrigger("disguisestart");
            StartCoroutine("Disguise");
        }
        if (Input.GetKeyDown(KeyCode.S) && sOpen && !jokerOn)
            StartCoroutine("Joker");
        if (isDisguise == true)
        {
            EffectManager.instance.effectSounds[12].source.pitch += Time.deltaTime;
        }

        if (skill == false)
        {
            if (Input.GetMouseButton(0))
            {
                if (cardCurtime0 <= 0 && comboIng == false && deck > 0 && reload == false && !PostItManager.instance.GetComponent<Animator>().GetBool("post"))
                {
                    animator.SetTrigger("cardthrow");
                    if (hits != 3)
                    {
                        hits++;
                        if (!(isDisguise && fade.enabled)) deck--;
                        EffectManager.instance.effectSounds[1].source.Play();
                        if (MousePosition.x > transform.position.x + 0.5 * playerSize)
                            Instantiate(card0, cardShoot.position, Quaternion.Euler(0, 0, -5.7f));
                        if (MousePosition.x < transform.position.x - 0.5 * playerSize)
                            Instantiate(card0, cardShoot.position, Quaternion.Euler(0, 180, -5.7f));
                        cardCurtime0 = cardCooltime0;
                    }
                    else if (hits == 3)
                    {
                        hits = 1;
                        if (!(isDisguise && fade.enabled)) deck--;
                        EffectManager.instance.effectSounds[10].source.Play();
                        if (MousePosition.x > transform.position.x + 0.5 * playerSize)
                            Instantiate(card1, cardShoot.position, Quaternion.Euler(0, 0, -5.7f));
                        if (MousePosition.x < transform.position.x - 0.5 * playerSize)
                            Instantiate(card1, cardShoot.position, Quaternion.Euler(0, 180, -5.7f));
                        cardCurtime0 = cardCooltime0 * 0.5f;
                    }
                }
            }
            if (Input.GetMouseButton(1))
            {
                if (cardCurtime1 <= 0 && deck > 0 && reload == false && !PostItManager.instance.GetComponent<Animator>().GetBool("post"))
                {
                    StartCoroutine(Combo());
                    cardCurtime1 = cardCooltime1;
                }
            }
        }
        cardCurtime0 -= Time.deltaTime;
        cardCurtime1 -= Time.deltaTime;

        if (isHacking && isDisguise && !fade.enabled)
        {
            StopDisguise();
        }
    }
    private void FixedUpdate()
    {
        if (gameover) return;
        if (isPause && !paused) return;

        isWalk = (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) && !(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A));
        if (!skill)
        {
            if (Input.GetKey(KeyCode.D) && !rightWall)
            {
                animator.SetBool("walking", true);
                transform.position += new Vector3(1, 0, 0) * playerSpeed * Time.fixedDeltaTime;
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("walking") || animator.GetCurrentAnimatorStateInfo(0).IsName("DisguiseWalking"))
                {
                    if (MousePosition.x < transform.position.x + 0.5 * playerSize && !DialogueManager.instance.animator.GetBool("Window"))
                        sprite.flipX = true;
                    else
                        sprite.flipX = false;
                }
                else
                    sprite.flipX = false;
            }
            if (Input.GetKey(KeyCode.A) && !leftWall)
            {
                animator.SetBool("walking", true);
                transform.position += new Vector3(-1, 0, 0) * playerSpeed * Time.fixedDeltaTime;
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("walking") || animator.GetCurrentAnimatorStateInfo(0).IsName("DisguiseWalking"))
                {
                    if (MousePosition.x > transform.position.x + 0.5 * playerSize && !DialogueManager.instance.animator.GetBool("Window"))
                        sprite.flipX = true;
                    else
                        sprite.flipX = false;
                }
                else
                    sprite.flipX = false;
            }
            if (!(Input.GetKey(KeyCode.D) && !rightWall) && !(Input.GetKey(KeyCode.A) && !leftWall) || (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)))
            {
                animator.SetBool("walking", false);

            }
        }
    }
    public void StopDisguise()
    {
        StopCoroutine("Disguise");
        animator.SetBool("disguise", false);
        disguiseCur = disguiseCool;
        alpha = sprite.color;
        alpha.a = 1;
        sprite.color = alpha;
        isDisguise = false;
        EffectManager.instance.effectSounds[12].source.Stop();
        Dummy.instance.DestroyDummy();
    }

    IEnumerator Combo()
    {
        comboIng = true;
        if (deck > 0 && !(isDisguise && fade.enabled)) deck--;
        EffectManager.instance.effectSounds[1].source.Play();
        animator.SetTrigger("cardthrow2");
        if (MousePosition.x > transform.position.x + 0.5 * playerSize)
            Instantiate(card0, cardShoot.position, Quaternion.Euler(0, 0, -5.7f));
        if (MousePosition.x < transform.position.x - 0.5 * playerSize)
            Instantiate(card0, cardShoot.position, Quaternion.Euler(0, 180, -5.7f));
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < 4; i++)
        {
            if (deck > 0 && !(isDisguise && fade.enabled)) deck--;
            EffectManager.instance.effectSounds[10].source.Play();
            if (MousePosition.x > transform.position.x + 0.5 * playerSize)
                Instantiate(card1, cardShoot.position, Quaternion.Euler(0, 0, -5.7f));
            if (MousePosition.x < transform.position.x - 0.5 * playerSize)
                Instantiate(card1, cardShoot.position, Quaternion.Euler(0, 180, -5.7f));
            yield return new WaitForSeconds(0.13f);
            if (i == 1)
                yield return new WaitForSeconds(0.08f);
        }
        comboIng = false;
    }
    public IEnumerator LightOff()
    {
        animator.SetTrigger("lightoff");
        skill = true;
        animator.SetBool("walking", false);
        myRigid.constraints = RigidbodyConstraints2D.FreezePosition|RigidbodyConstraints2D.FreezeRotation;


        yield return new WaitForSeconds(0.2f);
        EffectManager.instance.effectSounds[31].source.Play();
        yield return new WaitForSeconds(0.2f);
        EffectManager.instance.effectSounds[32].source.Play();
        yield return new WaitForSeconds(0.3f);
        EffectManager.instance.effectSounds[33].source.Play();
        yield return new WaitForSeconds(0.3f);
        EffectManager.instance.effectSounds[34].source.Play();
        yield return new WaitForSeconds(1.0f);
        EffectManager.instance.effectSounds[35].source.Play();
        yield return new WaitForSeconds(1.0f);


        SoundManager.instance.bgmPlayer.volume *= 0.2f;

        alpha = sprite.color;
        alpha = Color.black;
        alpha.a = 0.6f;
        sprite.color = alpha;

        fade.enabled = true;

        isDisguise = true;
        EffectManager.instance.effectSounds[8].source.Play();
        EffectManager.instance.effectSounds[14].source.Play();
        myRigid.constraints = RigidbodyConstraints2D.FreezePositionY|RigidbodyConstraints2D.FreezeRotation;
        skill = false;
        EffectManager.instance.effectSounds[39].source.Play();

        Color tmp = light2D.color;
        while (deck > 3)
        {
            deck--;
            light2D.pointLightInnerRadius -= 0.2f;
            light2D.pointLightOuterRadius -= 0.9f;
            //light2D.color -= new Color(0.012f * tmp.r, 0.012f * tmp.g, 0.012f * tmp.b, 0);
            light2D.color -= new Color(0.008f * tmp.r, 0.008f * tmp.g, 0.008f * tmp.b, 0);
            yield return new WaitForSeconds(0.01f);
        }
        while (light2D.pointLightInnerRadius > 0.2f)
        {
            light2D.pointLightInnerRadius -= 0.2f;
            light2D.pointLightOuterRadius -= 0.9f;
            //light2D.color -= new Color(0.012f * tmp.r, 0.012f * tmp.g, 0.012f * tmp.b, 0);
            light2D.color -= new Color(0.008f * tmp.r, 0.008f * tmp.g, 0.008f * tmp.b, 0);
            yield return new WaitForSeconds(0.01f);
        }
        deck = 3;
        yield return new WaitForSeconds(0.3f);
        EffectManager.instance.effectSounds[39].source.Stop();
        light2D.pointLightInnerRadius = 0;
        light2D.pointLightOuterRadius = 5;
        //light2D.color = new Color(0.4f * tmp.r, 0.4f * tmp.g, 0.4f * tmp.b);
        light2D.color = new Color(0.6f * tmp.r, 0.6f * tmp.g, 0.6f * tmp.b);

        quitable = true;
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.5f);
            if (quitable == false) break;
        }
        quitable = false;

        fade.enabled = false;
        light2D.color = new Color(tmp.r, tmp.g, tmp.b);
        alpha = sprite.color;
        alpha = Color.white;
        alpha.a = 1;
        sprite.color = alpha;
        isDisguise = false;
        EffectManager.instance.effectSounds[14].source.Stop();
        EffectManager.instance.effectSounds[13].source.Play();
        light2D.pointLightInnerRadius = 10;
        light2D.pointLightOuterRadius = 50;
        SoundManager.instance.bgmPlayer.volume = slider.value;

    }
    IEnumerator Disguise()
    {
        animator.SetBool("walking", false);
        skill = true;
        if (deck > 0) deck--;
        EffectManager.instance.effectSounds[15].source.Play();
        yield return new WaitForSeconds(0.2f*1.5f);
        if (deck > 0) deck--;
        EffectManager.instance.effectSounds[16].source.Play();
        yield return new WaitForSeconds(0.2f*1.5f);
        if (deck > 0) deck--;
        EffectManager.instance.effectSounds[17].source.Play();
        yield return new WaitForSeconds(0.2f*1.5f);
        if (deck > 0) deck--;
        EffectManager.instance.effectSounds[18].source.Play();
        yield return new WaitForSeconds(0.4f*1.5f);
        if (deck > 0) deck--;
        EffectManager.instance.effectSounds[11].source.Play();
        PlayerPrefs.SetInt("DisguiseRec", PlayerPrefs.GetInt("DisguiseRec") + 1);
        if (PlayerPrefs.GetInt("DisguiseRec") >= 33)
            AchievementManager.instance.Unlock("NEW_ACHIEVEMENT_1_11");
        alpha = sprite.color;
        alpha.a = 0.7f;
        sprite.color = alpha;
        dummy.transform.position = transform.position;
        dummy.transform.localScale = transform.localScale;
        dummy.GetComponent<SpriteRenderer>().enabled = true;
        dummy.enabled = true;
        dummy.time = 0;
        skill = false;
        EffectManager.instance.effectSounds[12].source.Play();
        EffectManager.instance.effectSounds[12].source.pitch = 1.0f;
        animator.SetBool("disguise", true);
        disguiseCoolImageColor = disguiseCoolImage.color;
        disguiseCoolImageColor.a = 1;
        disguiseCoolImage.color = disguiseCoolImageColor;
        disguiseCoolImage.fillAmount = 0;
        isDisguise = true;
        yield return new WaitForSeconds(5);
        animator.SetBool("disguise", false);
        EffectManager.instance.effectSounds[12].source.Stop();
        EffectManager.instance.effectSounds[13].source.Play();
        alpha = sprite.color;
        alpha.a = 1;
        sprite.color = alpha;
        disguiseCur = disguiseCool;
        isDisguise = false;
    }
    IEnumerator Joker()
    {
        if (DialogueManager.instance.animator.GetBool("Window"))
            yield break;
        jokerOn = true;
        toNext = false;
        joker.SetBool("BWAppear", true);
        EffectManager.instance.effectSounds[36].source.Play();
        BWJoker = 5;
        while (globalVolume.weight < 1)
        {
            light2D.intensity = 2 * (globalVolume.weight - 0.5f) * (globalVolume.weight - 0.5f) + 0.5f;
            globalVolume.weight += Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
        EffectManager.instance.effectSounds[0].source.Play();
        globalVolume.weight = 1;

        /*while (Shape.instance.intensity < 1)
            {
                Shape.instance.intensity += 0.02f;
                Shape.instance.shadowThreshold += 0.008f;
                yield return new WaitForSeconds(0.01f);
            }
            EffectManager.instance.effectSounds[0].source.Play();
            Shape.instance.intensity = 1;
            Shape.instance.shadowThreshold = 0.4f;
*/
/*
        SoundManager.instance.bgmSounds[0].clip = SoundManager.instance.bgmPlayer.clip;
        float tmp = SoundManager.instance.bgmPlayer.time;
        SoundManager.instance.bgmPlayer.clip = SoundManager.instance.bgmSounds[6].clip;
        SoundManager.instance.bgmPlayer.time = bgmTime;
        bgmTime = tmp;

        //if (SoundManager.instance.bgmPlayer.time < 122)
        //SoundManager.instance.bgmPlayer.time = 122;

        SoundManager.instance.bgmPlayer.Play();*/
        joker.SetBool("BWAppear", false);
        float time = 0;
        while (true) //SoundManager.instance.bgmPlayer.time < 172)
        {
            if (Input.GetKey(KeyCode.S) || library || toNext) break;
            yield return new WaitForSeconds(0.1f);
            if (Input.GetKey(KeyCode.S) || library || toNext) break;
            yield return new WaitForSeconds(0.1f);
            if (Input.GetKey(KeyCode.S) || library || toNext) break;
            yield return new WaitForSeconds(0.1f);
            if (Input.GetKey(KeyCode.S) || library || toNext) break;
            yield return new WaitForSeconds(0.1f);
            if (Input.GetKey(KeyCode.S) || library || toNext) break;
            yield return new WaitForSeconds(0.1f);
            time += 0.5f;
            if (deck > 0 && !(isDisguise && fade.enabled)/* && SoundManager.instance.bgmPlayer.time > 122*/)
            {
                deck--;
                EffectManager.instance.effectSounds[41].source.Play();
            }
            if (Input.GetKey(KeyCode.S) || library || toNext) break;
            yield return new WaitForSeconds(0.1f);
            if (Input.GetKey(KeyCode.S) || library || toNext) break;
            yield return new WaitForSeconds(0.1f);
            if (Input.GetKey(KeyCode.S) || library || toNext) break;
            yield return new WaitForSeconds(0.1f);
            if (Input.GetKey(KeyCode.S) || library || toNext) break;
            yield return new WaitForSeconds(0.1f);
            if (Input.GetKey(KeyCode.S) || library || toNext) break;
            yield return new WaitForSeconds(0.1f);
            time += 0.5f;
            if (Input.GetKey(KeyCode.S) || library || toNext) break;
            if (deck > 0 && !(isDisguise && fade.enabled))
            {
                deck--;
                EffectManager.instance.effectSounds[41].source.Play();
            }
            yield return null;
            if (time > 15)
                AchievementManager.instance.Unlock("NEW_ACHIEVEMENT_1_10");
        }
        toNext = false;
        BWJoker = 1;
        //EffectManager.instance.effectSounds[41].source.Stop();
        joker.SetBool("CAppear", true);
        EffectManager.instance.effectSounds[37].source.Play();
        while (globalVolume.weight > 0)
        {
            light2D.intensity = 2 * (globalVolume.weight - 0.5f) * (globalVolume.weight - 0.5f) + 0.5f;
            globalVolume.weight -= Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
        globalVolume.weight = 0;
        /*   while (Shape.instance.intensity > 0)
           {
               Shape.instance.intensity -= 0.02f;
               Shape.instance.shadowThreshold -= 0.008f;
               yield return new WaitForSeconds(0.01f);
           }
           EffectManager.instance.effectSounds[0].source.Play();
           Shape.instance.intensity = 0;
           Shape.instance.shadowThreshold = 0;
   */

        /*tmp = 
         * .instance.bgmPlayer.time;
        SoundManager.instance.bgmPlayer.clip = SoundManager.instance.bgmSounds[0].clip;
        SoundManager.instance.bgmPlayer.time = bgmTime;
        bgmTime = tmp;
        if (!library)
            SoundManager.instance.bgmPlayer.Play();
            */

        joker.SetBool("CAppear", false);
        
        jokerOn = false;
    }

    IEnumerator IsPause()
    {
        paused = true;
        isPause = !isPause;
        if (isPause)
        {
            if (SoundManager.instance.lowPassFilter.enabled) soundFilter = true;
            else SoundManager.instance.lowPassFilter.enabled = true;

            blur.enabled = true;
            EffectManager.instance.effectSounds[28].source.Play();
            EscDown.SetBool("Esc", true);
            EscUp.SetBool("Esc", true);
            //yield return new WaitForSeconds(0.2f);
            cm.enabled = false;
            Time.timeScale = 0f;
        }
        else
        {
            if (soundFilter) soundFilter = false;
            else SoundManager.instance.lowPassFilter.enabled = false;
            blur.enabled = false;
            Time.timeScale = 1f;
            cm.enabled = true;
            EscDown.SetBool("Esc", false);
            EscUp.SetBool("Esc", false);
            yield return new WaitForSeconds(0.1f);
        }
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        paused = false;
    }

    IEnumerator GameOver()
    {
        gameover = true;
        if (PlayerPrefs.GetInt("StartLibrary") != 2)
            AchievementManager.instance.Unlock("NEW_ACHIEVEMENT_1_7");
        else
            AchievementManager.instance.Unlock("NEW_ACHIEVEMENT_1_9");
        if (reloadImage1.enabled)
        {
            reloadImage1.enabled = false;
            reloadImage2.enabled = false;
        }
        animator.SetTrigger("gameover");
        skill = true;
        isDisguise = true;
        animator.SetBool("walking", false);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;

        if (SoundManager.instance.bgmPlayer.clip != SoundManager.instance.bgmSounds[7].clip)
        {
            SoundManager.instance.ChangeBGM(7);
            SoundManager.instance.bgmPlayer.time = 35;
        }

        if (EffectManager.instance.effectSounds[7].source.isPlaying)
            EffectManager.instance.effectSounds[7].source.Stop();

        if (EffectManager.instance.effectSounds[22].source.isPlaying)
            EffectManager.instance.effectSounds[22].source.Stop();

        yield return new WaitForSeconds(0.2f);
        EffectManager.instance.effectSounds[31].source.Play();
        yield return new WaitForSeconds(0.2f);
        EffectManager.instance.effectSounds[32].source.Play();
        yield return new WaitForSeconds(0.3f);
        EffectManager.instance.effectSounds[33].source.Play();
        EffectManager.instance.effectSounds[39].source.Play();

        yield return new WaitForSeconds(0.5f);
        //EffectManager.instance.effectSounds[36].source.Play();
        rope1.SetActive(true);
        rope1.GetComponent<Rope>().enabled = true;

        yield return new WaitForSeconds(0.5f);
        //EffectManager.instance.effectSounds[37].source.Play();
        rope3.SetActive(true);
        rope3.GetComponent<Rope>().enabled = true;

        yield return new WaitForSeconds(0.5f);
        //EffectManager.instance.effectSounds[44].source.Play();
        rope2.SetActive(true);
        rope2.GetComponent<Rope>().enabled = true;

        yield return new WaitForSeconds(1);
        gameoverScene.SetActive(true);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(atkPos.position, atkSize);
    }
    public void OnClickHome()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void OnClickUSB()
    {
        blur.enabled = false;
        Time.timeScale = 1f;
        cm.enabled = true;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        paused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnClickBluePrint()
    {
        EffectManager.instance.effectSounds[42].source.Play();
        bluePrint.SetBool("appear", true);
    }
    public void OnClickBluePrintNull()
    {
        EffectManager.instance.effectSounds[43].source.Play();
        bluePrint.SetBool("appear", false);
    }
    public void OnClickFool()
    {
        tarotMagician.SetActive(true);
        guideText.text = "THE MAGICIAN\n직원들의 패턴이 눈에 보이게 됩니다.";
        guideCard.SetTrigger("appear");
        guideOn = true;
    }
    public void OnClickMagician()
    {
        tarotMagician.SetActive(false);
        guideText.text = "THE FOOL\n직원들의 패턴이 눈에 보이지 않게 됩니다.";
        guideCard.SetTrigger("appear");
        guideOn = false;
    }
    public void OnClickLang()
    {
        if (PlayerPrefs.GetInt("LangEng") == 0)
            PlayerPrefs.SetInt("LangEng", 1);
        else
            PlayerPrefs.SetInt("LangEng", 0);
        OnClickUSB();
    }
    public void OnClickKor()
    {
        if (PlayerPrefs.GetInt("LangEng") != 0)
        {
            PlayerPrefs.SetInt("LangEng", 0);
            OnClickUSB();
        }
    }
    public void OnClickEng()
    {
        if (PlayerPrefs.GetInt("LangEng") == 0)
        {
            PlayerPrefs.SetInt("LangEng", 1);
            OnClickUSB();
        }
    }
}
