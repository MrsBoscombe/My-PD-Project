using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem particleEffect;

    public void Play(){
        particleEffect.Play();
    }
}
