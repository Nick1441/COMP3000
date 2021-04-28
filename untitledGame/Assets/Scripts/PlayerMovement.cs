using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class OnCompleteMovement : UnityEvent { }
[System.Serializable]
public class OnBuyEvent : UnityEvent<int> { }
public class PlayerMovement : MonoBehaviour
{
    public Transform[] WayPoint;
    public int WayPointNumber = 0;
    public float MoveSpeed = 1f;
    public bool MoveAllowed = false;
    public int Moves = 0;
    public Text MovesLeftText;

    public GameObject BuyPoint;
    public Text BuyText;

    public int NumToMove = -1;
    public bool Moving = false;
    public bool Moved = false;
    //bool test = true;

    public int PlayerNumber = 0;
    public UnityEvent ChangeMovable;
    public OnCompleteMovement OnComplete;
    public OnBuyEvent onBuy;

    public bool Buying = false;

    bool FirstTImeSetup = true;

    [Header("Materials")]
    public Material mat1;
    public Material mat2;
    public Material mat3;
    public Material mat4;
    Material NewMat;

    GameObject SendToPlayer;
    void Start()
    {
        //transform.position = WayPoint[WayPointNumber].transform.position;
        //Debug.Log("WORKING");
        SendToPlayer = GameObject.FindGameObjectWithTag("Controller");
        BuyPoint = GameObject.FindGameObjectWithTag("BuyWayPoint");
        BuyText = GameObject.FindGameObjectWithTag("BuyText").GetComponent<Text>();
        //BuyPoint.SetActive(false);
    }


    void Update()
    { 
        if ((BuyPoint != null) && (BuyText != null) && FirstTImeSetup)
        {
            BuyPoint.SetActive(false);
            FirstTImeSetup = false;
        }

        Movement();
        PlayerMove();

        if (Buying == true && Input.GetKeyDown(KeyCode.A))
        {
            Buying = false;
            BuyCurrent(0);
        }

        if (Buying == true && Input.GetKeyDown(KeyCode.D))
        {
            Buying = false;
            BuyCurrent(1);
        }
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

            //Calls a trigger to incrament a ++ of the number.

            //Call method now i guess.
            //set Num to Move back to -1? then its reset i guess?
        }
    }

    //Method to check if waypoint is Special or Defualt
    void WayPointCheckeer()
    {
        if (WayPoint[WayPointNumber].GetComponent<WayPointChecker>().Owned == true)
        {
            //If Checkpoint is taken, Will give Punishment?
            //cehck to see if it themself? if so ignore!

            //If landed is not the owner!
            if (WayPoint[WayPointNumber].GetComponent<WayPointChecker>().OwnedBy != PlayerNumber.ToString())
            {
                int Transfer = WayPoint[WayPointNumber].GetComponent<WayPointChecker>().PayAmount;
                int NewInt = int.Parse(WayPoint[WayPointNumber].GetComponent<WayPointChecker>().OwnedBy);

                SendToPlayer.GetComponent<GameController>().PlayerList[PlayerNumber - 1].Coins -= Transfer; //Take From Current Player
                SendToPlayer.GetComponent<GameController>().PlayerList[NewInt -1].Coins += Transfer; //Give to Owned Player

                //Display Coins Moving? Some Sort of text or animation!
                EndTurn();
            }
            else
            {
                //Show you own it nothing happens?
                EndTurn();
            }

        }

        else if ((WayPoint[WayPointNumber].GetComponent<WayPointChecker>().Owned == false) && (WayPoint[WayPointNumber].GetComponent<WayPointChecker>().Ownable == true))
        {
            //Ask Player if they want to buy?
            //if they do Buy.
            //Change some sort of animation for that type of thing.

            //Enable Text/Buttons.
            //Buttons Trigger another method which player can buy or not buy...
            //Enable a Button Allowing the User to Buy..
            BuyPoint.SetActive(true);
            string PlayerName = SendToPlayer.GetComponent<GameController>().PlayerList[PlayerNumber - 1].PlayerObject.name;
            int Cost = WayPoint[WayPointNumber].GetComponent<WayPointChecker>().Cost;
            
            BuyText.text = PlayerName + "\nDo You wish to Buy This Checkpoint for " + Cost + "?";
            
            Buying = true;


        }
    }

    void EndTurn()
    {
        OnComplete.Invoke();
        ChangeMovable.Invoke();
    }

    void BuyCurrent(int Answer)
    {
        BuyPoint.SetActive(false);
        if (Answer == 1)
        {
            SendToPlayer.GetComponent<GameController>().PlayerList[PlayerNumber - 1].Coins -= WayPoint[WayPointNumber].GetComponent<WayPointChecker>().Cost;
            WayPoint[WayPointNumber].GetComponent<WayPointChecker>().Ownable = false;
            WayPoint[WayPointNumber].GetComponent<WayPointChecker>().Owned = true;
            string OwenedByString = PlayerNumber.ToString();
            WayPoint[WayPointNumber].GetComponent<WayPointChecker>().OwnedBy = OwenedByString;

            if (PlayerNumber == 1)
            {
                NewMat = mat1;
            }
            else if (PlayerNumber == 2)
            {
                NewMat = mat2;
            }
            else if (PlayerNumber == 3)
            {
                NewMat = mat3;
            }
            else if (PlayerNumber == 4)
            {
                NewMat = mat4;
            }
            WayPoint[WayPointNumber].GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = NewMat;
            EndTurn();
        }
        else
        {
            EndTurn();
        }
    }
}
