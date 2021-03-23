using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    public SpriteRenderer sprite;
    bool entered;
    Color color;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            entered = true;            
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            entered = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        sprite.color = new Color(1, 1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!enabled) return;

        if (entered)
        {
            if (sprite.color.a <= 0.3f)
                return;
            color = sprite.color;
            color.a -= 2 * Time.deltaTime;
            sprite.color = color;

        }
        else
        {
            if (sprite.color.a >= 1)
                return;
            color = sprite.color;
            color.a += 2 * Time.deltaTime;
            sprite.color = color;

        }
    }
}
