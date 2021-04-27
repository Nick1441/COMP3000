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
        if (Switcher.GetComponent<GameTransferData>().LoadingMiniGame1 == true)
        {
            Switcher.GetComponent<GameTransferData>().LoadingMiniGame1 = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NOW()
    {
        Switcher.GetComponent<GameTransferData>().LoadingMainGame = true;
        SceneManager.LoadScene("MainGame");
    }
}
