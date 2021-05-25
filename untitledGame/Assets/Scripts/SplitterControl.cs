using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterControl : MonoBehaviour
{
    [Header("Splitter Info")]
    public int SplitterLocation1 = 0;
    public int SplitterEndLocation1 = 0;
    [Space]
    public int SplitterLocation2 = 0;
    public int SplitterEndLocation2 = 0;
    [Space]
    public int SplitterLocation3 = 0;
    public int SplitterEndLocation3 = 0;

    [Space]
    [Header("Chest Info")]
    public int[] Chests = { 2, 8, 23, 27, 17 };
}
