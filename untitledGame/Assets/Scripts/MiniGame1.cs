using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class CollisionInfo : UnityEvent<int, int> { }
[System.Serializable]
public class SuicideEvent : UnityEvent<int> { }
public class MiniGame1 : MonoBehaviour
{
    public int PlayerNumber;
    //public int speed = 1;
    public Rigidbody rb;

    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public float TurnSpeed = 50.0f;
    CharacterController characterController;
    bool test = false;
    bool test2 = false;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    public float NewMove = 10.0f;
    public int PlayerLayer;

    public CollisionInfo hit;
    public SuicideEvent death;

    public void OnParticleCollision(GameObject other)
    {

        //Debug.Log("HIT");

        if (!other.CompareTag(PlayerNumber.ToString()))
        {
            if (other.layer != PlayerLayer)
            {
                //this.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
                //Destroy(other);
                //Debug.Log(PlayerNumber + " Hit " + other.transform.parent.GetComponent<MiniGame1>().PlayerNumber);


                hit.Invoke(PlayerNumber, int.Parse(other.tag));
            }

        }
        else if (other.CompareTag("Floor"))
        { }


    }


    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Wall"))
        {
            death.Invoke(PlayerNumber);
        }
    }


    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * NewMove) + (right * 1);

        if (PlayerNumber.ToString() == "1")
        {
            if (Input.GetKeyDown(KeyCode.A)) { test = true; }
            else if (Input.GetKeyUp(KeyCode.A)) { test = false; }

            if (Input.GetKeyDown(KeyCode.D)) { test2 = true; }
            else if (Input.GetKeyUp(KeyCode.D)) { test2 = false; }
        }
        if (PlayerNumber.ToString() == "2")
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) { test = true; }
            else if (Input.GetKeyUp(KeyCode.LeftArrow)) { test = false; }

            if (Input.GetKeyDown(KeyCode.RightArrow)) { test2 = true; }
            else if (Input.GetKeyUp(KeyCode.RightArrow)) { test2 = false; }
        }
        if (PlayerNumber.ToString() == "3")
        {
            if (Input.GetKeyDown(KeyCode.B)) { test = true; }
            else if (Input.GetKeyUp(KeyCode.B)) { test = false; }

            if (Input.GetKeyDown(KeyCode.M)) { test2 = true; }
            else if (Input.GetKeyUp(KeyCode.M)) { test2 = false; }
        }
        if (PlayerNumber.ToString() == "4")
        {
            if (Input.GetKeyDown(KeyCode.Keypad1)) { test = true; }
            else if (Input.GetKeyUp(KeyCode.Keypad1)) { test = false; }

            if (Input.GetKeyDown(KeyCode.Keypad3)) { test2 = true; }
            else if (Input.GetKeyUp(KeyCode.Keypad3)) { test2 = false; }
        }



        characterController.Move(moveDirection * Time.deltaTime);


        if (test)
        {

            transform.rotation *= Quaternion.Euler(0, 1.0f * TurnSpeed, 0);
        }
        if (test2)
        {

            transform.rotation *= Quaternion.Euler(0, -1.0f * TurnSpeed, 0);
        }
    }
}

