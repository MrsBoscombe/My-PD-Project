using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitHandler : MonoBehaviour
{
    [SerializeField] GameObject exitSign;
    // When the player triggers this object, show the exitSign
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            exitSign.SetActive(true);
            Destroy(gameObject);
        }
    }
    
}
