using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXscript : MonoBehaviour
{
    public AudioSource walking;

    public void playWalking()
    {
        walking.Play();
    }
}
