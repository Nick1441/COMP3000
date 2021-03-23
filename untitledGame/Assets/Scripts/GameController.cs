using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

[System.Serializable]
public class OnCameraChange : UnityEvent<int> { }
public class GameController : MonoBehaviour
{
    //Creating Player Info
    Player Player1Info = new Player();
    Player Player2Info = new Player();
    Player Player3Info = new Player();
    Player Player4Info = new Player();

    List<Player> PlayerList = new List<Player>();

    Player TempPlayer = new Player();
    public OnCameraChange CamChange;
    //Setting Paramaters
    int PlayerAmount;
    int CurrentTurn = 1;
    //bool Input;

    public GameObject RollButton;
    public Text TextDie;
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

    public bool RollLoopBool = false;
    public int RollLoopInt = 0;
    // Start is called before the first frame update
    void Start()
    {
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

    public void PlayerPicker()
    {
        if (FirstRollInt != PlayerAmount)
        {
            PlayerList[FirstRollInt].CurrentRoll = DiceRoll();
            TextDie.text = PlayerList[FirstRollInt].Name.ToString() + "     " + PlayerList[FirstRollInt].CurrentRoll.ToString();
            FirstRollInt++;
        }
        else
        {
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

                //string ahh = (i + 1).ToString();
                //PlayerList[i].RollOrder.GetComponent<Animator>().SetTrigger(ahh);
            }

            Debug.Log(Player1Info.Order);
            Debug.Log(Player2Info.Order);
            Debug.Log(Player3Info.Order);
            Debug.Log(Player4Info.Order);

            //RollLoop();
            RollLoopBool = true;
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

            RollNextText.text = "Player " + TempPlayer.PlayerNumber.ToString() + " Roll";
            MovesLeftText2.text = TempPlayer.Name + " Rolled " + Rolled.ToString();
            RollNextGameObject.SetActive(true);

            Debug.Log(TempPlayer.PlayerNumber);
        }
        else
        {
            RollNextGameObject.SetActive(false);
            //Call MiniGame Etc??
        }
    }

    public void RollLoop()
    {
        //Roll a Number
        //Set the Player To Roll
        Rolled = DiceRoll();

        //Set Camera To Follow Specific Player
        CamChange.Invoke(TempPlayer.PlayerNumber);

        ReOrder(Rolled, TempPlayer.PlayerNumber - 1);

    }


    public void ReOrder(int Moves, int Player)
    {
        PlayerList[Player].PlayerObject.GetComponent<PlayerMovement>().NumToMove = Moves;
        //this.GetComponent<CameraController>().SetCamera(Player);
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && (FirstRoll))
        {
            PlayerPicker();
        }

        if ((Input.GetKeyDown(KeyCode.Space)) && (RollLoopBool))
        {
            RollLoop();
        }
        if (RollLoopBool)
        {
            RollLoopText();
        }

    }

    public void IncreasePlayerCount()
    {
        RollLoopInt++;
    }
}
