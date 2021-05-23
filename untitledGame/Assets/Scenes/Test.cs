using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    GameObject Switcher;
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
}
