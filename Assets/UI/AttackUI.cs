using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AttackBar : MonoBehaviour
{
    public Image image;
    private float cooldownTime;
    private float lastAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        lastAttackTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float timeBetweenAttacks = Time.time - lastAttackTime;

        image.fillAmount = timeBetweenAttacks <= cooldownTime ? timeBetweenAttacks / cooldownTime : 1;
    }

    public void setCooldownTime(float timeBetweenAttacks) {
        cooldownTime = timeBetweenAttacks;
    }
    public void setLastAttackTime(float time) {
        lastAttackTime = time;
    }

    public float getLastAttackTime(){
        return lastAttackTime;
    }
}
