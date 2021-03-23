using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BluePrint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RawImage[] rawImage;
    public Text[] text;
    bool entered = false;
    bool langEng = false;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("LangEng") == 1)
            langEng = true;
        if (text.Length != 0)
        {
            if (!langEng)
                text[0].text = "최고 층:\t\t" + PlayerPrefs.GetInt("MaxLibrary").ToString() + "F\n현재 층:\t\t" + PlayerPrefs.GetInt("StartLibrary").ToString() + "F";
            else
                text[1].text = "Highest Floor:\t\t" + PlayerPrefs.GetInt("MaxLibrary").ToString() + "F\nCurrent Floor:\t\t" + PlayerPrefs.GetInt("StartLibrary").ToString() + "F";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (entered)
            if (!langEng)
                rawImage[0].transform.position = Input.mousePosition;
            else
                rawImage[1].transform.position = Input.mousePosition;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        entered = true;
        if (!langEng)
            rawImage[0].enabled = true;
        else
            rawImage[1].enabled = true;
        if (text.Length != 0)
            if (!langEng)
                text[0].enabled = true;
            else
                text[1].enabled = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (text.Length != 0)
            if (!langEng)
                text[0].enabled = false;
            else
                text[1].enabled = false;
        if (!langEng)
            rawImage[0].enabled = false;
        else
            rawImage[1].enabled = false;
        entered = false;
    }
}
