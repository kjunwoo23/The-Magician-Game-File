using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;

    public RawImage fade1;
    public RawImage fade2;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Fade1In(float time)
    {
        while (fade1.color.a > 0)
        {
            fade1.color -= new Color(0, 0, 0, Time.deltaTime / time);
            yield return null;
        }
    }
    public IEnumerator Fade1Out(float time)
    {
        while (fade1.color.a < 1)
        {
            fade1.color += new Color(0, 0, 0, Time.deltaTime / time);
            yield return null;
        }
    }
    public IEnumerator Fade2In(float time)
    {
        while (fade2.color.a > 0)
        {
            fade2.color -= new Color(0, 0, 0, Time.deltaTime / time);
            yield return null;
        }
    }
    public IEnumerator Fade2Out(float time)
    {
        while (fade2.color.a < 1)
        {
            fade2.color += new Color(0, 0, 0, Time.deltaTime / time);
            yield return null;
        }
    }
}
