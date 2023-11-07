using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    // Why not just create a Vector3 here?
    private Vector3 movementVector3;
    public float speed = 0;
    private int numCollectibles;
    public bool isGameOver = false;

    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private GameObject winTextObject;

    private SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {
        winTextObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
        count = 0;
        numCollectibles = 12;       // change if the number of collectibles changes!
        SetCountText();
        soundManager = GetComponent<SoundManager>();
    }

    void OnMove(InputValue movementValue){
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

        movementVector3 = new Vector3(movementX, 0.0f, movementY);

    }

    private void FixedUpdate(){
        //Vector3 movementVector
        rb.AddForce(movementVector3 * speed);    


    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Pickup")){
            other.gameObject.GetComponent<PlayParticleEffect>().Play();
            //other.gameObject.SetActive(false);
            count++;
            SetCountText();
            soundManager.PlayPickup();
        }
    }

    void OnCollisionEnter(Collision collision){
//        Debug.Log($"collision.gameObject: {collision.gameObject}");

        if (!isGameOver && collision.gameObject.CompareTag("Enemy")){
            soundManager.PlayLose();
            // Destroy the current game object / player
            Destroy(collision.gameObject);
            Destroy(gameObject);
            // Update the winText to display "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lost!";
            isGameOver = true;
        }

        if (collision.gameObject.CompareTag("Wall")){
            soundManager.PlayBounce();
        }
    }

    void SetCountText(){
        countText.text = "Count: " + count.ToString();
        if (count >= numCollectibles){
            soundManager.PlayWin();
            isGameOver = true;
            winTextObject.SetActive(true); 
        }
    }
    
}
