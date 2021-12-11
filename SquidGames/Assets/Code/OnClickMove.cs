using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

internal class OnClickMove : MonoBehaviour, IPointerDownHandler
{
    public delegate void Action(string nambuttonNamee, string colorButtong, GameObject obj);
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
                OnClicked(boxIndex, "R", this.gameObject);
            }
            else
            {
                OnClicked(boxIndex, "B", this.gameObject);
            }            
        }
    }
}
