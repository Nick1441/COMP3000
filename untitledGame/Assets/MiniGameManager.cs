using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    GameObject Switcher;
    int PlayerAmount = 4;
    SceneSwitcher ss;

    GameObject P1;
    GameObject P2;
    GameObject P3;
    GameObject P4;
    GameObject CD;

    int CDINT = 3;
    void Start()
    {
        Switcher = GameObject.FindGameObjectWithTag("SceneSwitcher");
        ss = Switcher.GetComponent<SceneSwitcher>();

        P1 = GameObject.FindGameObjectWithTag("1");
        P2 = GameObject.FindGameObjectWithTag("2");
        P3 = GameObject.FindGameObjectWithTag("3");
        P4 = GameObject.FindGameObjectWithTag("4");

        CD = GameObject.FindGameObjectWithTag("CD");
        SortPlayers();
    }

    void SortPlayers()
    {
        PlayerAmount = ss.PlayerAmount;
        //this.gameObject.GetComponent<CollisionManager>().PlayerAmount = PlayerAmount;

        if (PlayerAmount == 2)
        {
            P3.SetActive(false);
            P4.SetActive(false);
        }

        if (PlayerAmount == 3)
        {
            P4.SetActive(false);
        }

        ChangeTime();
    }

    void ChangeTime()
    {
        if (CDINT > 0)
        {
            CD.GetComponent<Text>().text = CDINT.ToString();
            CDINT--;
            Invoke("ChangeTime", 1.0f);
        }
        else
        {
            CD.SetActive(false);
            P1.GetComponent<MiniGame1>().NewMove = 56.82f;
            P2.GetComponent<MiniGame1>().NewMove = 56.82f;
            
            if (PlayerAmount == 3 || PlayerAmount == 4)
            {
                P3.GetComponent<MiniGame1>().NewMove = 56.82f;
            }
            
            if (PlayerAmount == 4)
            {
                P4.GetComponent<MiniGame1>().NewMove = 56.82f;
            }
            
        }
        
    }
}
