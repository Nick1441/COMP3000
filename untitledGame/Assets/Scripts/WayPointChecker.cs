using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointChecker : MonoBehaviour
{
    [Header("Types")]
    public bool Ownable = false;
    public bool Splitter = false;
    public bool Blank = false;

    [Space]
    [Header("Trap Variables")]
    public bool Owned = false;
    public string OwnedBy = "";
    public int Cost = 0;
    public int PayAmount = 0;

    [Space]
    [Header("Splitter Variables")]
    public int WayPointSplit = 0;
    public bool EndSplit = false;

    [Space]
    [Header("OTHER")]
    public int BackTrackNum = 0;

    [Header("Materials")]
    public Material mat1;
    public Material mat2;
    public Material mat3;
    public Material mat4;
    Material NewMat;


    public void UpdateColor()
    {
        if (OwnedBy == "1")
        {
            NewMat = mat1;
        }
        else if (OwnedBy == "2")
        {
            NewMat = mat2;
        }
        else if (OwnedBy == "3")
        {
            NewMat = mat3;
        }
        else if (OwnedBy == "4")
        {
            NewMat = mat4;
        }

        this.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material = NewMat;
    }
}
