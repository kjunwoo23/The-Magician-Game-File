using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            player = Player.instance;

        if (!player.skill && !player.barricade)
        {
            if (player.transform.position.y > transform.position.y + 5 || player.transform.position.y < transform.position.y - 5) return;
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)) return;
            if (Input.GetKey(KeyCode.D) && !Player.instance.rightWall)
                meshRenderer.material.mainTextureOffset -= new Vector2(1, 0) * player.playerSpeed * Time.deltaTime * 0.02f;
            if (Input.GetKey(KeyCode.A) && !Player.instance.leftWall)
                meshRenderer.material.mainTextureOffset -= new Vector2(-1, 0) * player.playerSpeed * Time.deltaTime * 0.02f;
        }
    }
}
