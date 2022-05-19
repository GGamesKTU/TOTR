using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollDeathScript : MonoBehaviour
{
    // Player animator.
    private Animator animator;

    // Players gun.
    private GameObject gun;

    // Guns Rigidbody
    private Rigidbody gunsRgb;

    // Guns Collider.
    private Collider gunsCol;

    // Ragdolls RigidBody array.
    private Rigidbody[] ragdollBodies;

    // Ragdolls colider array.
    private Collider[] ragdollColliders;

    /// <summary>
    /// Gets all the components that are used when activating the ragdoll
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();

        ToggleRagdoll(false);
        GetComponent<Collider>().enabled = true;
    }

    /// <summary>
    /// Players ragdoll activates.
    /// </summary>
    public void Die()
    {
        GetComponent<Collider>().enabled = false;
        ToggleRagdoll(true);
    }

    /// <summary>
    /// Toggles ragdoll on and off;
    /// </summary>
    /// <param name="state">State of the ragdoll. True = on, false = off</param>
    private void ToggleRagdoll(bool state)
    {
        //Gets the current gun held by the player and its components.
        gun = GameObject.FindGameObjectWithTag("Gun");
        gunsRgb = gun.GetComponent<Rigidbody>();
        gunsCol = gun.GetComponent<Collider>();

        animator.enabled = !state;

        // Enables gun physics and collider.
        gunsRgb.isKinematic = !state;
        gunsRgb.useGravity = state;
        gunsCol.enabled = state;


        // Activates Rigidbody of all bones
        foreach (Rigidbody rb in ragdollBodies)
        {
            rb.isKinematic = !state;
        }

        // Activates colliders of all bones.
        foreach (Collider collider in ragdollColliders)
        {
            collider.enabled = state;
        }
    }
}
