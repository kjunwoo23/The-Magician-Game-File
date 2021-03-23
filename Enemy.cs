using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float slowSpeed;
    public float fastSpeed;
    public float confSpeed;
    public float loopTime;
    public float backTime;
    public float curTime;
    //public Hacking hacking;
    public Animator animator;
    bool flag = false;
    //public Transform target;
    Dummy dummy;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetTrigger("awake");
        dummy = Player.instance.dummy;
    }
       
    // Update is called once per frame
    void Update()
    {
        if (Player.instance.transform.position.y > transform.position.y + 5 || Player.instance.transform.position.y < transform.position.y - 5) return;
        if (dummy == null)
            dummy = Player.instance.dummy;

        animator.speed = 1 / Player.instance.BWJoker;
        curTime += Time.deltaTime / Player.instance.BWJoker;
        /*
        if (hacking.hacking > 0.5 * hacking.maxHacking)
        {
            slowSpeed = 1;
            fastSpeed = 4;
        }*/

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
        if (!animator.GetBool("panic"))
        {
            if (dummy.enabled == false)
                dummy.transform.position = Player.instance.transform.position;
            if (curTime < backTime)
            {
                animator.SetBool("back", true);
                if (transform.position.x < dummy.transform.position.x)
                {
                    transform.localScale = new Vector3(-0.9f, 0.9f, 0);
                    transform.position -= new Vector3(slowSpeed * Time.fixedDeltaTime / Player.instance.BWJoker, 0, 0);
                }
                if (transform.position.x > dummy.transform.position.x)
                {
                    transform.localScale = new Vector3(0.9f, 0.9f, 0);
                    transform.position += new Vector3(slowSpeed * Time.fixedDeltaTime / Player.instance.BWJoker, 0, 0);
                }
            }
            else if (curTime < loopTime)
            {
                animator.SetBool("back", false);
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
            else curTime = 0;
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
