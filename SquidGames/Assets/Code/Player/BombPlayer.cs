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

    private bool found;

    // Start is called before the first frame update
    void Start()
    {
        movePlayer = GetComponent<MovePlayer>();
        //playerClicker = GetComponent<PlayerClicker>();
        toBombEnemy = false;
        found = false;
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
            this.skillsButtons = skillsButtons;

            if (toBombEnemy == true)
            {
                obj.GetComponent<Button>().interactable = false;
                indexToBomb = movePlayer.currentIndex + boxIndex;

                foreach (MovePlayer p in movePlayer.playersMove)
                {
                    if (p.initialIndex == indexToBomb)
                    {
                        found = true;
                        Bomb(p);
                    }
                }
                if (found == false)
                {
                    toBombEnemy = false;
                    skillsButtons[2].GetComponent<OnClickBomb>().activated = false;
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
            buttonObject.GetComponent<Button>().interactable = false;
        }
    }

    private void Bomb(MovePlayer target)
    {
        livesManager.Restart(true, this.gameObject, target.gameObject, target.startPosition);
        if (toBombEnemy == true)
        {
            toBombEnemy = false;
            found = false;
        }
    }
}
