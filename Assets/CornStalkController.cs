using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornStalkController : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        // Get the Animator component on startup
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is the player
        if (other.CompareTag("Player"))
        {
            // Trigger the animation on the prefab
            anim.SetTrigger("WalkBy");

        }
    }
}
