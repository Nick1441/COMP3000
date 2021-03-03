using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToPlayer : MonoBehaviour
{
    //Tags For each Player
    GameObject Player = new GameObject();

    public string PlayersTag = "";

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag(PlayersTag);
        transform.LookAt(Player.transform);
    }
}
