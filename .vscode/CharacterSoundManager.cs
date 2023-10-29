using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundManager : MonoBehaviour
{
    [SerializeField] AudioSource walkingAudio; 
    [SerializeField] AudioSource specialAudio;
    [SerializeField] AudioClip[] happyClips;
    [SerializeField] AudioClip interactClip;
    //[SerializeField] AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void PauseWalkingSound(){
        walkingAudio.Pause();
    }

    void PlayWalkingSound(){
        walkingAudio.Play();
    }

    void PlayHappy(){
        PauseWalkingSound();
        
        int selectedClip = Random.random(0, 2);
        specialAudio.clip = happyClip[selectedClip];
        specialAudio.PlayOneShot();

        PlayWalkingSound();
    }

    void PlayInteract(){
        PauseWalkingSound();

        specialAudio.clip = interactClip;
        specialAudio.PlayOneShot();

        PlayWalkingSound();
    }

}
