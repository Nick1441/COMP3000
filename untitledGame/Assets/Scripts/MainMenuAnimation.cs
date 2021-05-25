using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimation : MonoBehaviour
{
    public GameObject MainLogo;
    public GameObject Menu;

    public void Clicked()
    {
        MainLogo.GetComponent<Animator>().SetTrigger("Clicked");
        Menu.GetComponent<Animator>().SetTrigger("Clicked");
    }
}
