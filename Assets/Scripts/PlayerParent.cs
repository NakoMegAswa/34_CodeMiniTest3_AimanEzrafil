


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class PlayerParent : MonoBehaviour
{

    public Animator playerAnim;
    public float speed;
    public Rigidbody playerRb;
    public GameObject[] GO;

    //time coundown
    public GameObject TimerCountDownText;
    public float timeRemaining = 10;
    private bool timeCountDown = false;

    private bool coneTouched = false;

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



    //for powerup
    private int PowerUpCounter;
    private int Totalcounter = 4;
   

    //forbox
    public bool isHitBox = false;

    //forwin
    public bool isGoalcheck = false;

    //change colour
    public Material[] playerMtrs;

    Renderer playerRdr;

    // Start is called before the first frame update
    void Start()
    {


        playerRb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
        playerRdr = GetComponent<Renderer>();

        TimerCountDownText.GetComponent<Text>().text = "Start Function";
        TimerCountDownText.GetComponent<Text>().text = "Timer CountDown: " + timeRemaining.ToString();

        GO[0].GetComponent<Renderer>().material.color = Color.red;
        GO[1].GetComponent<Renderer>().material.color = Color.red;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
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
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(0, 90, 0);
            playerAnim.SetBool("isRun", true);

        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            playerAnim.SetBool("isRun", false);
        }

        if (PowerUpCounter == Totalcounter)
        {
            timeCountDown = true;



        }

        if (Input.GetKeyDown(KeyCode.Space) && (playerParentIsOnGround == true))
        {
            playerAnim.SetTrigger("isJump");
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerParentIsOnGround = false;
            currentJump++;
            GO[0].GetComponent<Renderer>().material.color = Color.blue;
            GO[1].GetComponent<Renderer>().material.color = Color.blue;

        }
        if (transform.position.y < -5)
        {
            print("You Lose");
            SceneManager.LoadScene("EndScene");
        }

        int inttimeRemaining = (int)timeRemaining;

        if(timeCountDown == true && coneTouched == true)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                TimerCountDownText.GetComponent<Text>().text = "TimerCountDown: " + inttimeRemaining.ToString();

            }
            else
            {
                TimerCountDownText.GetComponent<Text>().text = "TimerCountDown:0";
                rotating = false;
            }
        }

        if (rotating == true)
        {
            //rotate our platform
            rotatedObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if(rotating == false)
        {
            rotatedObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        //check if goal is reached
        
        if (isGoalcheck ==true)
        {
            SceneManager.LoadScene("EndScene");
        }
        else
        {
            isGoalcheck = false;
        }
        
        
      

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayPlane"))
        {
            playerParentIsOnGround = true;
            currentJump = 0;
            GO[0].GetComponent<Renderer>().material.color = Color.red;
            GO[1].GetComponent<Renderer>().material.color = Color.red;

        }

        if (collision.gameObject.CompareTag("RotatingPlane"))
        {
            playerParentIsOnGround = true;
            currentJump = 0;
            GO[0].GetComponent<Renderer>().material.color = Color.red;
            GO[1].GetComponent<Renderer>().material.color = Color.red;
        }

        if (collision.gameObject.CompareTag("MovingPlane"))
        {
            playerParentIsOnGround = true;
            currentJump = 0;
            GO[0].GetComponent<Renderer>().material.color = Color.red;
            GO[1].GetComponent<Renderer>().material.color = Color.red;
        }
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TagCone") && !rotating)
        {
            Debug.Log("Cone is touched!");
            //coneTouched = true;
            if (timeRemaining > 0 && Totalcounter == PowerUpCounter)
            {
                coneTouched = true;
                Debug.Log("Activated Plane");


                rotating = true;
            }

        }
        else if (other.gameObject.tag == "PowerUp")
        {
            Debug.Log("PowerUpcollected");
            PowerUpCounter++;
            Destroy(other.gameObject);
        }


        if (other.gameObject.CompareTag("TagBox"))
        {
            Debug.Log("Collided with Box!!");
            isHitBox = true;
        }

        if (other.gameObject.CompareTag("isGoal"))
        {
            Debug.Log("Goal Complete, Moving to Win Scene now!!!");

            isGoalcheck = true;
        }



    }

    private void OnTriggerExit(Collider other)
    {
        //rotating = false;
    }


    void StartRun()
    {
        //transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

        playerAnim.SetBool("isRun", true);
        playerAnim.SetFloat("startRun", 0.26f);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}


    