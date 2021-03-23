using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public static Dummy instance;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enabled) return;
        if (Player.instance.isHacking)
            DestroyDummy();
        time += Time.deltaTime / Player.instance.BWJoker;
        if (time > 5) DestroyDummy();

    }
    public void DestroyDummy()
    {
        if (!enabled) return;
        time = 0;
        GetComponent<SpriteRenderer>().enabled = false;
        enabled = false;
    }
}
