using SharpNeat.Network;
using SharpNeat.Phenomes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnitySharpNEAT;

/// <summary>
/// This class serves as an example template for how to create a UnitController.
/// </summary>
/// 




public class EnemyController : UnitController

{
    private EnemyMovement enemyMovement;
    GameObject player;
    float playerX = 0;
    float x = 0;
    public float rewardDist = 2;
    private float lastHealth;
    private float lastHealthEnemy;
    float playerAttack=0; 
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyMovement=transform.GetComponent<EnemyMovement>();
        lastHealth=player.GetComponent<EntityHealth>().getHealth();
        lastHealthEnemy=this.GetComponent<EntityHealth>().maxHealth;
    }

    protected override void UpdateBlackBoxInputs(ISignalArray inputSignalArray)
    {
        // Called by the base class on FixedUpdate

        // Feed inputs into the Neural Net (IBlackBox) by modifying its InputSignalArray
        // The size of the input array corresponds to NeatSupervisor.NetworkInputCount


        //Possible way 
        //Get player location, pass into the black box\
        //car uses walls proximity (walls bad)
        //This uses procimity to player (close to player good)


        playerX = player.transform.position.x;
        playerAttack = player.GetComponent<PlayerAttack>().attackArea; 
        x = transform.position.x;
        print("ATTACK DIR: " + playerAttack);
        inputSignalArray[0] = playerX;
        inputSignalArray[1] = transform.position.x;
        inputSignalArray[2] = playerAttack; //(0 for no attack 1,2,3 for sections of map)


        /* EXAMPLE */
        //inputSignalArray[0] = someSensorValue;
        //inputSignalArray[1] = someOtherSensorValue;
        //...
    }

    protected override void UseBlackBoxOutpts(ISignalArray outputSignalArray)
    {
        // Called by the base class after the inputs have been processed

        // Read the outputs and do something with them
        // The size of the array corresponds to NeatSupervisor.NetworkOutputCount
    
        enemyMovement.SetXMovementDirection((float)outputSignalArray[0]);
        enemyMovement.attack((float)outputSignalArray[1]);

        // print((float)outputSignalArray[0]);
        //enemyMovement.Jump(outputSignalArray[1]); 

       
        //someMoveSpeed = outputSignalArray[1];
        //...
    }

    public override float GetFitness()
    {
        float fitness=0;
        // Called during the evaluation phase (at the end of each trail)

        // The performance of this unit, i.e. it's fitness, is retrieved by this function.
        // Implement a meaningful fitness function here


        //For now:
        //Get player location, oif close enoiugh to player, get points

/*        playerX = player.transform.position.x;
        x = transform.position.x;*/

        // if (Mathf.Abs(x - playerX) < rewardDist) {
        //     return 1;
        // } 
        // if (10-Mathf.Abs(x - playerX)>0){
        //     fitness+= 10-Mathf.Abs(x - playerX);
        // }

        float playerHealth = player.GetComponent<EntityHealth>().getHealth();
        float enemyHealth = this.GetComponent<EntityHealth>().getHealth();

        float maxPlayerHealth = player.GetComponent<EntityHealth>().maxHealth;
        float maxEnemyHealth = this.GetComponent<EntityHealth>().maxHealth;

        float playerDamageTaken = lastHealth - playerHealth;
        float enemyDamageTaken = lastHealthEnemy - enemyHealth;

        lastHealth=playerHealth; 
        lastHealthEnemy=enemyHealth;
        //print ("ED "+ enemyDamageTaken+" PD "+ playerDamageTaken);
        fitness = playerDamageTaken - (2*enemyDamageTaken) +50;

        if (fitness<0){
            fitness=0;
        }
        
        return fitness;
    }

    protected override void HandleIsActiveChanged(bool newIsActive)
    {
        // Called whenever the value of IsActive has changed
        if (newIsActive == false)
            {
                // the unit has been deactivated, IsActive was switched to false

                // reset transform
                transform.position=new Vector3(0, 0, 0);

                // reset members
                enemyMovement.hitTally=0;
                player.GetComponent<EntityHealth>().maxHealth=7000;
                this.GetComponent<EntityHealth>().maxHealth=100;
            }

            // hide/show children 
            // the children happen to be the car meshes => we hide this Unit when IsActive turns false and show it when it turns true
            foreach (Transform t in transform)
            {
                t.gameObject.SetActive(newIsActive);
            }
        // Since NeatSupervisor.cs is making use of Object Pooling, this Unit will never get destroyed. 
        // Make sure that when IsActive gets set to false, the variables and the Transform of this Unit are reset!
        // Consider to also disable MeshRenderers until IsActive turns true again.
    }
}