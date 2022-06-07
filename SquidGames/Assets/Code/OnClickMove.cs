using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

internal class OnClickMove : MonoBehaviour, IPointerDownHandler
{
    //public delegate void Action(int nambuttonNamee, string colorButtong, GameObject obj, Button[] moveButtons, Button[] skillsButtons, List<GameObject> usedButtons);
    public delegate void Action(int nambuttonNamee, string colorButtong, GameObject obj, Button[] moveButtons, Button[] skillsButtons, MoveButtonsStateController moveButtonsStateController);
    public static event Action OnClicked;

    [SerializeField] private Button[] moveButtons;
    [SerializeField] private Button[] skillsButtons;
    private MoveButtonsStateController moveButtonsStateController;
    private ButtonsController buttonsController;
    private GameObject[] players;

    //private Button button;
    private string buttonName;
    //internal Text moveNumber;
    internal Image moveNumberImage;

    private void Start()
    {
        //moveNumber = GetComponentInChildren<Text>();
        moveNumberImage = GetComponent<Image>();
        buttonsController = this.gameObject.transform.root.gameObject.GetComponent<ButtonsController>();
        moveNumberImage.sprite = buttonsController.numbersImages[UnityEngine.Random.Range(0, 4)];
        //moveNumber.text = UnityEngine.Random.Range(1, 5).ToString();
        moveButtonsStateController = GetComponentInParent<MoveButtonsStateController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonName = this.gameObject.name;

        if (OnClicked!= null)
        {
            int _moveNumber = int.Parse(moveNumberImage.sprite.name);
            if (buttonName.StartsWith("R"))
            {
                OnClicked(_moveNumber, "R", this.gameObject, moveButtons, skillsButtons, moveButtonsStateController);
            }
            else if(buttonName.StartsWith("B"))
            {
                OnClicked(_moveNumber, "B", this.gameObject, moveButtons, skillsButtons, moveButtonsStateController);
            }
            else if (buttonName.StartsWith("G"))
            {
                OnClicked(_moveNumber, "G", this.gameObject, moveButtons, skillsButtons, moveButtonsStateController);

            }
            else if (buttonName.StartsWith("W"))
            {
                OnClicked(_moveNumber, "W", this.gameObject, moveButtons, skillsButtons, moveButtonsStateController);
            }
        }
    }
}
