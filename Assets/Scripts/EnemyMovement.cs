using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] Slider slider;
    private NavMeshAgent navMeshAgent;

    private void OnAwake(){
        slider.value = navMeshAgent.speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
  
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null){
            if (!player.isGameOver){
                navMeshAgent.SetDestination(player.gameObject.transform.position);
            }
        }   
    }

}
