using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PushPlayer : MonoBehaviour
{
    //private List<GameObject> players;
    //private List<MovePlayer> playersMove;
    private Button[] moveButtons;
    private Button[] skillsButtons;
    //private MoveButtonsStateController moveButtonsStateController;
    //private MovePlayer movePlayer;
    internal bool toSwitchEnemy;
    internal bool toPushEnemy;

    // Start is called before the first frame update
    void Start()
    {
        //movePlayer = GetComponent<MovePlayer>();
        toPushEnemy = false;
    }

    private void OnEnable()
    {
        OnClickPush.OnClicked += MovePlayerForward;
    }

    private void OnDisable()
    {
        OnClickPush.OnClicked -= MovePlayerForward;
    }

    private void MovePlayerForward(int moveNumber, string colorButtong, GameObject buttonObject, Button[] moveButtons, Button[] skillsButtons, MoveButtonsStateController moveButtonsStateController)
    {
        if (this.gameObject.name.Substring(0, 1) == colorButtong)
        {
            toPushEnemy = true;
            buttonObject.GetComponent<Button>().interactable = false;
        }
    }
}
