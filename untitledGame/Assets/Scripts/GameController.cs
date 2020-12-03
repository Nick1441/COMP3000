using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Button RollButton;
    public Text TextDie;
    public GameObject Player1;

    public int Die = 0;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = RollButton.GetComponent<Button>();
        Text txt = TextDie.GetComponent<Text>();
        btn.onClick.AddListener(RollDice);
    }

    public void RollDice()
    {
        Die = Random.Range(1, 7);
        TextDie.text = "Number is " + Die;
        Player1.GetComponent<PlayerMovement>().Moves = Die;
        Player1.GetComponent<PlayerMovement>().MoveAllowed = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
