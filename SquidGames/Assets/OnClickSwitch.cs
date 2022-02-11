using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class OnClickSwitch : MonoBehaviour, IPointerDownHandler
{
    //public delegate void Action(int nambuttonNamee, string colorButtong, GameObject obj);
    //public static event Action OnClicked;

    private GameObject[] players;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 lastPosition = players[0].transform.position;
        Vector3 lastEulerAngle = players[0].transform.eulerAngles;
        players[0].transform.position = players[1].transform.position;
        players[0].transform.eulerAngles = players[1].transform.eulerAngles;
        players[1].transform.position = lastPosition;
        players[1].transform.eulerAngles = lastEulerAngle;
    }
}
