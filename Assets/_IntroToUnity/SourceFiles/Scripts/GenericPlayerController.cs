using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenericPlayerController : MonoBehaviour
{
    public float TurnSpeed = 200.0f;
    public float MaxSpeed = 6.0f;
    
    private Animator m_Animator;
    Rigidbody m_Rigidbody;

    float m_Horizontal;
    float m_Vertical;

    private int m_SpeedRatioHash;
    private bool m_HasSpeedRatio;

    [SerializeField] AudioSource walkingAudio; 
    [SerializeField] AudioSource interactAudio;
    [SerializeField] AudioClip[] happyClips;
    [SerializeField] AudioClip interactClip;
    [SerializeField] AudioSource footstepSource;
    [SerializeField] AudioClip[] foliageClips;
  
    [SerializeField] ParticleSystem foliage;
    
    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();

        m_SpeedRatioHash = Animator.StringToHash("SpeedRatio");
        m_HasSpeedRatio =
            m_Animator.parameters.Any(parameter => parameter.nameHash == m_SpeedRatioHash);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_Horizontal = Input.GetAxis ("Horizontal");
        m_Vertical = Input.GetAxis ("Vertical");

        float speed = MaxSpeed * m_Vertical;

        bool isWalking =  m_Vertical > 0.1f;

        m_Animator.SetBool ("IsWalking", isWalking);
        m_Animator.SetFloat("Speed", m_Vertical * speed);

        if (isWalking && !interactAudio.isPlaying){
            PlayWalkingSound();
        }

        if (!isWalking){
            PauseWalkingSound();
        }

        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, m_Horizontal * TurnSpeed * Time.deltaTime, 0));
        m_Rigidbody.MoveRotation(transform.rotation * deltaRotation);

        if (!m_Animator.hasRootMotion)
        {
            m_Rigidbody.MovePosition (m_Rigidbody.position + transform.forward * speed * Time.deltaTime);
        }

        if (m_HasSpeedRatio)
        {
            m_Animator.SetFloat(m_SpeedRatioHash, Mathf.Abs(speed / MaxSpeed));
        }
    }
    
    void OnAnimatorMove ()
    {
        Vector3 targetPosition = m_Rigidbody.position + transform.forward * m_Animator.deltaPosition.magnitude;
        Vector3 direction = targetPosition - m_Rigidbody.position;
        float distance = m_Animator.deltaPosition.magnitude;
        RaycastHit hit;

        bool hasCollision = Physics.Raycast(m_Rigidbody.position, direction, out hit, distance);

        if (!hasCollision)
        {
            m_Rigidbody.MovePosition(targetPosition);
        }
  
        // This was the only line in the method before I started messing with it!
        //m_Rigidbody.MovePosition (m_Rigidbody.position + transform.forward * m_Animator.deltaPosition.magnitude);
    }

    void OnTriggerEnter(Collider collider){
        Debug.Log(collider);
        if (collider.gameObject.CompareTag("Food")) {
            PlayInteract();
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.CompareTag("Happy")){
            PlayHappy();
        }

    }


    void PauseWalkingSound(){
        if (walkingAudio.isPlaying)
            walkingAudio.Pause();
    }

    void PlayWalkingSound(){
        if (!walkingAudio.isPlaying)
            walkingAudio.Play();
    }

    void PlayHappy(){
        PauseWalkingSound();
        int selectedClip = UnityEngine.Random.Range(0, happyClips.Length);
        walkingAudio.PlayOneShot(happyClips[selectedClip]);
    }

    void PlayInteract(){
        PauseWalkingSound();
        interactAudio.PlayOneShot(interactClip);
        
    }

    public void PlayStep(){
        int selectedClip = UnityEngine.Random.Range(0, foliageClips.Length);
        footstepSource.PlayOneShot(foliageClips[selectedClip]);
        PlayFoliageEffect();
    }

    public void PlayFoliageEffect(){
        //ParticleSystem leaf = Instantiate(foliage, transform.position, Quaternion.identity);
        foliage.Play();
        //Destroy(leaf, 2);
    }
}
