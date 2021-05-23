using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;
using System.IO;
using UnityEngine.SceneManagement;

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
    public int PlayerAmount = 0;
    //int CurrentTurn = 1;
    //bool Input;

    bool AfterMiniGameBool = false;

    public GameObject RollButton;

    [Header("Dice Rolling Image")]
    public GameObject DiceRollingImage;
    public GameObject PlayerRollOrderImage;

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
    //bool FirstRollCompleted = false;
    public Text MovesLeftText;
    public Text MovesLeftText2;
    bool FirstRoll = true;

    public int i;
    public GameObject RollNextGameObject;
    public Text RollNextText;

    public int Player;
    public int Movess;
    public GameObject SceneSwitcher;

    public bool RollLoopBool = false;
    public int RollLoopInt = 0;

    public bool RollLoopAfterBool = false;
    public bool Movable = true;
    public bool IgnorePlacements = true;

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
    public GameObject Text6;
    public GameObject Text7;
    public GameObject Text8;
    public GameObject Text9;
    public GameObject Text10;

    [Header("MiniGame Selector")]
    public GameObject MiniGameUI;
    bool MiniGameSelectorActive = false;

    [Header("Saving/Loading Minigame Info")]
    ClassSaver CrossGameInfo;
    public Text INTTHING;

    [Header("Other")]
    SavingLoading InData;
    public GameObject[] WayPoint;
    public GameObject Camera;
    // Start is called before the first frame update
    void Start()
    {
        SceneSwitcher = GameObject.FindGameObjectWithTag("SceneSwitcher");

        LoadDataJSON();

        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        Player1Info.PlayerObject = GameObject.FindGameObjectWithTag("Player1");
        Player2Info.PlayerObject = GameObject.FindGameObjectWithTag("Player2");
        Player3Info.PlayerObject = GameObject.FindGameObjectWithTag("Player3");
        Player4Info.PlayerObject = GameObject.FindGameObjectWithTag("Player4");

        Player1Info.RollOrder = GameObject.FindGameObjectWithTag("RO1");
        Player2Info.RollOrder = GameObject.FindGameObjectWithTag("RO2");
        Player3Info.RollOrder = GameObject.FindGameObjectWithTag("RO3");
        Player4Info.RollOrder = GameObject.FindGameObjectWithTag("RO4");

        //PlayerAmount = PlayerPrefs.GetInt("PCount");
        PlayerAmount = InData.PCount;

        CreatePlayers();

        TextDie.text = PlayerList[0].Name.ToString() + "'s Roll First";
        TextDieSmall.text = "Press Space To Roll";
        TextDieInit.text = "Highest Roll Moves First!";

        //Loading Data from Previos Main Level Using.
        if (SceneSwitcher.GetComponent<SceneSwitcher>().fromMiniGame == true)
        {
            IgnorePlacements = true;
            SceneSwitcher.GetComponent<SceneSwitcher>().fromMiniGame = false;
            LoadGameData();
            LoadAfterMiniGame();
            //TextDie.enabled = false;
            //TextDieSmall.enabled = false;
            //TextDieInit.enabled = false;
        }
    }


    public int DiceRoll()
    {
        Die = Random.Range(1, 7);
        return Die;
    }


    public void CreatePlayers()
    {   
        Player1Info.Name = InData.P1Name;
        Player2Info.Name = InData.P2Name;
        Player3Info.Name = InData.P3Name;
        Player4Info.Name = InData.P4Name;

        Player1Info.PlayerNumber = 1;
        Player2Info.PlayerNumber = 2;
        Player3Info.PlayerNumber = 3;
        Player4Info.PlayerNumber = 4;

        Player1Info.RollOrder.transform.GetChild(0).GetComponent<Text>().text = Player1Info.Name;
        Player2Info.RollOrder.transform.GetChild(0).GetComponent<Text>().text = Player2Info.Name;
        Player3Info.RollOrder.transform.GetChild(0).GetComponent<Text>().text = Player3Info.Name;
        Player4Info.RollOrder.transform.GetChild(0).GetComponent<Text>().text = Player4Info.Name;

        PlayerList.Add(Player1Info);
        PlayerList.Add(Player2Info);

        Text6.GetComponent<Text>().text = InData.PCount.ToString();


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
        if (PlayerAmount == 4)
        {
            PlayerList.Add(Player3Info);
            PlayerList.Add(Player4Info);
        }
    }

    public void PlayerPickerSecond(int rolled)
    {
        //Debug.Log("PLAYER PICKER SECOND");
        PlayerList[FirstRollInt].CurrentRoll = rolled;

        TextDie.text = PlayerList[FirstRollInt].Name.ToString() + " Rolled " + PlayerList[FirstRollInt].CurrentRoll.ToString();
        PlayerList[FirstRollInt].RollOrder.transform.GetChild(3).GetComponent<Text>().text = PlayerList[FirstRollInt].CurrentRoll.ToString();
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
        //Debug.Log("PLAYER PICKER");
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
        //Debug.Log("ROLL LOOP TEXT");
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
            PickMiniGame();

            int num = TempPlayer.PlayerObject.GetComponent<PlayerMovement>().WayPointNumber;
            if (TempPlayer.PlayerObject.transform.position == WayPoint[num].transform.position)
            {
                Debug.Log("ARIVED");
            }
        }
    }

    public void RollLoop()
    {
        PlayerRollEvent.Invoke();
        rollType.Invoke(2);
    }

    public void RollLoopRest(int NewRoll)
    {
        Rolled = NewRoll;
        CamChange.Invoke(TempPlayer.PlayerNumber);
    }

    public void RollRest2()
    {
        ReOrder(Rolled, TempPlayer.PlayerNumber - 1);
    }


    public void ReOrder(int Moves, int Player)
    {
        PlayerList[Player].PlayerObject.GetComponent<PlayerMovement>().NumToMove = Moves;
    }

    void Update()
    {
        PlayerStats();




        Text7.GetComponent<Text>().text = PlayerList[0].Name;
        Text8.GetComponent<Text>().text = PlayerList[1].Name;
        if (PlayerAmount == 3)
        {
            Text9.GetComponent<Text>().text = PlayerList[2].Name;
        }
        else if (PlayerAmount == 4)
         {

            Text9.GetComponent<Text>().text = PlayerList[2].Name;
            Text10.GetComponent<Text>().text = PlayerList[3].Name;
        }

        if ((Input.GetKeyDown(KeyCode.Space)) && (FirstRoll) && (Movable))
        {
            Movable = false;
            PlayerPicker();
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


        if (AfterMiniGameBool)
        {
            RollLoopText();
            //AfterMiniLoop();
        }
        if ((Input.GetKeyDown(KeyCode.Space)) && (AfterMiniGameBool) && (Movable))
        {
            RollLoop();
            Movable = false;
        }




        //Minigame Selector
        if (MiniGameSelectorActive == true && (Input.GetKeyDown(KeyCode.Q)))
        {
            //Selected MiniGame 1
            MiniGameSelectorActive = false;
            SelectedMiniGame(1);
        }
        else if (MiniGameSelectorActive == true && (Input.GetKeyDown(KeyCode.E)))
        {
            //Selected MiniGame 2
            MiniGameSelectorActive = false;
            SelectedMiniGame(2);
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

    public void LoadDataJSON()
    {
        InData = JsonUtility.FromJson<SavingLoading>(File.ReadAllText(Application.persistentDataPath + "/saveload.json"));
    }

    public void PickMiniGame()
    {
        //Enable MiniGame Selector UI
        MiniGameUI.SetActive(true);

        //Disable Rolling Features
        RollLoopBool = false;
        Movable = false;

        //Disable Other UI
        DiceRollingImage.SetActive(false);
        PlayerRollOrderImage.SetActive(false);
        RollNextGameObject.SetActive(false);
        
        //Toggle MiniGame Selector So User Can Pick
        MiniGameSelectorActive = true;
    }

    public void SelectedMiniGame(int selected)
    {
        //Create Saving Data to Be Loaded Back into Game!

        //Load Selected MiniGame

        //Save All Required Data
        SaveGameData();

        //Load New Scene
        string newScene = "MiniGame" + selected.ToString();
        SceneManager.LoadScene(newScene);
    }

    //
    // Used when moving between scenes, saves all required Infomation
    //
    public void SaveGameData()
    {
        //This is when Minigames Are Loaded, Each Attribute will be saved.
        ClassSaver classSaver = new ClassSaver();

        string JsonPath = Application.persistentDataPath + "/Test.json";
        //Save Array of Waypoints into List.
        WayPointChecker PointCheck;
        foreach (GameObject point in WayPoint)
        {
            PointCheck = point.GetComponent<WayPointChecker>();

            WayPoint WayToList = new WayPoint();

            WayToList.Owned = PointCheck.Owned;
            WayToList.OwnedBy = PointCheck.OwnedBy;
            WayToList.Cost = PointCheck.Cost;
            WayToList.PayAmount = PointCheck.PayAmount;
            WayToList.Ownable = PointCheck.Ownable;
            WayToList.Splitter = PointCheck.Splitter;
            WayToList.WayPointSplit = PointCheck.WayPointSplit;
            WayToList.EndSplit = PointCheck.EndSplit;
            WayToList.BackTrackNum = PointCheck.BackTrackNum;

            classSaver.wayPointSaveList.Add(WayToList);
        }

        //Save Players Current Postition and Points/Crowns.
        PlayerSaver playersave = new PlayerSaver();

        playersave.P1Pos = PlayerList[0].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber;
        playersave.P1Points = PlayerList[0].Coins;
        playersave.P1Crowns = PlayerList[0].Crowns;

        playersave.P2Pos = PlayerList[1].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber;
        playersave.P2Points = PlayerList[1].Coins;
        playersave.P2Crowns = PlayerList[1].Crowns;

        Debug.Log(PlayerList[0].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber);

        if (PlayerAmount == 3 || PlayerAmount == 4)
        {
            playersave.P3Pos = PlayerList[2].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber;
            playersave.P3Points = PlayerList[2].Coins;
            playersave.P3Crowns = PlayerList[2].Crowns;
        }

        if (PlayerAmount == 4)
        {
            playersave.P4Pos = PlayerList[3].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber;
            playersave.P4Points = PlayerList[3].Coins;
            playersave.P4Crowns = PlayerList[3].Crowns;
        }

        classSaver.savePayerPos.Add(playersave);
        
        Debug.Log("SAVING");
        string JSONDATA = JsonUtility.ToJson(classSaver, true);
        File.WriteAllText(JsonPath, JSONDATA);
    }
    public void LoadGameData()
    {
        Debug.Log("Loading Data");
        //This will be ran after a minigame has been played. It will sort everyone to their relevent location/stats/Waypoints info.
        CrossGameInfo = JsonUtility.FromJson<ClassSaver>(File.ReadAllText(Application.persistentDataPath + "/Test.json"));

        //Set Player Info to What it use to be.
        PlayerList[0].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber = CrossGameInfo.savePayerPos[0].P1Pos;
        PlayerList[1].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber = CrossGameInfo.savePayerPos[0].P2Pos;

        //Set Locations for players as otherwise they slowly move there. Checking to see if anyone is on start Stil also
        if (CrossGameInfo.savePayerPos[0].P1Pos != 0)
        {
            PlayerList[0].PlayerObject.transform.position = WayPoint[CrossGameInfo.savePayerPos[0].P1Pos - 1].transform.position;
        }

        if (CrossGameInfo.savePayerPos[0].P2Pos != 0)
        {
            PlayerList[1].PlayerObject.transform.position = WayPoint[CrossGameInfo.savePayerPos[0].P2Pos - 1].transform.position;
        }

        PlayerList[0].Crowns = CrossGameInfo.savePayerPos[0].P1Crowns;
        PlayerList[1].Crowns = CrossGameInfo.savePayerPos[0].P2Crowns;

        PlayerList[0].Coins = CrossGameInfo.savePayerPos[0].P1Points;
        PlayerList[1].Coins = CrossGameInfo.savePayerPos[0].P2Points;

        if (PlayerAmount == 3 || PlayerAmount == 4)
        {
            PlayerList[2].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber = CrossGameInfo.savePayerPos[0].P3Pos;
            PlayerList[2].Crowns = CrossGameInfo.savePayerPos[0].P3Crowns;
            PlayerList[2].Coins = CrossGameInfo.savePayerPos[0].P3Points;
            if (CrossGameInfo.savePayerPos[0].P3Pos != 0)
            {
                PlayerList[2].PlayerObject.transform.position = WayPoint[CrossGameInfo.savePayerPos[0].P3Pos - 1].transform.position;
            }
            
        }

        if (PlayerAmount == 4)
        {
            PlayerList[3].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber = CrossGameInfo.savePayerPos[0].P4Pos;
            PlayerList[3].Crowns = CrossGameInfo.savePayerPos[0].P4Crowns;
            PlayerList[3].Coins = CrossGameInfo.savePayerPos[0].P4Points;
            if (CrossGameInfo.savePayerPos[0].P4Pos != 0)
            {
                PlayerList[3].PlayerObject.transform.position = WayPoint[CrossGameInfo.savePayerPos[0].P4Pos - 1].transform.position;
            }
        }

        //Loading WayPoints Into Current Array.
        for (int i = 0; i < CrossGameInfo.wayPointSaveList.Count(); i++)
        {
            WayPoint[i].GetComponent<WayPointChecker>().Owned = CrossGameInfo.wayPointSaveList[i].Owned;
            WayPoint[i].GetComponent<WayPointChecker>().OwnedBy = CrossGameInfo.wayPointSaveList[i].OwnedBy;

            if (WayPoint[i].GetComponent<WayPointChecker>().Owned = CrossGameInfo.wayPointSaveList[i].Owned == true)
            {
                WayPoint[i].GetComponent<WayPointChecker>().UpdateColor();
            }
            
        }

        Debug.Log("Data Loading Complete");
    }

    void PlayerStats()
    {

        Player1Info.RollOrder.transform.GetChild(1).GetComponent<Text>().text = Player1Info.Crowns.ToString();
        Player1Info.RollOrder.transform.GetChild(2).GetComponent<Text>().text = Player1Info.Coins.ToString();

    }

    void LoadAfterMiniGame()
    {
        //Get Order into 4 ints?
        //Organise This into the new levels?
        //274
        //Mkake it so it can be reused now throughout the game.
        //Wont have to make new shit.


        //Getting Placements from Last MiniGame
        int[] Placements = new int[4];

        Placements[0] = SceneSwitcher.GetComponent<SceneSwitcher>().First;
        Placements[1] = SceneSwitcher.GetComponent<SceneSwitcher>().Second;
        Placements[2] = SceneSwitcher.GetComponent<SceneSwitcher>().Third;
        Placements[3] = SceneSwitcher.GetComponent<SceneSwitcher>().Fourth;

        for (int i = 0; i < PlayerAmount; i++)
        {
            if (Placements[0] == i + 1)
            {
                Debug.Log("WORKED");
                PlayerList[i].Order = 1;
                Camera.transform.position = PlayerList[i].PlayerObject.transform.GetChild(0).position;
            }
            if (Placements[1] == i + 1)
            {
                PlayerList[i].Order = 2;
            }
            if (Placements[2] == i + 1)
            {
                PlayerList[i].Order = 3;
            }
            if (Placements[3] == i + 1)
            {
                PlayerList[i].Order = 4;
            }
        }

        Debug.Log("Roll Order P1 - " + PlayerList[0].Order);
        Debug.Log("Roll Order P2 - " + PlayerList[1].Order);
        //Debug.Log("Roll Order P3 - " + PlayerList[2].Order);
        //Debug.Log("Roll Order P4 - " + PlayerList[3].Order);

        //Enable Code from Before? Might be able to get it to work!
        FirstRoll = false;
        Movable = true;
        InitTextObj.SetActive(false);
        RollButton.SetActive(false);
        //RollLoopBool = true;
        AfterMiniGameBool = true;
        RollLoopInt = 0;


        //Sort Camera Out? to New Top Player?
    }
}
