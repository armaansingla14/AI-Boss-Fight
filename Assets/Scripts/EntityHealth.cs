using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityHealth : MonoBehaviour
{
    public HealthBar bar;
    public float maxHealth;
    private float health;

    private void Start() {
        health = maxHealth;
        // Debug.Log("Health: " + health);
    }


    public float getHealth() {
        return health;
    }
    
    public void Damage(float amount) {
        health -= amount;
        Debug.Log(gameObject.name+" Health: " + health + " (Took "+amount+" damage)");
        //bar.updateHealth(health/maxHealth);

        if (health <= 0) {
            Debug.Log(gameObject.name + " died");
            Die();
        }
    }

    public void Die()
    {
        if (gameObject.tag == "Player") 
        {
            //Lose
            Debug.Log("Lose");
            SceneManager.LoadScene("Loss Screen");

        }
        else if (gameObject.tag == "Enemy")
        {
            //Win
            Debug.Log("Win");
            SceneManager.LoadScene("Win Screen");
        }
        // Debug.Log("Died");

    }
}

