using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantRagdollDeath : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;


    private Rigidbody[] ragdollBodies;
    private Collider[] ragdollColliders;
    private Collider[] charColiders;

    // Start is called before the first frame update
    private void Start()
    {
        
        ragdollBodies = gameObject.GetComponentsInChildren<Rigidbody>();
        ragdollColliders = gameObject.GetComponentsInChildren<Collider>();
        charColiders = gameObject.GetComponents<Collider>();
        

        ToggleRagdoll(false);
        //Invoke("Die", 10f);
    }

    public void Die()
    {
        ToggleRagdoll(true);
    }

    private void ToggleRagdoll(bool state)
    {
        animator.enabled = !state;

        foreach (Rigidbody rb in ragdollBodies)
        {

            rb.isKinematic = !state;
        }

        foreach (Collider collider in ragdollColliders)
        {
            collider.enabled = state;
        }
        foreach(Collider collider in charColiders)
        {
            collider.enabled = !state;
        }
    }
}
