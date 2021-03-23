using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutScene : MonoBehaviour
{
    [System.Serializable]
    public class Scene
    {
        public float fadeOutTime;
        public float fadeInTime;
        public Texture texture;
    }

    RawImage scene;
    public Scene[] scenes;

    int i = 0;
    int sceneStop = 0;
    int sceneSize;
    bool stay = false;
    bool skip = false;
    bool fadeOn;
    public bool ctrl;

    void Start()
    {
        if (!enabled) return;
        sceneSize = scenes.Length;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled) return;
        if (other.gameObject.tag == "Player")
        {
            scene = DialogueManager.instance.cutScene;
            //scene.texture = scenes[i].texture;
            Player.instance.skill = true;
            Player.instance.animator.SetBool("walking", false);
            stay = true;
            i = 0;
            scene.enabled = true;
            scene.texture = scenes[i].texture;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (!enabled) return;
        if (other.gameObject.tag == "Player")
        {
            if (stay == false)
            {
                stay = true;
                i = 0;
                scene.texture = scenes[i].texture;
            }
        }
    }
    void Update()
    {
        if (!enabled) return;
        if (stay)
        {/*
            if (Input.GetKeyDown(KeyCode.W) && !(DialogueManager.instance.animator.GetBool("Window")))
            {
                DialogueManager.instance.animator.SetBool("Window", true);
                EffectManager.instance.effectSounds[23].source.Play();
            }*/

            
            if (ctrl)
            {
                DialogueManager.instance.animator.SetBool("Window", false);
                Player.instance.skill = false;
                scene.enabled = false;
                skip = true;
                enabled = false;
                //EffectManager.instance.effectSounds[4].source.Stop();
            }
            if (sceneStop == 0)
            {
                scene.texture = scenes[i].texture;
                sceneStop++;
            }
            if (i == sceneSize - 2)
            {
                i++;
                Player.instance.skill = false;
                scene.enabled = false;
                skip = true;
                enabled = false;
            }
            if (Input.GetKeyDown(KeyCode.Space) && i < sceneSize - 2 && !DialogueManager.instance.printStart)
            {
                if (!fadeOn)
                    StartCoroutine(NextScene());
                //i++;
                //scene.texture = scenes[i].texture;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (!enabled) return;
        if (other.gameObject.tag == "Player")
        {
            Player.instance.skill = false;
            sceneStop = 0;
            scene.enabled = false;
            stay = false;
            enabled = false;
        }
    }
    public IEnumerator NextScene()
    {
        fadeOn = true;
        int tmp = i;
        if (scenes[tmp].fadeOutTime != 0)
            while (FadeManager.instance.fade1.color.a < 1)
            {
                FadeManager.instance.fade1.color += new Color(0, 0, 0, Time.deltaTime / scenes[tmp].fadeOutTime);
                yield return null;
            }

        i++;
        tmp = i;
        scene.texture = scenes[tmp].texture;

        if (scenes[tmp - 1].fadeInTime != 0)
            while (FadeManager.instance.fade1.color.a > 0)
            {
                FadeManager.instance.fade1.color -= new Color(0, 0, 0, Time.deltaTime / scenes[tmp - 1].fadeInTime);
                yield return null;
            }
        fadeOn = false;
    }
}
