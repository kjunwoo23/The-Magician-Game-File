using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public Text cardLeft;
    public Animator[] cards;
    public RawImage[] cardsA;
    int UIcard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int.TryParse(cardLeft.text, out UIcard);
        if (UIcard > Player.instance.deck)
        {
            cardLeft.text = Player.instance.deck.ToString();
            for (int i = Player.instance.deck; i < UIcard + 1; i++)
            {
                if (Player.instance.deck > 0)
                {
                    cards[i - 1].SetBool("Disappear", true);
                    if (i < 52)
                        cardsA[i].enabled = false;
                }
                //if (i < Player.instance.deck)
                //  cards[i].SetBool("Disappear", false);
            }
            if (Player.instance.deck > 0)
                cardsA[Player.instance.deck - 1].enabled = true;
            if (Player.instance.deck == 0)
            {
                for(int i = 0; i < 52; i++)
                {
                    cards[i].SetBool("Disappear", true);
                }
                cardsA[UIcard - 1].enabled = false;

            }
        }
        if (UIcard < Player.instance.deck)
        {
            cardLeft.text = Player.instance.deck.ToString();
            for (int i = UIcard; i < Player.instance.deck; i++)
            {
                if (0 < Player.instance.deck)
                {
                    cards[0].SetBool("Disappear", false);
                    if (UIcard > 0)
                        cards[i - 1].SetBool("Disappear", false);
                    Invoke("drawCard", 0.5f);
                }
                if (i < Player.instance.deck)
                  cards[i].SetBool("Disappear", false);
            }
            if (Player.instance.deck == 0)
            {
                cards[Player.instance.deck].SetBool("Disappear", false);
                cardsA[Player.instance.deck].enabled = false;
            }
        }
    }
    void drawCard()
    {
        for (int i = 0; i < Player.instance.deck; i++)
            cardsA[i].enabled = false;
        if (Player.instance.deck != 0)
            cardsA[Player.instance.deck - 1].enabled = true;
    }
}
