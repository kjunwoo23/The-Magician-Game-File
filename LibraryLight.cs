using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LibraryLight : MonoBehaviour
{
    public Light2D light2D;
    public float rate;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        light2D.intensity = Mathf.Sin(time * rate) * 0.25f + 0.75f;
    }
}
