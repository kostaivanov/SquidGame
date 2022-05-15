using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;

public class OnClickBomb : MonoBehaviour, IPointerDownHandler
{
    public delegate void Action(string buttonName, GameObject[] array, LivesManager livesManager, GameObject button);
    public static event Action OnClicked;
    private string buttonName;
    private GameObject[] players;
    [SerializeField] private LivesManager livesManager;
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
            OnClicked("R", players, livesManager, this.gameObject);

        }
        else if(buttonName.StartsWith("B"))
        {
            //ColorBlock cb = thisButton.colors;
            //cb.selectedColor = thisButton.colors.pressedColor;
            //thisButton.colors = cb;
            activated = true;
            OnClicked("B", players, livesManager, this.gameObject);
        }
        else if (buttonName.StartsWith("G"))
        {
            OnClicked("G", players, livesManager, this.gameObject);
            Debug.Log("Green + coun - " + players.Length);
        }
        else
        {
            OnClicked("W", players, livesManager, this.gameObject);
        }
    }
}
