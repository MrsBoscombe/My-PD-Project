using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip pickupClip;
    [SerializeField] AudioClip bounceClip;
    [SerializeField] AudioClip winClip;
    [SerializeField] AudioClip loseClip;

    [SerializeField] AudioSource oneShotAudioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayPickup(){
        oneShotAudioSource.PlayOneShot(pickupClip);
    }

    public void PlayBounce(){
        oneShotAudioSource.PlayOneShot(bounceClip);
    }

    public void PlayWin(){
        oneShotAudioSource.PlayOneShot(winClip);
    }

    public void PlayLose(){
        oneShotAudioSource.PlayOneShot(loseClip);
    }

}
