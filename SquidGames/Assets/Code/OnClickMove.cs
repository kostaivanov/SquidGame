using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

internal class OnClickMove : MonoBehaviour, IPointerDownHandler
{
    public delegate void Action(string nambuttonNamee, string colorButtong, GameObject obj);
    public static event Action OnClicked;
    [SerializeField] private Button[] moveButtons;

    private Button button;
    private string buttonName;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonName = this.gameObject.name;

        if (OnClicked!= null)
        {
            string boxIndex = buttonName.Substring(buttonName.Length - 1);
            if (buttonName.StartsWith("R"))
            {
                OnClicked(boxIndex, "R", this.gameObject);
                if (TurnButtonsInteractable() == true)
                {
                    foreach (Button _button in moveButtons)
                    {
                        _button.interactable = true;
                    }
                }
            }
            else
            {
                OnClicked(boxIndex, "B", this.gameObject);
                if (TurnButtonsInteractable() == true)
                {
                    foreach (Button _button in moveButtons)
                    {
                        _button.interactable = true;
                    }
                }
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
