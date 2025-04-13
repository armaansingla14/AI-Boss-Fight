using System.Collections;
using UnityEngine;
using static UnityEditor.FilePathAttribute;


public class PlayerAttack : MonoBehaviour
{
    public Transform attackTransform;
    public LayerMask attackableLayer;
    public LayerMask enemyLayer;

    public AttackBar cooldownBar;

    public float attackDamage = 10;
    //public float attackRange = 1; //Greater than min_distance

    public KeyCode attackKey;
    private float attackTimeCounter = 0;
    public float timeBetweenAttacks = 5;
    public float attackArea=0;

    private float mapHeight = 0;
    private float mapWidth = 0;

    private Vector2 locationVector;
    private Vector2 sizeVector;

    private Vector2 topLeft;
    private Vector2 topRight;
    private Vector2 bottomLeft;
    private Vector2 bottomRight;

    private RaycastHit2D[] hits;
    // Start is called before the first frame update
    void Start()
    {

        Camera cam = Camera.main;
        mapHeight = 2f * cam.orthographicSize;
        mapWidth = mapHeight * cam.aspect;

        Debug.Log("Map Width: "+ mapWidth);
        Debug.Log("Map Height: "+ mapHeight);

        sizeVector = new Vector2(mapWidth / 3, mapHeight);
        Debug.Log("sizeVector: " + sizeVector);

        cooldownBar.setCooldownTime(timeBetweenAttacks);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Time.time - cooldownBar.getLastAttackTime() >= timeBetweenAttacks)
            {
                attack(1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (Time.time - cooldownBar.getLastAttackTime() >= timeBetweenAttacks)
            {
                attack(2);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (Time.time - cooldownBar.getLastAttackTime() >= timeBetweenAttacks)
            {
                attack(3);
            }
        }
        else{
             if (Time.time - cooldownBar.getLastAttackTime() >= 3*timeBetweenAttacks){
                attackArea=0;
             }
        }

    }

public void attack(int location)
    {

        if (Time.time - attackTimeCounter >= timeBetweenAttacks){
        attackArea=location;
        attackTimeCounter = Time.time;

        cooldownBar.setLastAttackTime(Time.time);

        //locationVector = new Vector2((mapWidth / 6) * (1 + (2 * location)), mapHeight / 2);
         locationVector = new Vector2((mapWidth / 6) * ((2 * location)-1) - mapWidth/2, 0);

        Debug.Log("Location: " + locationVector);

        // Draw the boxcast for visualization
        bottomLeft = locationVector - sizeVector / 2;
        topLeft = new Vector2(bottomLeft.x, bottomLeft.y + sizeVector.y);
        bottomRight = new Vector2(bottomLeft.x + sizeVector.x, bottomLeft.y);
        topRight = new Vector2(bottomRight.x, topLeft.y);

        // Print the coordinates of boxcast corners
        Debug.Log("Top Left: " + topLeft);
        Debug.Log("Top Right: " + topRight);
        Debug.Log("Bottom Left: " + bottomLeft);
        Debug.Log("Bottom Right: " + bottomRight);

        Debug.DrawLine(topLeft, topRight, Color.red, 0.6f);
        Debug.DrawLine(topRight, bottomRight, Color.red, 0.6f);
        Debug.DrawLine(bottomRight, bottomLeft, Color.red, 0.6f);
        Debug.DrawLine(bottomLeft, topLeft, Color.red,0.6f);

        StartCoroutine(delayedAttack());
    }

IEnumerator delayedAttack()
    {
        yield return new WaitForSeconds(0.6f);

        // Draw horizontal lines to fill the rectangle
        Debug.DrawLine(topLeft, bottomRight, Color.red, 0.15f);
        Debug.DrawLine(topRight, bottomLeft, Color.red, 0.15f);

        // Get a rectangle cast
        hits = Physics2D.BoxCastAll(locationVector, sizeVector, 0f, Vector2.down, 0.15f, enemyLayer);

        Debug.Log("Hits: " + hits.Length);

        for (int i = 0; i < hits.Length; i++)
        {
            Debug.Log(hits[i].collider.gameObject);
            hits[i].collider.gameObject.GetComponent<EntityHealth>().Damage(attackDamage);
        }

        
    }

    /*    private void attack(int location)
        {

            locationVector = new Vector2((mapWidth / 6) * (1 + (2 * location)), mapHeight / 2);


            //Get a rectangle cast
            hits = Physics2D.BoxCastAll(locationVector, sizeVector, 0f, Vector2.down, .1f, enemyLayer);


            Debug.Log(hits.Length);
            for (int i = 0; i < hits.Length; i++)
            {
                Debug.Log(hits[i].collider.gameObject);
                hits[i].collider.gameObject.GetComponent<EntityHealth>().Damage(attackDamage);
            }

            attackTimeCounter = Time.time;

        }*/

    /*    private void attack() 
        {
            hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, attackableLayer);

            Debug.Log(hits.Length);
            for (int i = 0; i < hits.Length; i++) 
            {
                hits[i].collider.gameObject.GetComponent<EntityHealth>().Damage(attackDamage);
            }

            attackTimeCounter = Time.time;
        }*/

    /*    private void OnDrawGizmosSelected() {
            //Gizmos.draww
 
        }*/
    }
}
