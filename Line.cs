using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [System.Serializable]
    public class Dialogue
    {
        [TextArea(3, 10)]
        public string[] sentences;
        [TextArea(3, 10)]
        public string[] sentencesEng;
        public int[] npc;
        public int[] setTrue = { 0, 0, 0 };
        public int[] setFalse = { 0, 0, 0 };
    }
    public Dialogue dialogue;

    int i = 0;
    string line;
    int npc;
    int lineStop = 0;
    int lineSize;
    bool stay = false;
    bool skip = false;
    bool fadeOn;
    bool langEng = false;

    void Awake()
    {
        //if (!enabled) return;
        npc = dialogue.npc[i];
        if (PlayerPrefs.GetInt("LangEng") == 0) langEng = false;
        else langEng = true;

        if (!langEng)
        {
            line = dialogue.sentences[i];
            lineSize = dialogue.sentences.Length;
        }
        else
        {
            line = dialogue.sentencesEng[i];
            lineSize = dialogue.sentencesEng.Length;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled) return;
        if (other.gameObject.tag == "Player")
        {
            Player.instance.skill = true;
            Player.instance.animator.SetBool("walking", false);
            stay = true;
            i = 0;
            DialogueManager.instance.ShowMessage(0, line);
            DialogueManager.instance.logButton.SetActive(true);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (!enabled) return;
        if (other.gameObject.tag == "Player")
        {
            if (stay == false)
            {
                Player.instance.skill = true;
                Player.instance.animator.SetBool("walking", false);
                stay = true;
                i = 0;
                DialogueManager.instance.ShowMessage(0, line);
                DialogueManager.instance.logButton.SetActive(true);
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
            if (!Player.instance.skill)
            {
                Player.instance.skill = true;
                Player.instance.animator.SetBool("walking", false);
            }
            if (Input.GetKeyDown(KeyCode.LeftControl) && (DialogueManager.instance.animator.GetBool("Window")))
            {
                DialogueManager.instance.ctrl = true;
                while (i < lineSize - 2)
                {
                    i++;
                    npc = dialogue.npc[i];
                    if (!langEng)
                        line = dialogue.sentences[i];
                    else
                        line = dialogue.sentencesEng[i];
                    DialogueManager.instance.WriteMessage(npc, line);
                }
                DialogueManager.instance.animator.SetBool("Window", false);
                DialogueManager.instance.HideMessage();
                Player.instance.skill = false;
                skip = true;
                for (int i = 0; i < 3; i++)
                {
                    SwitchManager.instance.switches[dialogue.setTrue[i]].bools = true;
                    SwitchManager.instance.switches[dialogue.setFalse[i]].bools = false;
                }
                if (GetComponent<CutScene>())
                    GetComponent<CutScene>().ctrl = true;
                if (!Player.instance.library)
                {
                    DialogueManager.instance.logButton.SetActive(false);
                    if (DialogueManager.instance.logAnimator.GetBool("appear"))
                        DialogueManager.instance.logAnimator.SetBool("appear", false);
                }
                DialogueManager.instance.ctrl = false;
                enabled = false;
                //EffectManager.instance.effectSounds[4].source.Stop();
            }
            if (lineStop == 0)
            {
                npc = dialogue.npc[i];
                if (!langEng)
                    line = dialogue.sentences[i];
                else
                    line = dialogue.sentencesEng[i];
                DialogueManager.instance.WriteMessage(npc, line);
                lineStop++;
            }
            if (i == lineSize - 2)
            {
                i++;
                DialogueManager.instance.HideMessage();
                Player.instance.skill = false;
                skip = true;
                for (int i = 0; i < 3; i++)
                {
                    SwitchManager.instance.switches[dialogue.setTrue[i]].bools = true;
                    SwitchManager.instance.switches[dialogue.setFalse[i]].bools = false;
                }
                if (!Player.instance.library)
                {
                    DialogueManager.instance.logButton.SetActive(false);
                    if (DialogueManager.instance.logAnimator.GetBool("appear"))
                        DialogueManager.instance.logAnimator.SetBool("appear", false);
                }
                enabled = false;
            }
            if (Input.GetKeyDown(KeyCode.Space) && i < lineSize - 2 && !DialogueManager.instance.printStart)
            {
                if (!GetComponent<CutScene>())
                {
                    //DialogueManager.instance.Write();
                    i++;
                    npc = dialogue.npc[i];
                    if (!langEng)
                        line = dialogue.sentences[i];
                    else
                        line = dialogue.sentencesEng[i];
                    DialogueManager.instance.WriteMessage(npc, line);
                }
                else if (!fadeOn && GetComponent<CutScene>().scenes[i].fadeOutTime != 0)
                    StartCoroutine(NextLine());
                else if (!fadeOn)
                {
                    //DialogueManager.instance.Write();
                    i++;
                    npc = dialogue.npc[i];
                    if (!langEng)
                        line = dialogue.sentences[i];
                    else
                        line = dialogue.sentencesEng[i];
                    DialogueManager.instance.WriteMessage(npc, line);
                }
            }
        }
    }
  /*  void OnTriggerExit2D(Collider2D other)
    {
        if (!enabled) return;
        if (other.gameObject.tag == "Player")
        {
            Player.instance.skill = false;
            lineStop = 0;
            DialogueManager.instance.HideMessage();
            /*if (i == lineSize - 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    SwitchManager.instance.switches[dialogue.setTrue[i]].bools = true;
                    SwitchManager.instance.switches[dialogue.setFalse[i]].bools = false;
                }
            }
            if (!Player.instance.library)
            {
                DialogueManager.instance.logButton.SetActive(false);
                if (DialogueManager.instance.logAnimator.GetBool("appear"))
                    DialogueManager.instance.logAnimator.SetBool("appear", false);
            }
            stay = false;
            enabled = false;
        }
    }*/
    public IEnumerator NextLine()
    {
        fadeOn = true;
        while (FadeManager.instance.fade1.color.a < 1)
        {
            yield return null;
        }

        //DialogueManager.instance.Write();
        if (!GetComponent<CutScene>().ctrl)
        {
            i++;
            npc = dialogue.npc[i];
            if (!langEng)
                line = dialogue.sentences[i];
            else
                line = dialogue.sentencesEng[i];
            DialogueManager.instance.WriteMessage(npc, line);
        }
        while (FadeManager.instance.fade1.color.a > 0)
        {
            yield return null;
        }
        fadeOn = false;
    }
}
