using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private AudioSource [] soundPlayer;

    // Start is called before the first frame update
    void Start()
    {
        soundPlayer = GetComponents<AudioSource>();
    }

    private void SoundRightFootstep()
    {
        soundPlayer[1].Play();
    }

    private void SoundLeftFootstep()
    {
       soundPlayer[0].Play();
    }
}
