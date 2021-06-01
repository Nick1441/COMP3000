using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    public bool fromMainLevel = false;
    public bool fromMiniGame = false;

    public int First = 0;
    public int Second = 0;
    public int Third = 0;
    public int Fourth = 0;

    public int LastChestLocation;

    public int PlayerAmount = 0;

    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
