using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WayPoint
{
    public bool Owned;
    public string OwnedBy;
    public int Cost;
    public int PayAmount;
    public bool Ownable;
    public bool Blank;

    public bool Splitter;
    public int WayPointSplit;

    public bool EndSplit;
    public int BackTrackNum;
}
