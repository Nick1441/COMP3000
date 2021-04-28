using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraToPlayer : MonoBehaviour
{
    //Tags For each Player
    public GameObject Cam;
    public float speed = 0.5f;

    public UnityEvent OnFinishedMove;

    GameObject P1;
    GameObject P2;
    GameObject P3;
    GameObject P4;

    bool Moving = false;
    Vector3 MoveTo;

    void Start()
    {
        P1 = GameObject.FindGameObjectWithTag("Player1");
        P2 = GameObject.FindGameObjectWithTag("Player2");
        P3 = GameObject.FindGameObjectWithTag("Player3");
        P4 = GameObject.FindGameObjectWithTag("Player4");
    }

    public void ChangeFollow(int New)
    {
        if (New == 1)
        {
            
            MoveTo = P1.transform.GetChild(0).position;
            Moving = true;
            //Cam.transform.position = P1.transform.GetChild(0).position;
            //Cam.transform.LookAt(P1.transform);
            Cam.transform.parent = P1.transform;
        }
        if (New == 2)
        {
            MoveTo = P2.transform.GetChild(0).position;
            Moving = true;
            //Cam.transform.position = P2.transform.GetChild(0).position;
            //Cam.transform.LookAt(P2.transform);
            Cam.transform.parent = P2.transform;
        }
        if (New == 3)
        {
            MoveTo = P3.transform.GetChild(0).position;
            Moving = true;
            //Cam.transform.position = P3.transform.GetChild(0).position;
            //Cam.transform.LookAt(P3.transform);
            Cam.transform.parent = P3.transform;
        }
        if (New == 4)
        {
            MoveTo = P4.transform.GetChild(0).position;
            Moving = true;
            //Cam.transform.position = P4.transform.GetChild(0).position;
            //Cam.transform.LookAt(P4.transform);
            Cam.transform.parent = P4.transform;
        }
    }

    private void Update()
    {
        if (Moving && Cam.transform.transform.position != MoveTo)
        {
            Cam.transform.position = Vector3.MoveTowards(Cam.transform.position, MoveTo, speed * Time.deltaTime);
        }
        else if (Moving && Cam.transform.transform.position == MoveTo)
        {
            Moving = false;
            OnFinishedMove.Invoke();
        }
    }
}
