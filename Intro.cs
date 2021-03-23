using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public Text text;
    public float appear;
    public float stay;
    bool fade;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !fade)
            StartCoroutine(FadeOut());
    }
    
    public IEnumerator FadeIn()
    {
        fade = true;
        while (text.color.a < 1)
        {
            text.color += new Color(0, 0, 0, appear * Time.deltaTime);
            yield return null;
        }
        fade = false;
    }
    public IEnumerator FadeOut()
    {
        fade = true;
        while (text.color.a > 0)
        {
            text.color -= new Color(0, 0, 0, appear * Time.deltaTime);
            yield return null;
        }
        SceneManager.LoadScene("MainMenu");
        yield return null;
    }
}
