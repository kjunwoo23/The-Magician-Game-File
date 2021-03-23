using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Posts : MonoBehaviour
{
    public bool pw = false;
    public Animator animator;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void disappear()
    {
        if (!pw)
        {
            GetComponent<Button>().enabled = false;
            EffectManager.instance.effectSounds[25].source.Play();
            animator.SetBool("click", true);
            Invoke("disappear2", 0.1f);
        }
        else if (pw && transform.parent.childCount == 1)
        {
            GetComponent<Button>().enabled = false;
            Invoke("Invisible", 2);
            PostItManager.instance.GetComponent<Animator>().SetBool("post", false);
            PostItManager.instance.post = false;
            StartCoroutine(PostItManager.instance.Typing());
            animator.SetBool("click2", true);
            Invoke("disappear2", 12f);
        }
    }

    public void Invisible()
    {
        GetComponent<RawImage>().enabled = false;
        text.enabled = false;
    }
    public void disappear2()
    {
        Destroy(gameObject);
    }

}
