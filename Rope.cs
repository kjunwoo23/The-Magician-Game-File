using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rope : MonoBehaviour
{
    public int num;
    public RawImage rawImage;
    bool flag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!enabled) return;
        rawImage.uvRect = new Rect(rawImage.uvRect.x + Time.deltaTime, 0, 1, 1);
        if (!flag)
        {
            flag = true;
            Invoke("Stop", 5);
        }
    }

    public void Stop()
    {
        enabled = false;
        EffectManager.instance.effectSounds[44 + num].source.Play();
    }
}
