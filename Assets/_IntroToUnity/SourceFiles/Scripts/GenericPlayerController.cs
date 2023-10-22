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
        /*Vector3 targetPosition = m_Rigidbody.position + transform.forward * m_Animator.deltaPosition.magnitude;
        Vector3 direction = targetPosition - m_Rigidbody.position;

        CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
        Vector3 startPoint = transform.TransformPoint(capsuleCollider.center + Vector3.up * -capsuleCollider.height * 0.5f);
        Vector3 endPoint = transform.TransformPoint(capsuleCollider.center + Vector3.up * capsuleCollider.height * 0.5f);

        bool hasCollision = Physics.CheckCapsule(startPoint, endPoint, capsuleCollider.radius, ~0);

        if (!hasCollision)
        {
            m_Rigidbody.MovePosition(targetPosition);
        }*/

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
}
