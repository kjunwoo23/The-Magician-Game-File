using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Faces
{
    public RawImage face;
}

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public Animator animator;
    public RawImage rawImage;
    public Faces[] faces;
    public Text text;

    public Animator logAnimator;
    public Text log;
    public string color;
    public GameObject logButton;
    public RawImage cutScene;

    public bool ctrl;

    public bool printStart;

    void Start()
    {
        instance = this;
    }
    public void Write()
    {
        if (!enabled)
            return;
        //EffectManager.instance.effectSounds[4].source.Play();
    }

    public void ShowMessage(int npc, string line)
    {
        if (!enabled)
            return;
        if (!(animator.GetBool("Window")))
        {
            EffectManager.instance.effectSounds[23].source.Play();
        }
        rawImage.texture = faces[npc].face.texture;
        animator.SetBool("Window", true);
        //text.text = line;
        //StartCoroutine(PrintMessage(line));
    }


    public void WriteMessage(int npc, string line)
    {
        if (!enabled)
            return;
        rawImage.texture = faces[npc].face.texture;
        //text.text = line;
        if (!ctrl)
            StartCoroutine(PrintMessage(line));
        if (npc == 1 || npc == 9)
            log.text += line + "\n";
        else
            log.text += "<color=#" + color + ">" + line + "</color>" + "\n";
    }

    IEnumerator PrintMessage(string line)
    {
        printStart = true;
        float cool = 0.5f / line.Length;
        string blank = line;
        string realText = "";
        bool italic = false;
        bool bold = false;
        text.text = blank;
        if (line.Length > 3)
            if (line.Substring(0, 3).Equals("<i>"))
            {
                italic = true;
                line = line.Substring(3, line.Length - 7);
                cool = 0.5f / line.Length;
                blank = line;
                text.text = blank;
            }
            else if (line.Substring(0, 3).Equals("<b>"))
            {
                bold = true;
                line = line.Substring(3, line.Length - 7);
                cool = 0.5f / line.Length;
                blank = line;
                text.text = blank;

            }
        //Debug.Log(line.Substring(0, 3));
        for (int i = 0; i < line.Length; i++)
        {
            if (ctrl) cool = 0;
            realText += line[i];
            blank = blank.Substring(1, blank.Length - 1);
            if (italic)
                text.text = "<i>" + realText + "</i>" + "<color=#00000000>" + blank + "</color>";
            else
                text.text = realText + "<color=#00000000>" + blank + "</color>";

            yield return new WaitForSeconds(cool);
            if (Input.GetKeyDown(KeyCode.Space) || ctrl)
            {
                text.text = line;
                break;
            }
        }
        if (italic)
            text.text = "<i>" + line + "</i>";
        else if (bold)
            text.text = "<b>" + line + "</b>";

        printStart = false;
    }


    public void HideMessage()
    {
        if (!enabled)
            return;
        if (animator.GetBool("Window"))
        {
            //EffectManager.instance.effectSounds[3].source.Play();
        }
        text.text = "";
        animator.SetBool("Window", false);
        rawImage.texture = faces[0].face.texture;
        //EffectManager.instance.effectSounds[4].source.Stop();
    }

    public void LogAppear()
    {
        if (logAnimator.GetBool("appear")) return;
        logAnimator.SetBool("appear", true);
        Player.instance.animator.SetBool("walking", false);
        EffectManager.instance.effectSounds[43].source.Play();
    }

    public void LogDisappear()
    {
        logAnimator.SetBool("appear", false);
    }
}
