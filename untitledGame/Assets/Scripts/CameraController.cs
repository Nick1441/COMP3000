using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject P1Camera = new GameObject();
    GameObject P2Camera = new GameObject();
    GameObject P3Camera = new GameObject();
    GameObject P4Camera = new GameObject();

    int PlayerAmount;

    void Start()
    {
        //Finding all the Cameras linked to Players.
        P1Camera = GameObject.FindGameObjectWithTag("Cam1");
        P1Camera = GameObject.FindGameObjectWithTag("Cam2");
        P1Camera = GameObject.FindGameObjectWithTag("Cam3");
        P1Camera = GameObject.FindGameObjectWithTag("Cam4");

        PlayerAmount = PlayerPrefs.GetInt("PCount");
    }

    public void SetCamera(int Player)
    {
        //From 0 - 3 Normaly, so its 1 - 4 now.
        Player = Player + 1;

        if (Player == 1)
        {
            P1Camera.SetActive(true);
            P2Camera.SetActive(false);

            if (PlayerAmount == 3)
            {
                P3Camera.SetActive(false);
            }
            if (PlayerAmount == 4)
            {
                P3Camera.SetActive(false);
                P4Camera.SetActive(false);
            }
        }
        else if (Player == 2)
        {
            P1Camera.SetActive(false);
            P2Camera.SetActive(true);

            if (PlayerAmount == 3)
            {
                P3Camera.SetActive(false);
            }
            if (PlayerAmount == 4)
            {
                P3Camera.SetActive(false);
                P4Camera.SetActive(false);
            }
        }
        else if (Player == 3)
        {
            P1Camera.SetActive(false);
            P2Camera.SetActive(false);
            P3Camera.SetActive(true);

            if (PlayerAmount == 4)
            {
                P4Camera.SetActive(false);
            }
        }
        else if (Player == 4)
        {
            P1Camera.SetActive(false);
            P2Camera.SetActive(false);
            P3Camera.SetActive(false);
            P4Camera.SetActive(true);
        }
    }
}
