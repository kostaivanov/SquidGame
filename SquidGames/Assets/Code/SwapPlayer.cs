using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SwapPlayer : MonoBehaviour
{
    private List<GameObject> players;
    private PlayerClicker playerClicker;
    //private List<MovePlayer> playersMove;
    private Button[] moveButtons;
    private Button[] skillsButtons;
    //private MoveButtonsStateController moveButtonsStateController;
    private MovePlayer movePlayer;
    private int indexToSwitchWith;
    // Start is called before the first frame update
    void Start()
    {
        movePlayer = GetComponent<MovePlayer>();
        playerClicker = GetComponent<PlayerClicker>();

    }
    private void OnEnable()
    {
        OnClickMove.OnClicked += SwapPlayerFunc;
    }

    private void OnDisable()
    {
        OnClickMove.OnClicked -= SwapPlayerFunc;
    }

    internal void SwapPlayerFunc(int boxIndex, string buttonColor, GameObject obj, Button[] moveButtons, Button[] skillsButtons, MoveButtonsStateController moveButtonsStateController)
    {
        if (this.gameObject.name.Substring(0, 1) == buttonColor)
        {
            this.moveButtons = moveButtons;
            this.skillsButtons = skillsButtons;
            //this.moveButtonsStateController = moveButtonsStateController;
            if (playerClicker.toSwitchEnemy == true)
            {
                indexToSwitchWith = movePlayer.currentIndex + boxIndex;
                foreach (MovePlayer p in movePlayer.playersMove)
                {
                    if (p.currentIndex == indexToSwitchWith)
                    {
                        Swap(p);
                    }
                }
            }
        }
    }

    private void Swap(MovePlayer target)
    {
        //Debug.Log("this player name = " + this.gameObject.name + " - chosen player = " + target.gameObject.name);
        Vector3 lastPosition = this.gameObject.transform.position;
        Vector3 lastEulerAngle = this.gameObject.transform.eulerAngles;
        this.gameObject.transform.position = target.gameObject.transform.position;
        this.gameObject.transform.eulerAngles = target.gameObject.transform.eulerAngles;
        target.gameObject.transform.position = lastPosition;
        target.gameObject.transform.eulerAngles = lastEulerAngle;
        //target.playerWasChosen = false;
        //Button button = buttonObject.GetComponent<Button>();
        //button.interactable = false;
        //target.gameObject.GetComponentInChildren<SpriteRenderer>().color = initialColor;
        //spriteRenderer.color = initialColor;

        SwapMovementtValues(this.movePlayer, target);
        
    }

    private void SwapMovementtValues(MovePlayer thisPlayer, MovePlayer targetPlayer)
    {
        int lastInitialIndex = thisPlayer.initialIndex;
        int lastCurrentIndex = thisPlayer.currentIndex;
        thisPlayer.initialIndex = targetPlayer.initialIndex;
        thisPlayer.currentIndex = targetPlayer.currentIndex;
        targetPlayer.initialIndex = lastInitialIndex;
        targetPlayer.currentIndex = lastCurrentIndex;
    }
}
