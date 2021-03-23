using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightShake : MonoBehaviour
{
    public Light2D light2D;
    public float x = 0, y = 0;
    float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Light());
    }

    // Update is called once per frame
    void Update()
    {
        if (light2D.enabled)
        {
            light2D.pointLightInnerRadius = 3 + 0.5f * Mathf.Sin(t);
            light2D.pointLightOuterRadius = 4 + 0.5f * Mathf.Sin(t);
            if (t > 20 * Mathf.PI)
            {
                transform.position = new Vector3(x + 0.3f * Mathf.Cos(t), y + 0.3f * -Mathf.Sin(t), 0);
                t += Time.deltaTime;
            }
            else
            {
                transform.position = new Vector3(x + 0.15f * (20 * Mathf.PI - t + 2) * Mathf.Cos(t * 0.5f), y - 0.15f * (20 * Mathf.PI - t + 2) * Mathf.Sin(t * 0.5f), 0);
                t += 60 * Time.deltaTime;
            }
        }
    }
    IEnumerator Light()
    {
        yield return new WaitForSeconds(0.01f);
        EffectManager.instance.effectSounds[3].source.Play();
        yield return new WaitForSeconds(0.4f);
        EffectManager.instance.effectSounds[3].source.Play();
        yield return new WaitForSeconds(0.2f);
        EffectManager.instance.effectSounds[3].source.Play();
        yield return new WaitForSeconds(0.4f);
        EffectManager.instance.effectSounds[4].source.Play();
        light2D.enabled = true;
        yield return null;
    }
}
