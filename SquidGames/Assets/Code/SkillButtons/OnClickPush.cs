using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;

public class OnClickPush : MonoBehaviour, IPointerDownHandler
{
    private const int movesNumber = 1;

    public delegate void Action(int numberMoves, string colorButtong, GameObject buttonObject, Button[] moveButtons, Button[] skillsButtons, MoveButtonsStateController moveButtonsStateController);
    public static event Action OnClicked;

    //private GameObject[] players;
    private string buttonName;
    //private Button thisButton;
    //private Color newColor;
    private Button[] moveButtons;
    private Button[] skillsButtons;
    [SerializeField] private GameObject usedButtonsObject;
    private MoveButtonsStateController moveButtonsStateController;

    // Start is called before the first frame update
    void Start()
    {
        //thisButton = GetComponent<Button>();
        //players = GameObject.FindGameObjectsWithTag("Player");
        moveButtonsStateController = usedButtonsObject.GetComponent<MoveButtonsStateController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonName = this.gameObject.name;
        //bool anyCloseEnemies = players.ToList().ForEach(p => p.GetComponent<MovePlayer>().currentIndex)
        if (buttonName.StartsWith("R"))
        {
            OnClicked(movesNumber, "R", this.gameObject, moveButtons, skillsButtons, moveButtonsStateController);         
        }
        else if(buttonName.StartsWith("B"))
        {
            OnClicked(movesNumber, "B", this.gameObject, moveButtons, skillsButtons, moveButtonsStateController);         
        }
        else if (buttonName.StartsWith("G"))
        {
            OnClicked(movesNumber, "G", this.gameObject, moveButtons, skillsButtons, moveButtonsStateController);
        }
        else
        {
            OnClicked(movesNumber, "W", this.gameObject, moveButtons, skillsButtons, moveButtonsStateController);
        }
    }
}
