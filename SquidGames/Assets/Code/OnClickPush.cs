using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;

public class OnClickPush : MonoBehaviour, IPointerDownHandler
{
    private const int movesNumber = 1;

    public delegate void Action(int numberMoves, string colorButtong, GameObject buttonObject, GameObject[] array, Button[] moveButtons, Button[] skillsButtons);
    public static event Action OnClicked;

    private GameObject[] players;
    private string buttonName;
    [SerializeField] private Button[] moveButtons;
    [SerializeField] private Button[] skillsButtons;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonName = this.gameObject.name;
        //bool anyCloseEnemies = players.ToList().ForEach(p => p.GetComponent<MovePlayer>().currentIndex)
        if (buttonName.StartsWith("R"))
        {
            OnClicked(movesNumber, "R", this.gameObject, players, moveButtons, skillsButtons);         
        }
        else if(buttonName.StartsWith("B"))
        {
            OnClicked(movesNumber, "B", this.gameObject, players, moveButtons, skillsButtons);         
        }
        else if (buttonName.StartsWith("G"))
        {
            OnClicked(movesNumber, "G", this.gameObject, players, moveButtons, skillsButtons);
        }
        else
        { 
            OnClicked(movesNumber, "W", this.gameObject, players, moveButtons, skillsButtons);
        }
    }
}
