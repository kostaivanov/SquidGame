using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class OnClickSwitch : MonoBehaviour, IPointerDownHandler
{
    public delegate void Action(string buttonName, GameObject[] array);
    public static event Action OnClicked;
    private string buttonName;
    private GameObject[] players;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonName = this.gameObject.name;
        if (buttonName.StartsWith("R"))
        {
            OnClicked("R", players);

        }
        else
        {
            OnClicked("B", players);
        }
    }

    //private void SwtichPositionAndRotation()
    //{
    //    Vector3 lastPosition = players[0].transform.position;
    //    Vector3 lastEulerAngle = players[0].transform.eulerAngles;
    //    players[0].transform.position = players[1].transform.position;
    //    players[0].transform.eulerAngles = players[1].transform.eulerAngles;
    //    players[1].transform.position = lastPosition;
    //    players[1].transform.eulerAngles = lastEulerAngle;
    //}
}
