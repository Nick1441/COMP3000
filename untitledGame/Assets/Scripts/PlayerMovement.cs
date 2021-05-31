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

    [Header("GUI's")]
    public GameObject guiExtraKeys;
    public GameObject guiLooseKeys;

    public int NumToMove = -1;
    public int TempMove = 0;
    public bool Moving = false;
    public bool Moved = false;
    //bool test = true;

    public int PlayerNumber = 0;
    public UnityEvent ChangeMovable;
    public OnCompleteMovement OnComplete;
    public OnBuyEvent onBuy;

    public bool Buying = false;
    public bool Opening = false;
    public bool OpeningError = false;
    public GameObject OpenChestComplete;

    bool FirstTImeSetup = true;

    [Header("Materials")]
    public Material mat1;
    public Material mat2;
    public Material mat3;
    public Material mat4;
    Material NewMat;


    float MovementTime = 0;
    [Space]
    public GameObject SplitterText;
    bool Splitting = false;
    public int SplitterNumber = 0;
    GameObject SplitterControler;
    GameObject gameController;
    public bool test;
    bool EndSplitter = false;

    public GameObject OpenChestMenu;
    public GameObject OpenChestError;

    GameObject SendToPlayer;
    void Start()
    {
        //transform.position = WayPoint[WayPointNumber].transform.position;
        //Debug.Log("WORKING");
        gameController = GameObject.FindGameObjectWithTag("Controller");
        test = gameController.GetComponent<GameController>().IgnorePlacements;
        SendToPlayer = GameObject.FindGameObjectWithTag("Controller");
        BuyPoint = GameObject.FindGameObjectWithTag("BuyWayPoint");
        BuyText = GameObject.FindGameObjectWithTag("BuyText").GetComponent<Text>();

        SplitterText.SetActive(false);
        SplitterControler = GameObject.FindGameObjectWithTag("SPLITTERCONTROL");
    }


    void Update()
    {

        //Debug.Log(test);
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
            Splitting = false;
            SpillterExit(0);
            SplitterText.SetActive(false);
        }

        if (Splitting && Input.GetKeyDown(KeyCode.D))
        {
            Splitting = false;
            SpillterExit(1);
            SplitterText.SetActive(false);
        }

        //Skip Buyinh
        if (Opening && Input.GetKeyDown(KeyCode.A))
        {
            Opening = false;
            OpenChest(1);
        }
        //Buy
        if (Opening && Input.GetKeyDown(KeyCode.D))
        {
            Opening = false;
            OpenChest(2);
        }
        if (OpeningError && Input.GetKeyDown(KeyCode.Space))
        {
            OpeningError = false;
            OpenChest(3);
        }
    }

    public void PlayerMove()
    {
        if (transform.position != WayPoint[WayPointNumber].transform.position)
        {
            var lookPos = WayPoint[WayPointNumber + 1].position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.GetChild(1).rotation = Quaternion.Slerp(transform.GetChild(1).rotation, rotation, Time.deltaTime * 50.0f);



            Moved = false;
            transform.position = Vector3.MoveTowards(transform.position, WayPoint[WayPointNumber].transform.position, MoveSpeed * Time.deltaTime);

            this.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Moving");

            if (MovementTime <= 1.0f)
            {
                MovementTime += 0.02f;

            }

            this.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Animator>().SetFloat("Blend", MovementTime);
        }
        else
        {
            Moved = true;
            MovementTime = 0;
            this.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Animator>().SetFloat("Blend", MovementTime);
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
            test = false;
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
        if (test == false)
        {
            //Will not Trigger on Starting Point.
            if (WayPoint[WayPointNumber].GetComponent<WayPointChecker>() != null)
            {
                //Creating General Bools to shorten other if Statments.
                bool Owned = WayPoint[WayPointNumber].GetComponent<WayPointChecker>().Owned;
                bool Ownable = WayPoint[WayPointNumber].GetComponent<WayPointChecker>().Ownable;
                bool isSplitter = WayPoint[WayPointNumber].GetComponent<WayPointChecker>().Splitter;
                bool isBlank = WayPoint[WayPointNumber].GetComponent<WayPointChecker>().Blank;

                bool isPenalty = WayPoint[WayPointNumber].GetComponent<WayPointChecker>().Penalty;
                bool isExtra = WayPoint[WayPointNumber].GetComponent<WayPointChecker>().ExtraKeys;
                bool isAdvance = WayPoint[WayPointNumber].GetComponent<WayPointChecker>().AdvanceSpaces;

                //Space is Owned by A Player
                if (Owned == true)
                {
                    TrapOwnded();
                }

                //Space is Free to Be Owned.
                else if ((Owned == false) && (Ownable == true))
                {
                    //Debug.Log("TRAP FREE");
                    TrapFree();
                }

                //Space is Nothing.
                else if (isBlank == true)
                {
                    EndTurn();
                }

                //Space Advancement
                else if (isAdvance == true)
                {
                    AdvanceSpaces();
                }

                //Extra Keys! Free
                else if (isExtra == true)
                {
                    ExtraKeys();
                }

                //Take Keys Away! Not as many as Given!
                else if (isPenalty == true)
                {
                    Penalty();
                }
            }
        }
    }

    void EndTurn()
    {
        OnComplete.Invoke();
        ChangeMovable.Invoke();
    }



    void WayPointCheckerMoving()
    {
        bool isSplitter = WayPoint[WayPointNumber].GetComponent<WayPointChecker>().Splitter;
        bool isSplitterEnd = WayPoint[WayPointNumber].GetComponent<WayPointChecker>().EndSplit;
        bool isChest = WayPoint[WayPointNumber].GetComponent<WayPointChecker>().ChestSpace;
        bool isChestActive = WayPoint[WayPointNumber].GetComponent<WayPointChecker>().ChestActive;

        if ((isSplitter == true) && (isSplitterEnd == false))
        {
            Splitter();
        }

        if ((isSplitter == true) && (isSplitterEnd == true))
        {
            SplitterEnd();
        }

        if ((isChest == true) && (isChestActive == true))
        {
            ChestOpener();
        }

        //THIS WILL NOT WORK
        //Should check to see if this is last option?
        if (NumToMove == 0 && transform.position == WayPoint[WayPointNumber].transform.position)
        {
            //    EndTurn();
            Debug.Log("SHOULD ROLL NEXT TURN");
        }
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


    void Splitter()
    {
        //Seting its remaining moves to a temp
        TempMove = NumToMove;               //Remaining Moves to A Temp Move
        NumToMove = -1;                     //Setting it as No Moves Left.
        SplitterText.SetActive(true);
        Splitting = true;
    }

    void SpillterExit(int Answer)
    {
        int SplitterNum = WayPoint[WayPointNumber].GetComponent<WayPointChecker>().WayPointSplit;
        SplitterControl sControl = SplitterControler.GetComponent<SplitterControl>();
        int SplitterWayNumber = 0;

        //First Splitter, DUplicate for More Splitters!
        if (SplitterNum == 1) { SplitterWayNumber = sControl.SplitterLocation1; }
        if (SplitterNum == 2) { SplitterWayNumber = sControl.SplitterLocation2; }


        // 0 - Left, 1 - Right First Splitter, 0 = Off Main route
        //This was Incorrect for a long time. Casuing Issues for Everyone..
        if (Answer == 0)
        {
            WayPointNumber = SplitterWayNumber;
            NumToMove = TempMove;
        }
        else
        {
            NumToMove = TempMove;
        }
}

    void SplitterEnd()
    {
        int toMove = WayPoint[WayPointNumber].GetComponent<WayPointChecker>().WayPointSplit;

        int NewWay = 0;

        if (toMove == 1) { NewWay = SplitterControler.GetComponent<SplitterControl>().SplitterEndLocation1; }
        if (toMove == 2) { NewWay = SplitterControler.GetComponent<SplitterControl>().SplitterEndLocation2; }
        if (toMove == 3) { NewWay = SplitterControler.GetComponent<SplitterControl>().SplitterEndLocation3; }

        WayPointNumber = NewWay;
    }

    void ChestOpener()
    {
        TempMove = NumToMove;
        NumToMove = -1;

        if (gameController.GetComponent<GameController>().PlayerList[PlayerNumber - 1].Coins >= 25)
        {
            //Ask if want to Buy!
            Opening = true;
            OpenChestMenu.SetActive(true);
        }
        else
        {
            //Say Cant Afford it! Then Press Space to Move on!
            OpeningError = true;
            OpenChestError.SetActive(true);
        }
    }
    void OpenChest(int num)
    {
        // 1 - Skip
        // 2 - Open
        // 3 - Not Enough Points

        if (num == 1)
        {
            NumToMove = TempMove;
            OpenChestMenu.SetActive(false);

        }
        else if (num == 2)
        {
            Debug.Log("CHEST OPENED");
            OpenChestMenu.SetActive(false);
            OpenChestComplete.SetActive(true);

            gameController.GetComponent<GameController>().PlayerList[PlayerNumber - 1].Crowns++;
            gameController.GetComponent<GameController>().PlayerList[PlayerNumber - 1].Coins -= 25;
            WayPoint[WayPointNumber].GetComponent<WayPointChecker>().ChestActive = false;
            WayPoint[WayPointNumber].GetComponent<WayPointChecker>().HideChest();

            //Check to see if someone has won. Reset Chest Into another Location
            gameController.GetComponent<GameController>().CheckCrowns();
            gameController.GetComponent<GameController>().UpdateChest();

            OpeningError = true;
        }
        else if (num == 3)
        {
            NumToMove = TempMove;
            OpenChestError.SetActive(false);
            OpenChestComplete.SetActive(false);
        }

    }


    void Penalty()
    {
        //Enable GUI
        //Diable Normal GUI
        gameController.GetComponent<GameController>().PlayerList[PlayerNumber - 1].Coins -= 5;
        guiLooseKeys.SetActive(true);

        string PlayerName = gameController.GetComponent<GameController>().PlayerList[PlayerNumber - 1].PlayerObject.name;
        guiLooseKeys.transform.GetChild(0).GetComponent<Text>().text = PlayerName + " Lost 5 Coins!";

        Invoke("PenaltyCompelte", 4.0f);
    }

    void PenaltyCompelte()
    {
        guiLooseKeys.SetActive(false);
        EndTurn();
    }

    void ExtraKeys()
    {
        gameController.GetComponent<GameController>().PlayerList[PlayerNumber - 1].Coins += 10;
        guiLooseKeys.SetActive(true);

        string PlayerName = gameController.GetComponent<GameController>().PlayerList[PlayerNumber - 1].PlayerObject.name;
        guiLooseKeys.transform.GetChild(0).GetComponent<Text>().text = PlayerName + " Earnt 5 Coins!";

        Invoke("PenaltyCompelte", 4.0f);
    }

    void AdvanceSpaces()
    {
        //Set the Items to Move i guess? Will move to a blank space.
        NumToMove = 2;
        guiLooseKeys.SetActive(true);

        string PlayerName = gameController.GetComponent<GameController>().PlayerList[PlayerNumber - 1].PlayerObject.name;
        guiLooseKeys.transform.GetChild(0).GetComponent<Text>().text = PlayerName + " Earnt 2 Extra Spaces!";

        Invoke("AdvanceSpaceComplete", 2.0f);
    }

    void AdvanceSpaceComplete()
    {
        guiLooseKeys.SetActive(false);
    }
}
