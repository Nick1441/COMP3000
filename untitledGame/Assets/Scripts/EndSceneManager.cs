using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    public GameObject sceneSwitcher;
    SceneSwitcher ss;
    int winner = 0;


    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;

    void Start()
    {
        sceneSwitcher = GameObject.FindGameObjectWithTag("SceneSwitcher");
        ss = sceneSwitcher.GetComponent<SceneSwitcher>();
        SetPlayers();
    }

    void SetPlayers()
    {
        int[] test = new int[4];
        test[0] = ss.F1;
        test[1] = ss.F2;
        test[2] = ss.F3;
        test[3] = ss.F4;

        for (int i = 0; i < 4; i++)
        {
            if (winner < test[i])
            {
                winner = test[i];
            }
        }

        if (winner == 0)
        {

        }
        if (winner == 1)
        {

        }
        if (winner == 2)
        {

        }
        if (winner == 3)
        {

        }

    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void LoadMain()
    {
        Destroy(sceneSwitcher);
        SceneManager.LoadScene("PlayerPicker");
    }


}
