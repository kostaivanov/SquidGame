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
            //string _moveNumber = buttonName.Substring(buttonName.Length - 1);
            int _moveNumber = int.Parse(moveNumberImage.sprite.name);
            if (buttonName.StartsWith("R"))
            {
                OnClicked(_moveNumber, "R", this.gameObject, moveButtons, skillsButtons, moveButtonsStateController);
                //moveButtonsStateController.usedButtons.Add(this.gameObject);
                //moveButtonsStateController.CheckIfAllUsed(moveButtons);

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
                OnClicked(_moveNumber, "B", this.gameObject, moveButtons, skillsButtons, moveButtonsStateController);
                //moveButtonsStateController.usedButtons.Add(this.gameObject);
                //moveButtonsStateController.CheckIfAllUsed(moveButtons);

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
                OnClicked(_moveNumber, "G", this.gameObject, moveButtons, skillsButtons, moveButtonsStateController);
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
                OnClicked(_moveNumber, "W", this.gameObject, moveButtons, skillsButtons, moveButtonsStateController);
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

    //private bool CheckIfAnyButtonIsDisabled(Button[] buttons)
    //{
    //    foreach (Button b in buttons)
    //    {
    //        if (b.interactable == false)
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}

    //private bool TurnButtonsInteractable()
    //{
    //    bool allInactive = true;
    //    foreach (Button _button in moveButtons)
    //    {
    //        if (_button.interactable == true)
    //        {
    //            allInactive = false;
    //        }
    //    }
    //    return allInactive;
    //}

    //private IEnumerator ActivateButtons(List<GameObject> usedButtons, Button[] moveButtons, Button[] skillsButtons)
    //{
    //    //TurnOnOffButtons(false, usedButton, moveButtons, skillsButtons);
    //    yield return new WaitForSecondsRealtime(5f);
    //    //if (TurnButtonsInteractable(moveButtons) == true)
    //    //{
    //    foreach (Button _button in moveButtons)
    //    {
    //        if (usedButtons.Any(x => x.name == _button.gameObject.name))
    //        {
    //            continue;
    //        }

    //        _button.interactable = true;

    //    }
    //    // }
    //}
}
