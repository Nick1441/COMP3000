using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionManager : MonoBehaviour
{
    int LastX;
    int LastY;
    int Y;
    int X;

    int PlayerAmount = 4;
    public GameObject SceneSwitcher;
    SceneSwitcher ss;

    public void Start()
    {
        SceneSwitcher = GameObject.FindGameObjectWithTag("SceneSwitcher");
        ss = SceneSwitcher.GetComponent<SceneSwitcher>();
        PlayerAmount = SceneSwitcher.GetComponent<SceneSwitcher>().PlayerAmount;
    }
    public void Info(int x, int y)
    {
        if (LastX != y && LastY != x)
        {
            LastX = x;
            LastY = y;

            X = x;
            Y = y;

            //Debug.Log("DESTROY PLAYER " + y);
            Destroy(GameObject.FindGameObjectWithTag(y.ToString()));


            if (PlayerAmount == 4)
            {
                Debug.Log("Fourth is " + y);
                ss.Fourth = y;
                PlayerAmount--;
            }
            else if (PlayerAmount == 3)
            {
                Debug.Log("Third is " + y);
                ss.Third = y;
                PlayerAmount--;
            }
            else if (PlayerAmount == 2)
            {
                Debug.Log("Second is " + y);
                ss.Second = y;
                PlayerAmount--;
                Invoke("Test", 0.1f);
            }

        }

    }

    public void Suicide(int y)
    {
        Destroy(GameObject.FindGameObjectWithTag(y.ToString()));
        if (PlayerAmount == 4)
        {
            Debug.Log("DFourth is " + y);
            ss.Fourth = y;
            PlayerAmount--;
        }
        else if (PlayerAmount == 3)
        {
            Debug.Log("DThird is " + y);
            ss.Third = y;
            PlayerAmount--;
        }
        else if (PlayerAmount == 2)
        {
            Debug.Log("DSecond is " + y);
            ss.Second = y;
            PlayerAmount--;
            Invoke("Test", 0.1f);
        }

    }

    private void Test()
    {
        if (PlayerAmount == 1)
        {
            if (GameObject.FindGameObjectWithTag("1") != null)
            {
                Debug.Log("SFirst is " + 1);
                ss.First = 1;
            }
            else if (GameObject.FindGameObjectWithTag("2") != null)
            {
                Debug.Log("SFirst is " + 2);
                ss.First = 2;
            }
            else if (GameObject.FindGameObjectWithTag("3") != null)
            {
                Debug.Log("SFirst is " + 3);
                ss.First = 3;
            }
            else if (GameObject.FindGameObjectWithTag("4") != null)
            {
                Debug.Log("SFirst is " + 4);
                ss.First = 4;
            }

            ss.fromMiniGame = true;
            Invoke("EndGame", 2.0f);
        }
    }

    void EndGame()
    {
        SceneManager.LoadScene("MainGame");
    }


}
