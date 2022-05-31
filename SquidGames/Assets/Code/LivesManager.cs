using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LivesManager : MonoBehaviour, IDestroyable
{
    //private GameObject[] players;
    //private List<PlayerClicker> playersClicker;
    //private List<MovePlayer> playersMove;
    [SerializeField] private List<Button> moveButtons, pushButtons, switchButtons, bombButtons;
    [SerializeField] private List<MoveButtonsStateController> moveButtonsStateControllerList;
    // Start is called before the first frame update
    void Start()
    {
        //players = GameObject.FindGameObjectsWithTag("Player");
        //playersClicker = new List<PlayerClicker>();
        //playersMove = new List<MovePlayer>();
        //foreach (GameObject p in players)
        //{
        //    playersClicker.Add(p.GetComponent<PlayerClicker>());
        //    playersMove.Add(p.GetComponent<MovePlayer>());
        //}
    }

    private void Update()
    {
        //if (playersClicker != null && playersClicker.Any(p => p.toPushEnemy == true))
        //{
        //    MovePlayer movePlayer = playersClicker.FirstOrDefault(p => p.toPushEnemy == true).GetComponent<MovePlayer>();
        //    PlayerClicker playerClicker = playersClicker.FirstOrDefault(p => p.toPushEnemy == true);
        //    //MovePlayer movePlayer = player.GetComponent<MovePlayer>();
        //    int index = movePlayer.currentIndex;
        //    if (playersMove.Any(p => p.currentIndex == index + 2))
        //    {
        //        MovePlayer playerToPush = playersMove.FirstOrDefault(p => p.currentIndex == index + 2);
        //        if (playerToPush != null)
        //        {
        //            MoveButtonsStateController moveButtonsStateController = moveButtonsStateControllerList.FirstOrDefault(p => p.gameObject.name.StartsWith(movePlayer.gameObject.name.Substring(0)));
        //            playerToPush.MovePlayerForward(2, playerToPush.gameObject.name.Substring(0), playerToPush.gameObject, moveButtons.ToArray(), pushButtons.ToArray(), moveButtonsStateController);
        //        }
        //    }
        //    playerClicker.toPushEnemy = false;
        //}
    }

    private void OnEnable()
    {
        BombController.OnBombExplodeHandler += Deactivate;
        BombController.OnBombExplodeHandler += Restart;
    }

    private void OnDisable()
    {
        BombController.OnBombExplodeHandler -= Deactivate;
        BombController.OnBombExplodeHandler -= Restart;
 
    }

    public void Deactivate(bool killedByTrap, GameObject bombObject, GameObject playerObject, Vector3 playerStartPosition)
    {
        bombObject.GetComponent<SpriteRenderer>().enabled = false;
        foreach (Transform bodyPart in playerObject.transform)
        {
            bodyPart.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void Restart(bool killedByTrap, GameObject bombObject, GameObject playerObject, Vector3 playerStartPosition)
    {
        playerObject.transform.position = playerStartPosition;
        PlayerHealth playerHealth = playerObject.GetComponent<PlayerHealth>();
        MovePlayer movePlayer = playerObject.GetComponent<MovePlayer>();
        movePlayer.trap = false;

        if (movePlayer.coroutine != null)
        {
            StopCoroutine(movePlayer.coroutine);
        }

        if (moveButtonsStateControllerList != null)
        {
            foreach (MoveButtonsStateController button in moveButtonsStateControllerList)
            {
                //Debug.Log(button.usedButtons.Count);
                button.usedButtons.Clear();
            }
        }

        foreach (Transform bodyPart in playerObject.transform)
        {
            bodyPart.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        foreach (Button button in moveButtons)
        {
            if (playerObject.name.StartsWith("B") && button.gameObject.name.StartsWith("B"))
            {
                button.interactable = true;
            }
            else if(playerObject.name.StartsWith("R") && button.gameObject.name.StartsWith("R"))
            {
                button.interactable = true;
            }

        }
        //foreach (Button button in pushButtons)
        //{
        //    if (playerObject.name.StartsWith("B") && button.gameObject.name.StartsWith("R"))
        //    {
        //        button.interactable = true;
        //    }
        //    else if (playerObject.name.StartsWith("R") && button.gameObject.name.StartsWith("B"))
        //    {
        //        button.interactable = true;
        //    }
        //}
        if (movePlayer != null)
        {
            movePlayer.currentIndex = 0;
            movePlayer.initialIndex = movePlayer.currentIndex;
        }
        
        if (playerHealth != null)
        {
            playerHealth.dead = false;
            playerHealth.numbersChanged = false;
        }

        playerObject.transform.localScale = new Vector2(-0.5f, 0.5f);
        movePlayer.rotationChanged = false;
        playerHealth.dead = true;


        for (int i = 0; i < switchButtons.Count; i++)
        {
            if (playerObject.name.Substring(0, 1) == switchButtons[i].gameObject.name.Substring(0, 1))
            {
                switchButtons[i].interactable = true;
                switchButtons[i].GetComponent<OnClickSwitch>().activated = false;
                bombButtons[i].interactable = true;
                bombButtons[i].GetComponent<OnClickBomb>().activated = false;
                pushButtons[i].interactable = true;
            }
        }

        //if (!bombOrSkillObject.name.Any(char.IsDigit))
        //{
        //    bombOrSkillObject.GetComponent<OnClickBomb>().activated = false;
        //}

        //if (killedByTrap == false)
        //{
        //    if (bombOrSkillObject.name.EndsWith("C"))
        //    {
        //        Button button = bombOrSkillObject.GetComponent<Button>();
        //        button.interactable = true;
        //    }
        //}      
    }
}
