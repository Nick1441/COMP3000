using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    public Vector3 InititalPosition;

    static Rigidbody rigid;
    public static Vector3 Velocity;

    public static int TestInt = 0;
    public int THIS = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Velocity = rigid.velocity;

        //This is where custom code will be implamented to Create the Dice Moving/Triggers Created.
        if (Input.GetKeyDown(KeyCode.T))
        {
            DiceRollV2();
        }
        THIS = TestInt;
    }

    public void DiceRollV2()
    {
        TestInt = 0;
        float x = Random.Range(0, 500);
        float y = Random.Range(0, 500);
        float z = Random.Range(0, 500);

        transform.position = InititalPosition;
        transform.rotation = Quaternion.identity;

        rigid.AddForce(transform.up * 500);
        rigid.AddTorque(x, y, z);
    }    
}
