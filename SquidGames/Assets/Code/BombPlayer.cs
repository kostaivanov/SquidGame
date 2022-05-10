using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BombPlayer : MonoBehaviour
{
    private List<GameObject> players;
    private PlayerClicker playerClicker;
    //private List<MovePlayer> playersMove;
    private Button[] moveButtons;
    private Button[] skillsButtons;
    //private MoveButtonsStateController moveButtonsStateController;
    private MovePlayer movePlayer;
    private int indexToSwitchWith;
    internal bool toBombEnemy;


    // Start is called before the first frame update
    void Start()
    {
        movePlayer = GetComponent<MovePlayer>();
        playerClicker = GetComponent<PlayerClicker>();
        toBombEnemy = false;
    }

    private void OnEnable()
    {
        OnClickBomb.OnClicked += ActivateBomb;
        OnClickMove.OnClicked += BombPlayerFunc;
    }

    private void OnDisable()
    {
        OnClickBomb.OnClicked -= ActivateBomb;
        OnClickMove.OnClicked -= BombPlayerFunc;
    }

    // Update is called once per frame
    void Update()
    {

    }
    internal void BombPlayerFunc(int boxIndex, string buttonColor, GameObject obj, Button[] moveButtons, Button[] skillsButtons, MoveButtonsStateController moveButtonsStateController)
    {
        if (this.gameObject.name.Substring(0, 1) == buttonColor)
        {
            this.moveButtons = moveButtons;
            this.skillsButtons = skillsButtons;
            //this.moveButtonsStateController = moveButtonsStateController;
            if (toBombEnemy == true)
            {
                Debug.Log("swap?");

                indexToSwitchWith = movePlayer.currentIndex + boxIndex;
                foreach (MovePlayer p in movePlayer.playersMove)
                {
                    if (p.currentIndex == indexToSwitchWith)
                    {
                        Bomb(p);
                    }
                }
            }
        }
    }
    private void ActivateBomb(string buttonName, GameObject[] players, LivesManager livesManager, GameObject buttonObject)
    {
        if (this.gameObject.name.Substring(0, 1) == buttonName)
        {
            toBombEnemy = true;
            //PlayerClicker chosenClickedPlayer = GetMovePlayerVariable(players);
            //if (chosenClickedPlayer != null)
            //{
            //Log(chosenClickedPlayer.gameObject.name);
            //livesManager.Restart(null, chosenClickedPlayer.gameObject, chosenClickedPlayer.gameObject.GetComponent<MovePlayer>().startPosition);
            //chosenClickedPlayer.playerWasChosen = false;
            //Button button = buttonObject.GetComponent<Button>();
            // button.interactable = false;
            //chosenClickedPlayer.gameObject.GetComponentInChildren<SpriteRenderer>().color = initialColor;
            //spriteRenderer.color = initialColor;
            //}
        }
    }

    private void Bomb(MovePlayer target)
    {
        //livesManager.Restart(null, chosenClickedPlayer.gameObject, chosenClickedPlayer.gameObject.GetComponent<MovePlayer>().startPosition);
    }
}