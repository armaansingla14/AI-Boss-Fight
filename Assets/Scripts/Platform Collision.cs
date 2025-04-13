using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformCollision : MonoBehaviour
{
    private GameObject player;
    private GameObject[] enemy;
    private BoxCollider2D platformCollider;

    private BoxCollider2D playerCollider;
    private BoxCollider2D[] enemyCollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        platformCollider = transform.GetComponent<BoxCollider2D>();
        playerCollider = player.transform.GetComponent<BoxCollider2D>();
    }

    void Awake()
    {
       RefreshEnemyArray();
    }

    // Update is called once per frame
    void Update()
    {
        //if players bottom is above platforms top then have collision, if not than do not!

        if (-playerCollider.size.y + player.transform.position.y <= platformCollider.size.y + transform.position.y)
        {
            Physics2D.IgnoreCollision(platformCollider, playerCollider, true);
        }
        else
        {
            Physics2D.IgnoreCollision(platformCollider, playerCollider, false);
        }


        for (int x = 0; x < enemy.Length; x++)
        {
            if (enemy[x].transform.position.y - enemyCollider[x].size.y <= transform.position.y + platformCollider.size.y)
            {
                Physics2D.IgnoreCollision(platformCollider, enemyCollider[x], true);
            }
            else
            {
                Physics2D.IgnoreCollision(platformCollider, enemyCollider[x], false);
            }

        }
    }

    private void RefreshEnemyArray()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCollider= new BoxCollider2D[enemy.Length];
        for (int x = 0; x < enemy.Length; x++)
        {
            enemyCollider[x] = enemy[x].transform.GetComponent<BoxCollider2D>();
        }
    }
}
