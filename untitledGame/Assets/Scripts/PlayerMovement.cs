using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] WayPoint;
    public int WayPointNumber = 0;
    public float MoveSpeed = 1f;
    public bool MoveAllowed = false;
    public int Moves = 0;

    void Start()
    {
        transform.position = WayPoint[WayPointNumber].transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        if (MoveAllowed == true)
        {
            MovePlayer();
        }

        if (Input.GetKeyDown("w"))
        {
            MoveAllowed = true;

        }

        if (Input.GetKeyDown("e"))
        {
            MoveAllowed = false;
        }
    }

    public void MovePlayer()
    {
        for (int i = 0; i <= Moves - 1;)
        {
            if (WayPointNumber <= WayPoint.Length - 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, WayPoint[WayPointNumber].transform.position, MoveSpeed * Time.deltaTime);

                if (transform.position == WayPoint[WayPointNumber].transform.position)
                {
                    WayPointNumber += 1;
                    i++;
                }
            }

        }
        MoveAllowed = false;
    }
}
