using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem particleEffect;

    public void PlayEffect(){
        particleEffect = Instantiate(particleEffect, transform.position, 3);
        particleEffect.Play();
    }
}
