using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    // Why not just create a Vector3 here?
    private Vector3 movementVector3;
    public float speed = 3.5f;
    private int numCollectibles;
    public bool isGameOver = false;
    [SerializeField] private ParticleSystem explosionFX;
    [SerializeField] private Slider slider;

    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private GameObject winTextObject;

    private SoundManager soundManager;
    private Animator anim;

    private Vector3 targetPos;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private bool followTheMouse = true;

    private void OnAwake(){
        slider.value = GetComponent<AudioSource>().volume;
    }
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

    // Added to support following the mouse
    private void Update(){
        if (Input.GetMouseButton(0)){        // check if left mouse button is held down
        
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction*50, Color.yellow);

            RaycastHit hit;  // define variable to hold raycast hit information

            // check to see if raycast hits an object
            if (Physics.Raycast(ray, out hit)){
                targetPos = hit.point;      // Set target position
                isMoving = true;            // Start player movement
            }
            else{
                isMoving = false;          // stop player movement
            } 
        }
    }

    private void FixedUpdate(){
        if (followTheMouse){
            if (isMoving){
                // Move the player toward the target position
                Vector3 direction = targetPos - rb.position;
                direction.Normalize();
                rb.AddForce(direction * speed);
            }

        }
        else{
            // movement with arrow/ad keys
        
            rb.AddForce(movementVector3 * speed);    
        }


    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Pickup")){
            other.gameObject.GetComponent<PlayParticleEffect>().Play();
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
            soundManager.PlayPickup();
        }
    }

    void OnCollisionEnter(Collision collision){
//        Debug.Log($"collision.gameObject: {collision.gameObject}");

        if (!isGameOver && collision.gameObject.CompareTag("Enemy")){
            //anim = collision.gameObject.GetComponentInChildren<Animator>();
            Animator anim = collision.gameObject.GetComponentInChildren<Animator>();
            anim.SetFloat("speed_f", 0f);
            collision.gameObject.transform.LookAt(gameObject.transform);
            anim.SetTrigger("Magic");
 
            soundManager.PlayLose();
            rb.velocity = Vector3.zero;
            // Generate a Visual Particle Effect
            
            explosionFX = Instantiate(explosionFX, transform.position, Quaternion.identity);
            explosionFX.Play();

            GameOver();

            // Update the winText to display "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lost!";
            isGameOver = true;
        }

        if (collision.gameObject.CompareTag("Wall")){
            soundManager.PlayBounce();
        }
    }

    void GameOver(){
        // Attempting to wait until animation plays to delete player / enemy
        GameObject enemy = GameObject.FindWithTag("Enemy");
        Destroy(enemy, 1.5f);
        Destroy(gameObject, 1.5f);
        Destroy(explosionFX, 1);
    }
  
    void SetCountText(){
        countText.text = "Count: " + count.ToString();
        if (count >= numCollectibles){
            soundManager.PlayWin();
            isGameOver = true;
            GameOver();
            winTextObject.SetActive(true); 
        }
    }
}
