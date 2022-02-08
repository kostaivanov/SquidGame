using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickPush : MonoBehaviour, IPointerDownHandler
{
    private GameObject[] players;
    private MovePlayer movePlayer1, movePlayer2;


    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        if (this.gameObject.name.StartsWith("R"))
        {
            movePlayer1 = players[0].GetComponent<MovePlayer>();
        }
        else
        {
            movePlayer2 = players[1].GetComponent<MovePlayer>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (movePlayer1 != null && movePlayer2 != null)
        {
            if (true)
            {

            }
            movePlayer1.currentIndex++;
        }
    }
}
