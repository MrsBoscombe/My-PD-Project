using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem particleEffect;

    public void Play(){
        particleEffect = Instantiate(particleEffect, transform.position, Quaternion.identity);
        particleEffect.Play();
        //Destroy(particleEffect, 3);
        Debug.Log("ParticleEffect Instantiated and Played");
    }
}
