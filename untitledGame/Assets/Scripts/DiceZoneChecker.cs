using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InfoBack : UnityEvent<int> { }
[System.Serializable]
public class InfoBackPreRoll : UnityEvent<int> { }
public class DiceZoneChecker : MonoBehaviour
{
    Vector3 Velocity;
    public InfoBack infoBack;
    public InfoBackPreRoll infoBackPre;
    public UnityEvent OnChangeMovable;
    bool Once = false;

    int Rolled = 0;
    int FeedbackType = 0;

    void Update()
    {
        Velocity = DiceRoll.Velocity;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Velocity.x == 0f && Velocity.y == 0f && Velocity.z == 0f)
        {
            switch (other.gameObject.name)
            {
                case "Side1":
                    DiceRoll.TestInt = 6;
                    Rolled = 6;
                    break;

                case "Side2":
                    DiceRoll.TestInt = 5;
                    Rolled = 5;
                    break;

                case "Side3":
                    DiceRoll.TestInt = 4;
                    Rolled = 4;
                    break;

                case "Side4":
                    DiceRoll.TestInt = 3;
                    Rolled = 3;
                    break;

                case "Side5":
                    DiceRoll.TestInt = 2;
                    Rolled = 2;
                    break;

                case "Side6":
                    DiceRoll.TestInt = 1;
                    Rolled = 1;
                    break;

            }

            if (Once == true)
            {
                Once = false;

                if (FeedbackType == 1)
                {
                    infoBackPre.Invoke(Rolled);
                    OnChangeMovable.Invoke();
                }
                else if (FeedbackType == 2)
                {
                    infoBack.Invoke(Rolled);
                }

                
            }
            
        }
    }

    public void Switch(int type)
    {
        Once = true;
        FeedbackType = type;
    }
}
