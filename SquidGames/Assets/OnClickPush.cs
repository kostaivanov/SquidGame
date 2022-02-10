using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
public class OnClickPush : MonoBehaviour, IPointerDownHandler
{
    private const int movesNumber = 1;

    public delegate void Action(int nambuttonNamee, string colorButtong, GameObject obj);
    public static event Action OnClicked;

    private GameObject[] players;
    private string buttonName;


    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonName = this.gameObject.name;
        //bool anyCloseEnemies = players.ToList().ForEach(p => p.GetComponent<MovePlayer>().currentIndex)
        if (buttonName.StartsWith("R"))
        {
            OnClicked(movesNumber, "R", this.gameObject);
          
        }
        else
        {
            OnClicked(movesNumber, "B", this.gameObject);
          
        }
    }
}
