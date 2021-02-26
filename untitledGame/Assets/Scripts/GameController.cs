using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameController : MonoBehaviour
{
    //Creating Player Info
    Player Player1Info = new Player();
    Player Player2Info = new Player();
    Player Player3Info = new Player();
    Player Player4Info = new Player();

    List<Player> PlayerList = new List<Player>();

    //Setting Paramaters
    int PlayerAmount;
    int CurrentTurn = 1;
    bool Input;

    public GameObject RollButton;
    public Text TextDie;
    //public GameObject Player1;

    public int Die = 0;
    int FirstRollInt = 0;
    bool FirstRollCompleted = false;

    public int i;
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

            //PlayerList = PlayerList.OrderByDescending(w => w.CurrentRoll).ToList();

            //for (int i = 0; i < PlayerList.Count; i++)
            //{
            //    string ahh = (i + 1).ToString();
            //    PlayerList[i].RollOrder.GetComponent<Animator>().SetTrigger(ahh);
            //}
        }
    }

    public void Order()
    {
        ReOrder(2, 0);
        //ReOrder(0, 0);
    }


    public void ReOrder(int Moves, int Player)
    {
        //Debug.Log(Moves);
        //Debug.Log(Player);

        //for (i = 0; i <= Moves; i++)  //GOES UP BEFORE IT GETS TO NEXT ONE?
        //{
        PlayerList[0].PlayerObject.GetComponent<PlayerMovement>().NumToMove = Moves;
        //Debug.Log(PlayerList[0].PlayerObject.GetComponent<PlayerMovement>().Moved);

        ////As clicked once, always clicked.
        //if (PlayerList[0].PlayerObject.GetComponent<PlayerMovement>().Moved == true)
        //{
        //    PlayerList[0].PlayerObject.GetComponent<PlayerMovement>().Moved = false;
        //    if (Moves > 0)
        //    {
        //        ReOrder(Moves - 1, 0);
        //    }
            
        //}
        //}

        //i = 0;
        //while (i <= Moves)
        //{
        //    Debug.Log(i);
        //    PlayerList[0].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber++;

        //    if (PlayerList[0].PlayerObject.GetComponent<PlayerMovement>().Moved == true)
        //    {
        //        //i++;  //ONLY WANT IT TO INCREASE HERE NOT ALWAYS
        //        PlayerList[0].PlayerObject.GetComponent<PlayerMovement>().Moved = false;
        //        i++;
        //    }
        //}

        //CHANGE IT FROM A FOR TO IF? OR WHILE> AS THEY DONT WAIT FOR THEM TO SORT ITSELF OUT?
    }


}
