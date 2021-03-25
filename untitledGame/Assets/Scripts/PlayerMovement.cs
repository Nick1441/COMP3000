using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class OnCompleteMovement : UnityEvent { }
public class PlayerMovement : MonoBehaviour
{
    public Transform[] WayPoint;
    public int WayPointNumber = 0;
    public float MoveSpeed = 1f;
    public bool MoveAllowed = false;
    public int Moves = 0;
    public Text MovesLeftText;

    public int NumToMove = -1;
    public bool Moving = false;
    public bool Moved = false;
    bool test = true;

    public int PlayerNumber = 0;

    public OnCompleteMovement OnComplete;
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
            //Debug.Log("Moving");
        }
        else
        {
            Moved = true;
            //Debug.Log("Moved");
        }
    }

    public int ReturnMoves()
    {
        return NumToMove;
    }

    void Movement()
    {
        if (Moved && NumToMove > 0)
        {
            NumToMove--;
            WayPointNumber++;
            MovesLeftText.text = "Moves Left" + NumToMove.ToString();
        }
        else if (NumToMove == 0 && transform.position == WayPoint[WayPointNumber].transform.position)
        {
            //Debug.Log("Finished!");
            NumToMove = -1;
            WayPointCheckeer();
            OnComplete.Invoke();
            //Calls a trigger to incrament a ++ of the number.

            //Call method now i guess.
            //set Num to Move back to -1? then its reset i guess?
        }
    }

    //Method to check if waypoint is Special or Defualt
    void WayPointCheckeer()
    {

    }    
}
