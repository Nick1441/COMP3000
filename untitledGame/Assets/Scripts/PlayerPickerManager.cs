using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerPickerManager : MonoBehaviour
{
    //Creating Thrid Player
    [Header("Player 3 Items")]
    public GameObject Player3Add;
    public GameObject Player3;
    public bool Player3Bool = false;

    //Creating Fourth Player
    [Header("Player 4 Items")]
    public GameObject Player4Add;
    public GameObject Player4;
    public bool Player4Bool = false;

    [Header("Player Names Texts")]
    public GameObject Player1Name;
    public GameObject Player2Name;
    public GameObject Player3Name;
    public GameObject Player4Name;

    string SaveP1Name = "";
    string SaveP2Name = "";
    string SaveP3Name = "";
    string SaveP4Name = "";

    // Start is called before the first frame update
    void Start()
    {
        //Hiding Added Player Images.
        Player3Add.SetActive(true);
        Player4Add.SetActive(true);

        //Showing The Add Player Images.
        Player3.SetActive(false);
        Player4.SetActive(false);
    }

    //Tersting Purposes
    public void StartGame()
    {
        string name;

        if (Player1Name.GetComponent<Text>().text == "")
        {
            name = "Player1";
            PlayerPrefs.SetString("P1Name", name);
        }
        else
        {
            name = Player1Name.GetComponent<Text>().text;
            PlayerPrefs.SetString("P1Name", name);
        }

        if (Player2Name.GetComponent<Text>().text == "")
        {
            name = "Player2";
            PlayerPrefs.SetString("P2Name", name);
        }
        else
        {
            name = Player2Name.GetComponent<Text>().text;
            PlayerPrefs.SetString("P2Name", name);
        }

        PlayerPrefs.SetInt("PCount", 2);

        if (Player3Bool)
        {
            if (Player3Name.GetComponent<Text>().text == "")
            {
                name = "Player3";
                PlayerPrefs.SetString("P3Name", name);
            }
            else
            {
                name = Player3Name.GetComponent<Text>().text;
                PlayerPrefs.SetString("P3Name", name);
            }
            PlayerPrefs.SetInt("PCount", 3);
        }
        if (Player4Bool)
        {
            if (Player4Name.GetComponent<Text>().text == "")
            {
                name = "Player4";
                PlayerPrefs.SetString("P4Name", name);
            }
            else
            {
                name = Player4Name.GetComponent<Text>().text;
                PlayerPrefs.SetString("P4Name", name);
            }

            PlayerPrefs.SetInt("PCount", 4);
        }

        SceneManager.LoadScene("MainGame");
    }



    //
    // Addding New Players
    //
    public void AddPlayer3()
    {
        Player3Bool = true;

        Player3Add.SetActive(false);
        Player3.SetActive(true);
    }

    public void AddPlayer4()
    {
        if (Player3Bool)
        {
            Player4Bool = true;

            Player4Add.SetActive(false);
            Player4.SetActive(true);
        }
    }



    //
    //Remvoing Players
    //
    public void RemovePlayer3()
    {
        if (!Player4Bool)
        {
            Player3Bool = false;
            Player3Add.SetActive(true);
            Player3.SetActive(false);
        }
    }

    public void RemovePlayer4()
    {
        Player4Bool = false;
        Player4Add.SetActive(true);
        Player4.SetActive(false);
    }



}
