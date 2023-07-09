using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollScript : MonoBehaviour
{
    public Animator anim;
    public Rigidbody[] bones;

    public PlayerMovement movement;
    void Start()
    {
        bones = GetComponentsInChildren<Rigidbody>();
        foreach (var b in bones)
        {
            b.isKinematic = true;
        }
    }

    [ContextMenu("Ragdoll")]
    public void Ragdoll()
    {
        anim.enabled = false;

        foreach(var b in bones)
        {
            b.isKinematic = false;
        }

        movement.enabled = false;

    }
}
