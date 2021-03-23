using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy4 : MonoBehaviour
{
    public float slowSpeed;
    public float fastSpeed;
    public float confSpeed;
    public float maxTime;
    public float curTime;
    public Animator animator;
    bool flag = false;
    //public Transform target;
    Dummy dummy;
    public bool area;
    Vector2 MousePosition;
    public Text text;
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetTrigger("awake");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.instance.transform.position.y > transform.position.y + 5 || Player.instance.transform.position.y < transform.position.y - 5) return;

        if (dummy == null)
            dummy = Player.instance.dummy;

        curTime += Time.deltaTime / Player.instance.BWJoker;

        animator.speed = 1 / Player.instance.BWJoker;

        if (Player.instance.guideOn == true)
        {
            if (canvas.enabled == false)
                canvas.enabled = true;
        }
        else if (canvas.enabled == true)
            canvas.enabled = false;

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
        
        area = false;
    }
    private void FixedUpdate()
    {
        if (Player.instance.transform.position.y > transform.position.y + 5 || Player.instance.transform.position.y < transform.position.y - 5) return;
        if (dummy == null)
            dummy = Player.instance.dummy;
        if (!animator.GetBool("panic"))
        {
            if (dummy.enabled == true)
            {
                //target.position = dummy.transform.position;
                if (dummy.transform.localScale.x > 0)
                    if (dummy.transform.transform.position.x < transform.position.x)
                        curTime = 0;
                if (dummy.transform.localScale.x < 0)
                    if (dummy.transform.transform.position.x > transform.position.x)
                        curTime = 0;
                if (curTime > maxTime)
                {
                    animator.SetBool("back", false);
                    text.enabled = true;
                    if (transform.position.x < dummy.transform.position.x)
                    {
                        transform.localScale = new Vector3(-0.9f, 0.9f, 0);
                        transform.position += new Vector3(fastSpeed * Time.fixedDeltaTime / Player.instance.BWJoker, 0, 0);
                    }
                    if (transform.position.x > dummy.transform.position.x)
                    {
                        transform.localScale = new Vector3(0.9f, 0.9f, 0);
                        transform.position -= new Vector3(fastSpeed * Time.fixedDeltaTime / Player.instance.BWJoker, 0, 0);
                    }
                }
                else
                {
                    animator.SetBool("back", true);
                    text.enabled = false;
                    if (transform.position.x < dummy.transform.position.x)
                    {
                        transform.localScale = new Vector3(-0.9f, 0.9f, 0);
                        if (!area)
                            transform.position -= new Vector3(slowSpeed * Time.fixedDeltaTime / Player.instance.BWJoker, 0, 0);
                        else
                            transform.position += new Vector3(slowSpeed * Time.fixedDeltaTime / Player.instance.BWJoker, 0, 0);

                    }
                    if (transform.position.x > dummy.transform.position.x)
                    {
                        transform.localScale = new Vector3(0.9f, 0.9f, 0);
                        if (!area)
                            transform.position += new Vector3(slowSpeed * Time.fixedDeltaTime / Player.instance.BWJoker, 0, 0);
                        else
                            transform.position -= new Vector3(slowSpeed * Time.fixedDeltaTime / Player.instance.BWJoker, 0, 0);
                    }
                }
            }
            else
            {
                dummy.transform.position = Player.instance.transform.position;
                //MousePosition = Player.instance.MousePosition;
                if (Player.instance.isHacking) ;
                else if ((Player.instance.transform.localScale.x > 0 && !Player.instance.sprite.flipX) || (Player.instance.transform.localScale.x < 0 && Player.instance.sprite.flipX)
                    /*MousePosition.x > Player.instance.transform.position.x + 0.5 * Player.instance.playerSize*/)
                {
                    if (Player.instance.transform.position.x < transform.position.x)
                        curTime = 0;
                }
                else if ((Player.instance.transform.localScale.x > 0 && Player.instance.sprite.flipX) || (Player.instance.transform.localScale.x < 0 && !Player.instance.sprite.flipX)
                    /*MousePosition.x < Player.instance.transform.position.x - 0.5 * Player.instance.playerSize*/)
                {
                    if (Player.instance.transform.position.x > transform.position.x)
                        curTime = 0;
                }
                if (curTime > maxTime)
                {
                    animator.SetBool("back", false);
                    text.enabled = true;
                    if (transform.position.x < dummy.transform.position.x)
                    {
                        transform.localScale = new Vector3(-0.9f, 0.9f, 0);
                        transform.position += new Vector3(fastSpeed * Time.fixedDeltaTime / Player.instance.BWJoker, 0, 0);
                    }
                    if (transform.position.x > dummy.transform.position.x)
                    {
                        transform.localScale = new Vector3(0.9f, 0.9f, 0);
                        transform.position -= new Vector3(fastSpeed * Time.fixedDeltaTime / Player.instance.BWJoker, 0, 0);
                    }
                }
                else
                {
                    animator.SetBool("back", true);
                    text.enabled = false;
                    if (transform.position.x < dummy.transform.position.x)
                    {
                        transform.localScale = new Vector3(-0.9f, 0.9f, 0);
                        if (!area)
                            transform.position -= new Vector3(slowSpeed * Time.fixedDeltaTime / Player.instance.BWJoker, 0, 0);
                        else
                            transform.position += new Vector3(slowSpeed * Time.fixedDeltaTime / Player.instance.BWJoker, 0, 0);

                    }
                    if (transform.position.x > dummy.transform.position.x)
                    {
                        transform.localScale = new Vector3(0.9f, 0.9f, 0);
                        if (!area)
                            transform.position += new Vector3(slowSpeed * Time.fixedDeltaTime / Player.instance.BWJoker, 0, 0);
                        else
                            transform.position -= new Vector3(slowSpeed * Time.fixedDeltaTime / Player.instance.BWJoker, 0, 0);
                    }
                }
            }
        }
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
