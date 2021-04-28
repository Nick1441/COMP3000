using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

[System.Serializable]
public class OnCameraChange : UnityEvent<int> { }
[System.Serializable]
public class OnPlayerRollType : UnityEvent<int> { }


public class GameController : MonoBehaviour
{
    public UnityEvent PlayerRollEvent;
    public UnityEvent SwitchOnce;

    //Creating Player Info
    Player Player1Info = new Player();
    Player Player2Info = new Player();
    Player Player3Info = new Player();
    Player Player4Info = new Player();

    public List<Player> PlayerList = new List<Player>();

    Player TempPlayer = new Player();
    public OnCameraChange CamChange;
    public OnPlayerRollType rollType;
    //Setting Paramaters
    int PlayerAmount;
    int CurrentTurn = 1;
    //bool Input;

    public GameObject RollButton;

    [Header("Initial Roll Items")]
    public GameObject InitTextObj;
    public Text TextDie;
    public Text TextDieSmall;
    public Text TextDieInit;

    [Header("Player Names Texts")]
    //public GameObject Player1;
    int Rolled = 0;
    public int Die = 0;
    int FirstRollInt = 0;
    bool FirstRollCompleted = false;
    public Text MovesLeftText;
    public Text MovesLeftText2;
    bool FirstRoll = true;

    public int i;
    public GameObject RollNextGameObject;
    public Text RollNextText;

    public int Player;
    public int Movess;
    public GameObject SceneSwitcher;
    public GameObject[] SceneSwitcherSorter;

    public bool RollLoopBool = false;
    public int RollLoopInt = 0;

    public bool Movable = true;


    [Header("Player Stats")]
    public int Player1Coins = 0;
    public int Player2Coins = 0;
    public int Player3Coins = 0;
    public int Player4Coins = 0;

    [Header("Player Demo Text")]
    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;
    public GameObject Text4;
    public GameObject Text5;

    // Start is called before the first frame update
    void Start()
    {
            //SceneSwitcher = GameObject.FindGameObjectWithTag("SceneSwitcher");

            Player1Info.PlayerObject = GameObject.FindGameObjectWithTag("Player1");
            Player2Info.PlayerObject = GameObject.FindGameObjectWithTag("Player2");
            Player3Info.PlayerObject = GameObject.FindGameObjectWithTag("Player3");
            Player4Info.PlayerObject = GameObject.FindGameObjectWithTag("Player4");

            Player1Info.RollOrder = GameObject.FindGameObjectWithTag("RO1");
            Player2Info.RollOrder = GameObject.FindGameObjectWithTag("RO2");
            Player3Info.RollOrder = GameObject.FindGameObjectWithTag("RO3");
            Player4Info.RollOrder = GameObject.FindGameObjectWithTag("RO4");

            PlayerAmount = PlayerPrefs.GetInt("PCount");

            CreatePlayers();

            TextDie.text = PlayerList[0].Name.ToString() + "'s Roll First";
            TextDieSmall.text = "Press Space To Roll";
            TextDieInit.text = "Highest Roll Moves First!";


        SceneSwitcherSorter = GameObject.FindGameObjectsWithTag("SceneSwitcher");

        if (SceneSwitcherSorter[1] != null)
        {
            Destroy(SceneSwitcherSorter[1]);
        }

        if (SceneSwitcherSorter[0].GetComponent<GameTransferData>().LoadingMainGame)
        {
            LoadData();
            SceneSwitcherSorter[0].GetComponent<GameTransferData>().LoadingMainGame = false;
        }

    }


    public int DiceRoll()
    {
        Die = Random.Range(1, 7);
        return Die;
    }


    public void CreatePlayers()
    {
        Player1Info.Name = PlayerPrefs.GetString("P1Name");
        Player2Info.Name = PlayerPrefs.GetString("P2Name");
        Player3Info.Name = PlayerPrefs.GetString("P3Name");
        Player4Info.Name = PlayerPrefs.GetString("P4Name");

        Player1Info.PlayerNumber = 1;
        Player2Info.PlayerNumber = 2;
        Player3Info.PlayerNumber = 3;
        Player4Info.PlayerNumber = 4;

        PlayerList.Add(Player1Info);
        PlayerList.Add(Player2Info);

        Player1Info.RollOrder.transform.GetChild(0).GetComponent<Text>().text = Player1Info.Name;
        Player2Info.RollOrder.transform.GetChild(0).GetComponent<Text>().text = Player2Info.Name;
        Player3Info.RollOrder.transform.GetChild(0).GetComponent<Text>().text = Player3Info.Name;
        Player4Info.RollOrder.transform.GetChild(0).GetComponent<Text>().text = Player4Info.Name;

        if (PlayerAmount == 4)
        {
            PlayerList.Add(Player3Info);
            PlayerList.Add(Player4Info);
        }
        if (PlayerAmount == 3)
        {
            PlayerList.Add(Player3Info);
            Player4Info.PlayerObject.SetActive(false);
            Player4Info.RollOrder.SetActive(false);
        }
        if (PlayerAmount == 2)
        {
            Player3Info.PlayerObject.SetActive(false);
            Player4Info.PlayerObject.SetActive(false);

            Player3Info.RollOrder.SetActive(false);
            Player4Info.RollOrder.SetActive(false);
        }
    }

