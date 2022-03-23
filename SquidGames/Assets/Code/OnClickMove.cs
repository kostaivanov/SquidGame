using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

internal class OnClickMove : MonoBehaviour, IPointerDownHandler
{
    public delegate void Action(int nambuttonNamee, string colorButtong, GameObject obj, Button[] moveButtons, Button[] skillsButtons);
    public static event Action OnClicked;
    [SerializeField] private Button[] moveButtons;
    [SerializeField] private Button[] skillsButtons;
    private MoveButtonsStateController moveButtonsStateController;

    //private Button button;
    private string buttonName;
    internal Text moveNumber;


    private void Start()
    {

        //button = GetComponent<Button>();
        moveNumber = GetComponentInChildren<Text>();
        moveNumber.text = UnityEngine.Random.Range(1, 5).ToString();
        moveButtonsStateController = GetComponentInParent<MoveButtonsStateController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonName = this.gameObject.name;

        if (OnClicked!= null)
        {
            //string _moveNumber = buttonName.Substring(buttonName.Length - 1);
            int _moveNumber = int.Parse(moveNumber.text);
            if (buttonName.StartsWith("R"))
            {
                OnClicked(_moveNumber, "R", this.gameObject, moveButtons, skillsButtons);
                moveButtonsStateController.usedButtons.Add(this.gameObject);
                moveButtonsStateController.CheckIfAllUsed(moveButtons);
                //if (TurnButtonsInteractable() == true)
                //{
                //    foreach (Button _button in moveButtons)
                //    {
                //        _button.interactable = true;
                //    }
                //}
            }
            else if(buttonName.StartsWith("B"))
            {
                OnClicked(_moveNumber, "B", this.gameObject, moveButtons, skillsButtons);
                moveButtonsStateController.usedButtons.Add(this.gameObject);
                moveButtonsStateController.CheckIfAllUsed(moveButtons);
                //if (TurnButtonsInteractable() == true)
                //{
                //    foreach (Button _button in moveButtons)
                //    {
                //        _button.interactable = true;
                //    }
                //}
            }
            else if (buttonName.StartsWith("G"))
            {
                OnClicked(_moveNumber, "G", this.gameObject, moveButtons, skillsButtons);
                //if (TurnButtonsInteractable() == true)
                //{
                //    foreach (Button _button in moveButtons)
                //    {
                //        _button.interactable = true;
                //    }
                //}
            }
            else if (buttonName.StartsWith("W"))
            {
                OnClicked(_moveNumber, "W", this.gameObject, moveButtons, skillsButtons);
                //if (TurnButtonsInteractable() == true)
                //{
                //    foreach (Button _button in moveButtons)
                //    {
                //        _button.interactable = true;
                //    }
                //}
            }

        }
    }

    private bool TurnButtonsInteractable()
    {
        bool allInactive = true;
        foreach (Button _button in moveButtons)
        {
            if (_button.interactable == true)
            {
                allInactive = false;
            }
        }
        return allInactive;
    }
}
