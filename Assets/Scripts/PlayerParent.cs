using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerParent : MonoBehaviour
{
    
    public GameObject TimerCountDownText;
    public Animator playerAnim;

    public float speed;
   
    public Rigidbody playerRb;

    //time coundown 
    public float timeRemaining = 10;

    //forjump
    public BoxCollider col;

    public bool playerParentIsOnGround = true;
    public float jumpForce = 7;
    
    private int currentJump = 0;
    private const int MAX_JUMP = 1;

    //for rotatingplatform
    public GameObject rotatedObject;
    bool rotating = false;
    public float smoothTime = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();

        TimerCountDownText.GetComponent<Text>().text = "Start Function";

        TimerCountDownText.GetComponent<Text>().text = "Timer CountDown: " + timeRemaining.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("W is pressed");
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            playerAnim.SetBool("isRun", true);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        else if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.SetBool("isRun", false);
        }

     

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("S is pressed");
            transform.Translate(Vector3.back * Time.deltaTime * -speed);
            playerAnim.SetBool("isRun", true);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        else if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.SetBool("isRun", false);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("A is pressed");
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, -90, 0);
            playerAnim.SetBool("isRun", true);
        }

        else if (Input.GetKeyUp(KeyCode.A))
        {
            playerAnim.SetBool("isRun", false);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("D is pressed");
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, 90, 0);
            playerAnim.SetBool("isRun", true);

        }

        else if (Input.GetKeyUp(KeyCode.D))
        {
            playerAnim.SetBool("isRun", false);
        }
        //if (PowerUpCounter == Totalcounter)
        //{
        //    SceneManager.LoadScene("WinScene");
        //}

        if (Input.GetKeyDown(KeyCode.Space) && (playerParentIsOnGround))
       {
            playerAnim.SetTrigger("isJump");
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerParentIsOnGround = false;
            currentJump++;
       
       }

        

        if (timeRemaining > 0)
        {
          
            timeRemaining -= Time.deltaTime;
            TimerCountDownText.GetComponent<Text>().text = "Timer Countdown: " + timeRemaining.ToString();
        }

        else
        {
            TimerCountDownText.GetComponent<Text>().text = "Timer Countdown: 0";
        }
        
        /*
        if (timeRemaining == 0)
        {
            Debug.Log("Time is up!");
            TimerCountDownText.GetComponent<Text>().text = "Timer Countdown: 0";
        }
        */

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayPlane"))
        {
            playerParentIsOnGround = true;
            currentJump = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayPlane"))
        {
            playerParentIsOnGround = false;
        }
    }
    void StartRun()
    {
        //transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

        playerAnim.SetBool("isRun", true);
        playerAnim.SetFloat("startRun", 0.26f);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TagCone"))
        {
            Debug.Log("Activated Plane");
        }
    }

    private void OnCollision(Collision Cylinder)
    {
        if (col.gameObject.name == "RotatingPlane" && !rotating) 
        {
            //Rotate rotatedObject by 90 degrees on the Y axis
            rotating = true;
      
        }
}

    
}
