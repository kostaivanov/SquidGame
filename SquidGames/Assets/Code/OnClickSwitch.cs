using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;

public class OnClickSwitch : MonoBehaviour, IPointerDownHandler
{
    public delegate void Action(string buttonName, GameObject[] array, GameObject buttonObject);
    public static event Action OnClicked;
    private string buttonName;
    private GameObject[] players;
    private Button thisButton;
    internal bool activated;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        thisButton = GetComponent<Button>();
        activated = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonName = this.gameObject.name;
        if (buttonName.StartsWith("R"))
        {
            OnClicked("R", players, this.gameObject);
        }
        else if (buttonName.StartsWith("B"))
        {
            activated = true;
            OnClicked("B", players, this.gameObject);
        }
        else if (buttonName.StartsWith("G"))
        {
            OnClicked("G", players, this.gameObject);
        }
        else
        {
            OnClicked("W", players, this.gameObject);
        }
    }
}
