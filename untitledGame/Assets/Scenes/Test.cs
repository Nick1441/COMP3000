using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    public GameObject Switcher;
    // Start is called before the first frame update
    void Start()
    {
        Switcher = GameObject.FindGameObjectWithTag("SceneSwitcher");
    }

    public void NOW()
    {
        Switcher.GetComponent<SceneSwitcher>().fromMiniGame = true;

        Switcher.GetComponent<SceneSwitcher>().First = 2;
        Switcher.GetComponent<SceneSwitcher>().Second = 1;
        Switcher.GetComponent<SceneSwitcher>().Third = 3;
        Switcher.GetComponent<SceneSwitcher>().Fourth = 4;

        SceneManager.LoadScene("MainGame");
    }

    public void NOW2()
    {
        Switcher.GetComponent<SceneSwitcher>().fromMiniGame = true;

        Switcher.GetComponent<SceneSwitcher>().First = 1;
        Switcher.GetComponent<SceneSwitcher>().Second = 2;
        Switcher.GetComponent<SceneSwitcher>().Third = 3;
        Switcher.GetComponent<SceneSwitcher>().Fourth = 4;

        SceneManager.LoadScene("MainGame");
    }

    public void NOW31()
    {
        Switcher.GetComponent<SceneSwitcher>().fromMiniGame = true;

        Switcher.GetComponent<SceneSwitcher>().First = 3;
        Switcher.GetComponent<SceneSwitcher>().Second = 1;
        Switcher.GetComponent<SceneSwitcher>().Third = 2;
        Switcher.GetComponent<SceneSwitcher>().Fourth = 4;

        SceneManager.LoadScene("MainGame");
    }

    public void NOW32()
    {
        Switcher.GetComponent<SceneSwitcher>().fromMiniGame = true;

        Switcher.GetComponent<SceneSwitcher>().First = 2;
        Switcher.GetComponent<SceneSwitcher>().Second = 3;
        Switcher.GetComponent<SceneSwitcher>().Third = 1;
        Switcher.GetComponent<SceneSwitcher>().Fourth = 4;

        SceneManager.LoadScene("MainGame");
    }

    public void NOW41()
    {
        Switcher.GetComponent<SceneSwitcher>().fromMiniGame = true;

        Switcher.GetComponent<SceneSwitcher>().First = 4;
        Switcher.GetComponent<SceneSwitcher>().Second = 3;
        Switcher.GetComponent<SceneSwitcher>().Third = 1;
        Switcher.GetComponent<SceneSwitcher>().Fourth = 2;

        SceneManager.LoadScene("MainGame");
    }

    public void NOW42()
    {
        Switcher.GetComponent<SceneSwitcher>().fromMiniGame = true;

        Switcher.GetComponent<SceneSwitcher>().First = 2;
        Switcher.GetComponent<SceneSwitcher>().Second = 1;
        Switcher.GetComponent<SceneSwitcher>().Third = 4;
        Switcher.GetComponent<SceneSwitcher>().Fourth = 3;

        SceneManager.LoadScene("MainGame");
    }
}
