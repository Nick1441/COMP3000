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

    [Header("Splitter Spaces")]
    public int Splitter1;
    public int Spiltter2;
    public int Splitter3;
    [Space]
    public GameObject SplitterText;
    bool Splitting = false;
    public int SplitterNumber = 0;
    public int TempMove = 0;

    GameObject SendToPlayer;
    void Start()
    {
        //transform.position = WayPoint[WayPointNumber].transform.position;
        //Debug.Log("WORKING");
        SendToPlayer = GameObject.FindGameObjectWithTag("Controller");
        BuyPoint = GameObject.FindGameObjectWithTag("BuyWayPoint");
        BuyText = GameObject.FindGameObjectWithTag("BuyText").GetComponent<Text>();
        //BuyPoint.SetActive(false);

        SplitterText.SetActive(false);
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
            TrapBuy(0);
        }

        if (Buying == true && Input.GetKeyDown(KeyCode.D))
        {
            Buying = false;
            TrapBuy(1);
        }

        if (Splitting && Input.GetKeyDown(KeyCode.A))
        {
            SpillterExit(0);
            Splitting = false;
            SplitterText.SetActive(false);
        }

        if (Splitting && Input.GetKeyDown(KeyCode.D))
        {
            SpillterExit(1);
            Splitting = false;
            SplitterText.SetActive(false);
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

            //Checking for Anything on the Way!
            WayPointCheckerMoving();


            MovesLeftText.text = "Moves Left" + NumToMove.ToString();
        }
        else if (NumToMove == 0 && transform.position == WayPoint[WayPointNumber].transform.position)
        {
            //Finsished Moving to Location.
            NumToMove = -1;

            //Call Waypoint Checker to See if Normal or Special
            WayPointCheckerStopped();
        }
    }

    //Method to check if waypoint is Special or Defualt
    void WayPointCheckerStopped()
    {
        //Creating General Bools to shorten other if Statments.
        bool Owned = WayPoint[WayPointNumber].GetComponent<WayPointChecker>().Owned;
        bool Ownable = WayPoint[WayPointNumber].GetComponent<WayPointChecker>().Ownable;
        bool isSplitter = WayPoint[WayPointNumber].GetComponent<WayPointChecker>().Splitter;

        //Space is Owned by A Player
        if (Owned == true)
        {
            TrapOwnded();
        }

        //Space is Free to Be Owned.
        if ((Owned == false) && (Ownable == true))
        {
            TrapFree();
        }

        //Space is Nothing.
        if ((Ownable == false) && (isSplitter == false))
        {
            EndTurn();
        }
    }

    void EndTurn()
    {
        OnComplete.Invoke();
        ChangeMovable.Invoke();
    }



    void WayPointCheckerMoving()
    {
        if (WayPoint[WayPointNumber].GetComponent<WayPointChecker>().Splitter == true)
        {
            TempMove = NumToMove;
            NumToMove = -1;
            SplitterText.SetActive(true);
            Splitting = true;
        }
    }

    void SpillterExit(int Answer)
    {
        //First Splitter, DUplicate for More Splitters!
        if (WayPoint[WayPointNumber].GetComponent<WayPointChecker>().WayPointSplit == 1)
        {
            // 0 - Left, 1 - Right First Splitter, 0 = Off Main route
            if (Answer == 0)
            {
                if (TempMove == 0)
                {
                    WayPointNumber = Splitter1 - 1;
                    NumToMove = TempMove;
                }
                else if (TempMove == 1)
                {
                    WayPointNumber = Splitter1;
                    NumToMove = TempMove - 1;
                }
                else
                {
                    WayPointNumber = Splitter1 - 1;
                    NumToMove = TempMove;
                }
            }
            else
            {
                if (TempMove == 0)
                {
                    NumToMove = TempMove;
                }
                else if (TempMove == 1)
                {
                    WayPointNumber++;
                    NumToMove = TempMove - 1;
                }
                else
                {
                    NumToMove = TempMove;
                }
            }
        }
        //
        //MAKE THIS WAIT UNTIL THIS CHOOSING HAS BEEN COMPLETED>
        //
        ChangeMovable.Invoke();
    }

    //Paying Owned Trap
    void TrapOwnded()
    {
        if (WayPoint[WayPointNumber].GetComponent<WayPointChecker>().OwnedBy != PlayerNumber.ToString())
        {
            int Transfer = WayPoint[WayPointNumber].GetComponent<WayPointChecker>().PayAmount;
            int NewInt = int.Parse(WayPoint[WayPointNumber].GetComponent<WayPointChecker>().OwnedBy);

            SendToPlayer.GetComponent<GameController>().PlayerList[PlayerNumber - 1].Coins -= Transfer; //Take From Current Player
            SendToPlayer.GetComponent<GameController>().PlayerList[NewInt - 1].Coins += Transfer; //Give to Owned Player

            //Display Coins Moving? Some Sort of text or animation!
            EndTurn();
        }
        else
        {
            //Show you own it nothing happens?
            EndTurn();
        }
    }

    //Buying A Trap
    void TrapFree()
    {
        BuyPoint.SetActive(true);
        string PlayerName = SendToPlayer.GetComponent<GameController>().PlayerList[PlayerNumber - 1].PlayerObject.name;
        int Cost = WayPoint[WayPointNumber].GetComponent<WayPointChecker>().Cost;

        BuyText.text = PlayerName + "\nDo You wish to Buy This Checkpoint for " + Cost + "?";

        //Enable Bool For User to Press Buy Or Not.
        Buying = true;
    }
    void TrapBuy(int Answer)
    {
        BuyPoint.SetActive(false);
        if (Answer == 1)
        {
            //Deducting Cost of Space from Player.
            SendToPlayer.GetComponent<GameController>().PlayerList[PlayerNumber - 1].Coins -= WayPoint[WayPointNumber].GetComponent<WayPointChecker>().Cost;
            
            //Setting it to false.
            WayPoint[WayPointNumber].GetComponent<WayPointChecker>().Ownable = false;
            WayPoint[WayPointNumber].GetComponent<WayPointChecker>().Owned = true;
            string OwenedByString = PlayerNumber.ToString();
            WayPoint[WayPointNumber].GetComponent<WayPointChecker>().OwnedBy = OwenedByString;

            //Setting Material so we know who Owns it!
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