    public void PlayerPickerSecond(int rolled)
    {
        PlayerList[FirstRollInt].CurrentRoll = rolled;

        TextDie.text = PlayerList[FirstRollInt].Name.ToString() + " Rolled " + PlayerList[FirstRollInt].CurrentRoll.ToString();

        if (FirstRollInt + 1 == PlayerAmount)
        {
            TextDieSmall.text = "Press Space To Start Play!";
        }
        else
        {
            TextDieSmall.text = PlayerList[FirstRollInt + 1].Name.ToString() + ", Press Space To Roll";
        }


        FirstRollInt++;
    }

    public void PlayerPicker()
    {
        if (FirstRollInt != PlayerAmount)
        {
            TextDieInit.GetComponent<Text>().enabled = false;

            PlayerRollEvent.Invoke();
            rollType.Invoke(1);
        }
        else
        {
            InitTextObj.SetActive(false);
            RollButton.SetActive(false);
            FirstRoll = false;
            
            List<Player> PlayerList2 = new List<Player>();
            PlayerList2 = PlayerList.OrderByDescending(w => w.CurrentRoll).ToList();

            //Sorts List and Sets Order Number According To Roll.
            for (int i = 0; i < PlayerList2.Count; i++)
            {
                if (PlayerList2[i].PlayerNumber == 1)
                {
                    PlayerList[0].Order = i + 1;
                }
                if (PlayerList2[i].PlayerNumber == 2)
                {
                    PlayerList[1].Order = i + 1;
                }
                if (PlayerList2[i].PlayerNumber == 3)
                {
                    PlayerList[2].Order = i + 1;
                }
                if (PlayerList2[i].PlayerNumber == 4)
                {
                    PlayerList[3].Order = i + 1;
                }
            }

            //RollLoop();
            RollLoopBool = true;
            Movable = true;
            
        }
    }

    public void Order()
    {
        ReOrder(Movess, Player - 1);
    }

    public void RollLoopText()
    {
        if (RollLoopInt != PlayerAmount)
        {
            for (int i = 0; i < PlayerList.Count; i++)
            {
                if (PlayerList[i].Order == RollLoopInt + 1)
                {
                    TempPlayer = PlayerList[i];
                }
            }

            RollNextText.text = TempPlayer.Name.ToString() + "'s Roll";
            MovesLeftText2.text = TempPlayer.Name + " Rolled " + Rolled.ToString();
            RollNextGameObject.SetActive(true);

            //Debug.Log(TempPlayer.PlayerNumber);
        }
        else
        {
            RollNextGameObject.SetActive(false);
            Debug.Log("Hit Last?");
            Text5.SetActive(true);
            //SET SO CAN ONLY CALL ONCE< THIS IS WHERE THE OPTION TO LOAD NEXT SCEBNE IS! CHANGE IT UP ALSO SO THAT PLAYERS CANT ROLL
            //Call MiniGame Etc??
        }
    }

    public void RollLoop()
    {
        //Roll a Number
        //Set the Player To Roll
        //Rolled = DiceRoll();


        PlayerRollEvent.Invoke();
        rollType.Invoke(2);

        //CamChange.Invoke(TempPlayer.PlayerNumber);

        //ReOrder(Rolled, TempPlayer.PlayerNumber - 1);
        //Set Camera To Follow Specific Player
    }

    public void RollLoopRest(int NewRoll)
    {
        Debug.Log("NEW ROLE - " + NewRoll);
        Rolled = NewRoll;
        CamChange.Invoke(TempPlayer.PlayerNumber);

        ReOrder(Rolled, TempPlayer.PlayerNumber - 1);
    }


    public void ReOrder(int Moves, int Player)
    {
        PlayerList[Player].PlayerObject.GetComponent<PlayerMovement>().NumToMove = Moves;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && (FirstRoll) && (Movable))
        {
            Movable = false;
            PlayerPicker();
            
            Debug.Log("TRIGGERED");
        }

