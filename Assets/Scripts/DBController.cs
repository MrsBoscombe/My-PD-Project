using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Animations were downloaded from Mixamo
    12-2-2023

*/

public class DBController : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //To duplicate this code block, select the code between this comment
        if (Input.GetKeyDown(KeyCode.M))
        {
            anim.SetTrigger("Magic");
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            anim.SetTrigger("Yawn");
        }

    }

    void EndGame(){
        anim.SetTrigger("Yawn");
    }
}
