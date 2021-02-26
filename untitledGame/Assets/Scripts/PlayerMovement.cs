using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] WayPoint;
    public int WayPointNumber = 0;
    public float MoveSpeed = 1f;
    public bool MoveAllowed = false;
    public int Moves = 0;

    public int NumToMove = 0;
    public bool Moving = false;
    public bool Moved = false;
    bool test = true;
    void Start()
    {
        //transform.position = WayPoint[WayPointNumber].transform.position;
    }

    void Update()
    {
        Movement();
        PlayerMove();
    }

    public void PlayerMove()
    {
        if (transform.position != WayPoint[WayPointNumber].transform.position)
        {
            Moved = false;
            transform.position = Vector3.MoveTowards(transform.position, WayPoint[WayPointNumber].transform.position, MoveSpeed * Time.deltaTime);
            Debug.Log("Moving");
        }
        else
        {
            Moved = true;
            //Moved = false;
            Debug.Log("Moved");
        }
    }

    void Movement()
    {
        if (Moved && NumToMove != 0)
        {
            NumToMove--;
            WayPointNumber++;
        }
    }
}
