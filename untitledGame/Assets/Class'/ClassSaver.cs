using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClassSaver
{
    public List<WayPoint> wayPointSaveList = new List<WayPoint>();

    //Doesnt Need to be a list. How i changed it still like this. Everything is stored in
    public List<PlayerSaver> savePayerPos = new List<PlayerSaver>();
}
