using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class OnClickBomb : MonoBehaviour, IPointerDownHandler
{
    public delegate void Action(string buttonName, GameObject[] array, LivesManager livesManager, GameObject button);
    public static event Action OnClicked;
    private string buttonName;
    private GameObject[] players;
    [SerializeField] private LivesManager livesManager;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonName = this.gameObject.name;
        if (buttonName.StartsWith("R"))
        {
            OnClicked("B", players, livesManager, this.gameObject);

        }
        else
        {
            OnClicked("R", players, livesManager, this.gameObject);
        }
    }
}
