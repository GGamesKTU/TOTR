using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHealth : MonoBehaviour
{
    [Min(1)]
    public float HP = 100;
    private float currentHP;
    private float timer = 0f;
    private Collider[] hitColliders;
    private Animator stateController;

    // Start is called before the first frame update
    void Start()
    {

        currentHP = HP;
        hitColliders = GetComponents<Collider>();
        stateController = GetComponent<Animator>();
        Invoke("ActivateCol", 1);
        

    }

    private void ActivateCol()
    {
        foreach (Collider col in hitColliders)
        {
            col.enabled = true;
        }
    }

    public void TakeDamage(float damage)
    {
        stateController.SetBool("IsChasing", true);
        currentHP -= damage;
        if(currentHP < 0) Die();
        
    }

    private void Die()
    {
        foreach (Collider col in GetComponents<Collider>())
        {
            col.enabled = false;
        }
        GetComponent<MutantRagdollDeath>().Die();
    }

    public float GetHealth()
    {
        return HP;
    }
}
