using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveButtonsStateController : MonoBehaviour
{
    internal List<GameObject> usedButtons;

    private void Start()
    {
        usedButtons = new List<GameObject>();
    }

    private void Update()
    {
        //Debug.Log("count = " + usedButtons.Count);
    }

    internal void CheckIfAllUsed(List<GameObject> moveButtons)
    {
        if (usedButtons.Count > 3)
        {
            //foreach (Button _button in moveButtons)
            //{
            //    _button.interactable = true;
            //}
            usedButtons.Clear();
        }
    }
}