        if ((Input.GetKeyDown(KeyCode.Space)) && (RollLoopBool) && (Movable))
        {
            RollLoop();
            Movable = false;
        }
        if (RollLoopBool)
        {
            RollLoopText();
        }

        Debug.Log(PlayerList[0].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber.ToString());


        //Player1Coins = PlayerList[0].Coins;
        //Player2Coins = PlayerList[1].Coins;
        //Player3Coins = PlayerList[2].Coins;
        //Player4Coins = PlayerList[3].Coins;

        Text1.GetComponent<Text>().text = "Player 1 Coins = " + PlayerList[0].Coins.ToString();
        Text2.GetComponent<Text>().text = "Player 2 Coins = " + PlayerList[1].Coins.ToString();
        if (PlayerAmount == 3)
        {
            Text3.GetComponent<Text>().text = "Player 3 Coins = " + PlayerList[2].Coins.ToString();
        }
        else if (PlayerAmount == 4)
        {
            Text3.GetComponent<Text>().text = "Player 3 Coins = " + PlayerList[2].Coins.ToString();
            Text4.GetComponent<Text>().text = "Player 4 Coins = " + PlayerList[3].Coins.ToString();
        }

    }

    public void IncreasePlayerCount()
    {
        RollLoopInt++;
    }

    public void MoveBool()
    {
        Movable = true;
    }



    void LoadData()
    {
        //Setting the List to the current Data.
        //PlayerList.Clear();
        //PlayerList = SceneSwitcher.GetComponent<GameTransferData>().PlayerListSwitch;


        //Setting the position to the last set data.
        //PlayerList[0].PlayerObject.transform.position = PlayerList[0].PlayerObject.GetComponent<PlayerMovement>().WayPoint[PlayerList[0].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber].transform.position;
        //PlayerList[1].PlayerObject.transform.position = PlayerList[1].PlayerObject.GetComponent<PlayerMovement>().WayPoint[PlayerList[1].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber].transform.position;

        //if (PlayerAmount == 3)
        //{
        //    PlayerList[2].PlayerObject.transform.position = PlayerList[2].PlayerObject.GetComponent<PlayerMovement>().WayPoint[PlayerList[2].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber].transform.position;
        //}
        //else if (PlayerAmount == 4)
        //{
        //    PlayerList[2].PlayerObject.transform.position = PlayerList[2].PlayerObject.GetComponent<PlayerMovement>().WayPoint[PlayerList[2].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber].transform.position;
        //    PlayerList[3].PlayerObject.transform.position = PlayerList[3].PlayerObject.GetComponent<PlayerMovement>().WayPoint[PlayerList[3].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber].transform.position;
        //}
        //WILL NEED TO BE DONE WITH WAYPOINTS
        //WILL NEED TO BE DONE WITH CHESTS



        //Get the Waypoint Number from Player Prefs.
        //Set the Position of that one to the Waypoint.
        int WP1 = PlayerPrefs.GetInt("P1WP");
        int WP2 = PlayerPrefs.GetInt("P2WP");

        PlayerList[0].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber = WP1;
        PlayerList[1].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber = WP2;

        PlayerList[0].PlayerObject.transform.position = PlayerList[0].PlayerObject.GetComponent<PlayerMovement>().WayPoint[WP1].transform.position;
        PlayerList[1].PlayerObject.transform.position = PlayerList[1].PlayerObject.GetComponent<PlayerMovement>().WayPoint[WP2].transform.position;

        if (PlayerAmount == 3)
        {
            int WP3 = PlayerPrefs.GetInt("P3WP");

            PlayerList[2].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber = WP3;

            PlayerList[2].PlayerObject.transform.position = PlayerList[2].PlayerObject.GetComponent<PlayerMovement>().WayPoint[WP3].transform.position;
        }
        else if (PlayerAmount == 4)
        {
            int WP3 = PlayerPrefs.GetInt("P3WP");
            int WP4 = PlayerPrefs.GetInt("P4WP");

            PlayerList[2].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber = WP3;
            PlayerList[3].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber = WP4;

            PlayerList[2].PlayerObject.transform.position = PlayerList[2].PlayerObject.GetComponent<PlayerMovement>().WayPoint[WP3].transform.position;
            PlayerList[3].PlayerObject.transform.position = PlayerList[3].PlayerObject.GetComponent<PlayerMovement>().WayPoint[WP4].transform.position;
        }

        
    }

    public void SaveData()
    {
        SceneSwitcherSorter[0].GetComponent<GameTransferData>().SwitchScene();
    }
}
