using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BombPlayer : MonoBehaviour
{
    //private List<GameObject> players;
    //private PlayerClicker playerClicker;
    //private List<MovePlayer> playersMove;
    //private Button[] moveButtons;
    private Button[] skillsButtons;
    //private MoveButtonsStateController moveButtonsStateController;
    private MovePlayer movePlayer;
    private int indexToBomb;
    internal bool toBombEnemy;
    private LivesManager livesManager;

    // Start is called before the first frame update
    void Start()
    {
        movePlayer = GetComponent<MovePlayer>();
        //playerClicker = GetComponent<PlayerClicker>();
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

    internal void BombPlayerFunc(int boxIndex, string buttonColor, GameObject obj, Button[] moveButtons, Button[] skillsButtons, MoveButtonsStateController moveButtonsStateController)
    {
        if (this.gameObject.name.Substring(0, 1) == buttonColor)
        {
            //this.moveButtons = moveButtons;
            this.skillsButtons = skillsButtons;
            //this.moveButtonsStateController = moveButtonsStateController;

            if (toBombEnemy == true)
            {
                indexToBomb = movePlayer.currentIndex + boxIndex;
                foreach (MovePlayer p in movePlayer.playersMove)
                {
                    if (p.initialIndex == indexToBomb)
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
            this.livesManager = livesManager;

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
            buttonObject.GetComponent<Button>().interactable = false;
        }
    }

    private void Bomb(MovePlayer target)
    {
        livesManager.Restart(true, skillsButtons[2].gameObject, target.gameObject, target.startPosition);
        if (toBombEnemy == true)
        {
            toBombEnemy = false;
        }
    }
}
