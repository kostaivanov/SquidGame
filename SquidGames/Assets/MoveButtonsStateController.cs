using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class MoveButtonsStateController : MonoBehaviour
{
    internal List<GameObject> usedButtons;

    private void Start()
    {
        usedButtons = new List<GameObject>();
    }

    internal void CheckIfAllUsed(Button[] moveButtons)
    {
        if (usedButtons.Count > 3)
        {

            foreach (Button _button in moveButtons)
            {
                _button.interactable = true;
            }
            usedButtons.Clear();
        }
    }
}
