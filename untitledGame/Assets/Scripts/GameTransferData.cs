using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTransferData : MonoBehaviour
{
    public List<Player> PlayerListSwitch = new List<Player>();
    public int Four = 0;

    public bool LoadingMainGame = false;
    public bool LoadingMiniGame1 = false;
    public bool LoadingMiniGame2 = false;

    GameObject Controller;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        Controller = GameObject.FindGameObjectWithTag("Controller");
    }

    // Update is called once per frame
    public void SwitchScene()
    {
        int i = 1;
        //Setting PlayerList to This Version. Will not Get Destroyed When Moving Scenes.
        //PlayerListSwitch = Controller.GetComponent<GameController>().PlayerList;



        string Next = "";

        if (i == 0)
        {
            Next = "MainGame";
            LoadingMainGame = true;
        }
        else if (i == 1)
        {
            Next = "MiniGame1";
            LoadingMiniGame1 = true;
            //Controller.GetComponent<GameController>().SaveData();

            int PA = PlayerPrefs.GetInt("PCount");
            PlayerPrefs.SetInt("P1WP", Controller.GetComponent<GameController>().PlayerList[0].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber);
            PlayerPrefs.SetInt("P2WP", Controller.GetComponent<GameController>().PlayerList[1].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber);

            if (PA == 3)
            {
                PlayerPrefs.SetInt("P3WP", Controller.GetComponent<GameController>().PlayerList[2].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber);
            }
            else if (PA == 4)
            {
                PlayerPrefs.SetInt("P3WP", Controller.GetComponent<GameController>().PlayerList[2].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber);
                PlayerPrefs.SetInt("P4WP", Controller.GetComponent<GameController>().PlayerList[3].PlayerObject.GetComponent<PlayerMovement>().WayPointNumber);
            }
        }
        else
        {
            Next = "MiniGame2";
            LoadingMiniGame2 = true;
        }

        SceneManager.LoadScene(Next);
    }
}
