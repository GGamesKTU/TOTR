using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameOver : MonoBehaviour
{
    // Player Controller script.
    PlayerController pc;

    // IK Handler script.
    IkHandler ikh;

    // Animation controller script.
    AnimationStateController2D asc;

    // Ragdoll death script.
    RagdollDeathScript rgd;

    Shooting shot;

    // Game over UI object
    public GameObject GOUI;

    // Game HUD object
    public GameObject GHUD;

    /// <summary>
    /// Gets all components needed to be turned off or used when player dies.
    /// </summary>
    private void Start()
    {
        pc = GetComponent<PlayerController>();
        ikh = GetComponent<IkHandler>();
        asc = GetComponent<AnimationStateController2D>();
        rgd = GetComponent<RagdollDeathScript>();
        shot = GetComponent<Shooting>();

        //Invoke(nameof(Over), 5f);
    }

    /// <summary>
    /// Ends player controls and animations and enables players ragdolls.
    /// </summary>
    public void Over()
    {
        GHUD.SetActive(false);
        GOUI.SetActive(true);
        pc.enabled = false;
        ikh.enabled = false;
        asc.enabled = false;
        shot.enabled = false;
        rgd.Die();

        string path = Application.persistentDataPath + "/stats.data";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
