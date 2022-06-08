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
            activated = true;
            OnClicked("R", players, livesManager, this.gameObject);
        }
        else if(buttonName.StartsWith("B"))
        {
            activated = true;
            OnClicked("B", players, livesManager, this.gameObject);
        }
        else if (buttonName.StartsWith("G"))
        {
            activated = true;
            OnClicked("G", players, livesManager, this.gameObject);
        }
        else
        {
            activated = true;
            OnClicked("W", players, livesManager, this.gameObject);
        }
    }
}
