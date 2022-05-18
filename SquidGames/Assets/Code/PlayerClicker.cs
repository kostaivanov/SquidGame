using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerClicker : MonoBehaviour
{
    private bool playerWasChosen;
    private MovePlayer movePlayer;
    internal LayerMask playerLayer;
    private SpriteRenderer spriteRenderer;
    private Color initialColor;
    private Color currentColor;

    internal bool toPushEnemy;
    //internal bool toSwitchEnemy;
    internal bool toBombEnemy;
    // Start is called before the first frame update
    void Start()
    {
        playerWasChosen = false;

        toPushEnemy = false;
        //toSwitchEnemy = false;
        toBombEnemy = false;

        movePlayer = GetComponent<MovePlayer>();
        playerLayer = LayerMask.GetMask("GroundLayer");
        spriteRenderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
        initialColor = spriteRenderer.color;
        currentColor = initialColor;
    }

    //private void Update()
    //{
    //    Debug.Log(this.gameObject.name + " = to push enemy = " + toPushEnemy);
    //}

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
    //        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        //RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

    //        if (hit.collider != null)
    //        {
    //            if (this.gameObject.name == hit.collider.gameObject.name)
    //            {
    //                playerWasChosen = true;
    //                spriteRenderer.color = new Color(1, 0, 0, 1);
    //                currentColor = spriteRenderer.color;
    //            }
    //            Debug.Log("Target Position: " + hit.collider.gameObject.name);
    //            //SpriteRenderer sprite = this.gameObject.GetComponentInChildren<SpriteRenderer>();

    //        }
    //    }
    //}

    //private void OnEnable()
    //{
    //   // OnClickSwitch.OnClicked += SwapPlayer;
    //    //OnClickBomb.OnClicked += BombPlayer;
    //    OnClickPush.OnClicked += MovePlayerForward;

    //}

    //private void OnDisable()
    //{
    //    //OnClickSwitch.OnClicked -= SwapPlayer;
    //    //OnClickBomb.OnClicked -= BombPlayer;
    //    OnClickPush.OnClicked -= MovePlayerForward;
    //}

    //private void SwapPlayer(string buttonName, GameObject[] players, GameObject buttonObject)
    //{
    //    if (this.gameObject.name.Substring(0, 1) == buttonName)
    //    {
    //        toSwitchEnemy = true;           
    //    }   
    //}
    //private void MovePlayerForward(int moveNumber, string colorButtong, GameObject buttonObject, Button[] moveButtons, Button[] skillsButtons, MoveButtonsStateController moveButtonsStateController)
    //{
    //    if (this.gameObject.name.Substring(0, 1) == colorButtong)
    //    {
    //        toPushEnemy = true;

    //        //MovePlayer movePlayer = GetComponent<MovePlayer>();
            
    //        //movePlayer.MovePlayerForward(moveNumber, this.gameObject.name.Substring(0, 1), buttonObject, moveButtons, skillsButtons, moveButtonsStateController);
            
            
    //        //PlayerClicker chosenClickedPlayer = GetMovePlayerVariable(players);
    //        //if (chosenClickedPlayer != null)
    //        //{
    //        //    chosenClickedPlayer.GetComponentInChildren<MovePlayer>().MovePlayerForward(moveNumber, chosenClickedPlayer.gameObject.name.Substring(0, 1), buttonObject, moveButtons, skillsButtons, moveButtonsStateController);
    //        //    chosenClickedPlayer.playerWasChosen = false;
    //        //    //Button button = buttonObject.GetComponent<Button>();
    //        //    // button.interactable = false;
    //        //    chosenClickedPlayer.gameObject.GetComponentInChildren<SpriteRenderer>().color = initialColor;
    //        //    spriteRenderer.color = initialColor;
    //        //}
    //    }
    //}

    //private void BombPlayer(string buttonName, GameObject[] players, LivesManager livesManager, GameObject buttonObject)
    //{
    //    if (this.gameObject.name.Substring(0, 1) == buttonName)
    //    {
    //        PlayerClicker chosenClickedPlayer = GetMovePlayerVariable(players);
    //        if (chosenClickedPlayer != null)
    //        {
    //            Debug.Log(chosenClickedPlayer.gameObject.name);
    //            livesManager.Restart(null, chosenClickedPlayer.gameObject, chosenClickedPlayer.gameObject.GetComponent<MovePlayer>().startPosition);
    //            chosenClickedPlayer.playerWasChosen = false;
    //            //Button button = buttonObject.GetComponent<Button>();
    //            // button.interactable = false;
    //            chosenClickedPlayer.gameObject.GetComponentInChildren<SpriteRenderer>().color = initialColor;
    //            spriteRenderer.color = initialColor;
    //        }
    //    }
    //}

    //private PlayerClicker GetMovePlayerVariable(GameObject[] players)
    //{        
    //    foreach (GameObject player in players)
    //    {
    //        PlayerClicker clickedPlayer = player.GetComponent<PlayerClicker>();
    //        if (clickedPlayer.playerWasChosen == true)
    //        {
    //            return clickedPlayer;
    //        }

    //    }
    //    return null;
    //}

  
}
