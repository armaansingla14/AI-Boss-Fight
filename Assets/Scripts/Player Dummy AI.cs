using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDummyAI : MonoBehaviour
{
    private GameObject[] enemies;
    private PlayerMovement playerMovement;
    public int Smartness = 0;
    bool randomMovement = false;
    bool smartMovement = false;
    bool attack = false;
    bool smartAttack = false;
    bool attacking =false;
    //Will be used to determine length of an action in s 
    float decisionTime;
    bool jumping=false;
    int rnd;
    float time;
    float attackCooldown;
    float attackCooldownTime;
    // Start is called before the first frame update
    void Start()
    {
       
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        playerMovement = transform.GetComponent<PlayerMovement>();
    
        rnd = Random.Range(0, 100);
        switch (Smartness)
        {
            case 0:
                print("Hi I am a training dummy");
                break;
            case 1:
                randomMovement = true;
                decisionTime = 2;
                break;
            case 2:
                randomMovement = true;
                attack = true;
                decisionTime = 2;
                break;
            case 3:
                randomMovement = true;
                smartAttack = true;
                decisionTime = 2;
                break;
            case 4:
                smartMovement = true;
                smartAttack = true;
                decisionTime = 2;
                attackCooldownTime=1;
                attackCooldown=attackCooldownTime;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        playerMovement = transform.GetComponent<PlayerMovement>();
        if (enemies.Length<1){
            return;
        }
        time += Time.deltaTime;
        if (time >= decisionTime)
        {
            rnd = Random.Range(0, 100);
            if (attack)
            {
                AttackChoice();
            }

            time = 0;
        }

        if (smartAttack)
        {
            SmartAttack();
        }

        if (randomMovement)
        {
            PickAction();
        }

        if (smartMovement && !jumping)
        {
            SmartMovement();
        }

        if(transform.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("Ground"))){
            jumping=false;
        }
        else{
            jumping=true;
        }

        if (attacking){
            attackCooldown-=Time.deltaTime;
            if (attackCooldown<=0){
                attacking=false;
                attackCooldown=attackCooldownTime;
            }
        }

    }

    private void PickAction()
    {
        if (rnd <= 40)
        {
            playerMovement.MoveLeft();
        }
        else if (rnd <= 80)
        {
            playerMovement.MoveRight();
        }
        else if (rnd > 80)
        {
            playerMovement.Jump();
        }
    }
    private void AttackChoice()
    {
        int attackRan = Random.Range(0, 100);
        if (attackRan <= 50)
        {
            //insert player attack method
            print("I attacked!");
        }
    }

    private void SmartAttack()
    {
       int rnd2 = Random.Range(1, 4);
       transform.GetComponent<PlayerAttack>().attack(rnd2);
    }

    private void SmartMovement()
    {
        GameObject closestEnemy = FindNearestEnemy();
        float enemyPosition = closestEnemy.transform.position.x;
        float distanceFromEnemy = enemyPosition - transform.position.x;

        if (distanceFromEnemy > 2.9 || distanceFromEnemy < -2.9)
        {
            if (enemyPosition > transform.position.x)
            {
                playerMovement.MoveLeft();
            }
            else if (enemyPosition < transform.position.x)
            {
                playerMovement.MoveRight();
            }
        }
        else if (distanceFromEnemy<2.9 && distanceFromEnemy>0){
            playerMovement.MoveRight();
            playerMovement.Jump();
        }
        else if (distanceFromEnemy>-2.9 && distanceFromEnemy<0){
            playerMovement.MoveLeft();
            playerMovement.Jump();
        }



    }

    private GameObject FindNearestEnemy()
    {
        GameObject closestEnemy = enemies[0];
        float closestInX = 10000000;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].transform.position.x - transform.position.x < closestInX)
            {
                closestEnemy = enemies[i];
                closestInX = transform.position.x - enemies[i].transform.position.x;
            }
        }
        //print(closestEnemy);
        return closestEnemy;
    }
}
