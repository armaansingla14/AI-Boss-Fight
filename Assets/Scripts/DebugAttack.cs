using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugAttack : MonoBehaviour
{
    public KeyCode attackKey;
    public EntityHealth enemyHealth;

    // Update is called once per frame
    void Update()
    {
        //Debug attack to see if health updates
        if(Input.GetKeyUp(attackKey)) {
            enemyHealth.Damage(Random.Range(1, 20));
        }
    }


}
