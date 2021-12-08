using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

internal class OnClickMove : MonoBehaviour, IPointerDownHandler
{
    public delegate void Action(string nambuttonNamee, string colorButtong);
    public static event Action OnClicked;

    private string buttonName;

    private void Start()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonName = this.gameObject.name;

        if (OnClicked!= null)
        {
            string boxIndex = buttonName.Substring(buttonName.Length - 1);
            if (buttonName.StartsWith("R"))
            {
                OnClicked(boxIndex, "R");
            }
            else
            {
                OnClicked(boxIndex, "B");
            }            
        }
    }
}
