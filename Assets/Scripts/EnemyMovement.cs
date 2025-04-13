using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5;
    public int jumpPower = 1;
    public float riseDecceleration = 0.5f;
    public float minDistance = 1;
    public float attackRange = 5; //Greater than min_distance
    public GameObject player;
    public Transform target;
    public Transform attackTransform;
    public LayerMask attackableLayer;
    public float timeBetweenAttacks = 1;

    public float attackDamage = 10;
    private float attackTimeCounter = 0;
    private float yMovement = 0;
    private int xDirection = 0; 
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    private RaycastHit2D[] hits;
    public float movementInputX=0;
    public float hitTally=0;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
    //     // rotate to look at the player (left or right)
    //     Vector2 targetPosition = new Vector3(target.position.x, transform.position.y);
    //    // Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
    //     transform.LookAt(targetPosition);
    //     transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation

        // //movement

        // //If enemy is outside of attack range
        // if (Vector3.Distance(transform.position, target.position) > attackRange)
        // {
        //     //Move towards player
        //     transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        // }
        // //If enemy is in attack range, but outside of the minimum distance
        // else if (Vector3.Distance(transform.position, target.position) > minDistance)
        // {
        //     ////Try to attack
        //     if (Time.time - attackTimeCounter >= timeBetweenAttacks) 
        //     {
        //        Debug.Log("Attack");
        //        attack();
        //     }

        // }
        // //If enemy is too close (inside minimum distance)
        // else
        // {
        //     //move away
        //     transform.Translate(new Vector3(-1 * speed * Time.deltaTime, 0, 0));
        // }

        if (yMovement > 0)
        {
            yMovement -= riseDecceleration * Time.deltaTime * 500;
        }

        // if (player.transform.position.y > transform.position.y && rb.IsTouchingLayers(LayerMask.GetMask("Ground")) && !(player.transform.position.x > transform.position.x + 4 || player.transform.position.x < transform.position.x - 4))
        // {
        //     yMovement = jumpPower;
        // }


        //attackTimeCounter += Time.deltaTime;

        Move(movementInputX);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(xDirection * speed, yMovement);
    }

    public void attack(double inputValue)
    {
        
        if (Time.time-attackTimeCounter>3){
        // print("attack counter: "+attackTimeCounter+" Time: "+ Time.time);
        attackTimeCounter = Time.time;
        if (inputValue>0.5){
        //print("Attack");    
        hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, attackableLayer);
        //Debug.Log(hits.Length);
        
        for (int i = 0; i < hits.Length; i++)
        {
            hitTally+=1;
            hits[i].collider.gameObject.GetComponent<EntityHealth>().Damage(attackDamage);
        }
        

        }
        attackAnimation();
        }
        
    }

    public void attackAnimation() {
        animator.SetTrigger("Attack");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }

    
    private void Move(double movementDirection)
    {
        float direction; 
        if (movementDirection<0.3){
            direction=1;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (movementDirection>0.7){
            direction=1;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else{
            direction=0; 
        }
        transform.Translate(new Vector3((float)(speed*direction) * Time.deltaTime, 0, 0));    

    }
    
    public void SetXMovementDirection(double input){
        movementInputX=(float)input;
    } 

    public void Jump(double input){
         if (rb.IsTouchingLayers(LayerMask.GetMask("Ground")) && input>0)
        {
            yMovement = jumpPower;
        }
    }

}
