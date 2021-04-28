using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointChecker : MonoBehaviour
{
    public bool Owned = false;
    public string OwnedBy = "";
    public int Cost = 0;
    public int PayAmount = 0;
    public bool Ownable = false;

    public bool Splitter = false;
    public int WayPointSplit = 0;

    public bool EndSplit = false;
    public int BackTrackNum = 0;


    void Update()
    {
        
    }
}
