using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform pos;
    Dummy dummy;
    Player player;
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (player == null)
                player = Player.instance;
            if (Input.GetKeyDown(KeyCode.W) && !player.gameover && !player.library)
            {
                if (player.skill == false)
                {
                    if (player.isDisguise && !player.fade.enabled)
                    {
                        player.StopCoroutine("Disguise");
                        player.animator.SetBool("disguise", false);
                        player.disguiseCur = player.disguiseCool;
                        Color color = player.sprite.color;
                        color.a = 1;
                        player.sprite.color = color;
                        player.isDisguise = false;
                        EffectManager.instance.effectSounds[12].source.Stop();
                        Dummy.instance.DestroyDummy();
                    }
                    StartCoroutine(DoorOpen(collision));
                }
            }
        }
    }

    public IEnumerator DoorOpen(Collider2D collision)
    {
        player.animator.SetBool("walking", false);
        if (player.deck > 0)
        {
            //player.animator.SetBool("door", true);
            player.animator.SetTrigger("doorT");
            if (!(player.isDisguise && player.fade.enabled)) player.deck--;
            player.skill = true;
            player.myRigid.constraints = RigidbodyConstraints2D.FreezePosition|RigidbodyConstraints2D.FreezeRotation;
            yield return new WaitForSeconds(0.3f);
            if (player.deck > 0 && !(player.isDisguise && player.fade.enabled)) player.deck --;
            yield return new WaitForSeconds(0.1f);
            EffectManager.instance.effectSounds[3].source.Play();
            yield return new WaitForSeconds(0.1f);
            if (player.deck > 0 && !(player.isDisguise && player.fade.enabled)) player.deck --;
            yield return new WaitForSeconds(0.2f);
            //if (Player.instance.deck > 0) Player.instance.deck--;
            yield return new WaitForSeconds(0.2f);
            EffectManager.instance.effectSounds[40].source.Play();
            if (player.deck > 0 && !(player.isDisguise && player.fade.enabled)) player.deck --;
            yield return new WaitForSeconds(0.3f);
            if (player.deck > 0 && !(player.isDisguise && player.fade.enabled)) player.deck --;
            EffectManager.instance.effectSounds[4].source.Play();
            yield return new WaitForSeconds(0.3f);
            dummy.transform.position = player.transform.position;
            dummy.transform.localScale = player.transform.localScale;
            dummy.GetComponent<SpriteRenderer>().enabled = true;
            dummy.enabled = true;
            dummy.time = 0;
            player.gas.transform.position = dummy.transform.position;
            player.gas.enabled = true;
            //if (Player.instance.deck > 0) Player.instance.deck--;
            yield return new WaitForSeconds(1.2f);
            //if (Player.instance.deck > 0) Player.instance.deck--;
            player.transform.position = new Vector3(pos.position.x, player.transform.position.y, 0);
            EffectManager.instance.effectSounds[5].source.Play();
            player.myRigid.constraints = RigidbodyConstraints2D.FreezePositionY|RigidbodyConstraints2D.FreezeRotation;
            player.skill = false;
            //player.animator.SetBool("door", false);
            Invoke("GasDisappear", 5);
        }
    }
    
    public void GasDisappear()
    {
        player.gas.GetComponent<Animator>().SetTrigger("disappear");
        Invoke("NoGas", 1);
    }
    public void NoGas()
    {
        player.gas.enabled = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
        //dummy = Player.instance.dummy;
    }

    // Update is called once per frame
    void Update()
    {
        if (dummy == null)
            dummy = Player.instance.dummy;
    }
}
