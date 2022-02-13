using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClicker : MonoBehaviour
{
    private bool playerWasChosen;
    private MovePlayer movePlayer;
    // Start is called before the first frame update
    void Start()
    {
        playerWasChosen = false;
        movePlayer = GetComponent<MovePlayer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Target Position: " + hit.collider.gameObject.name);
                playerWasChosen = true;
            }
        }
    }

    private void OnEnable()
    {
        OnClickSwitch.OnClicked += SwapPlayer;
    }

    private void OnDisable()
    {
        OnClickSwitch.OnClicked -= SwapPlayer;
    }

    private void SwapPlayer(string buttonName, GameObject[] players)
    {
        if (this.gameObject.name.Substring(0, 1) == buttonName)
        {
            PlayerClicker chosenClickedPlayer = GetMovePlayerVariable(players);

            Vector3 lastPosition = this.movePlayer.gameObject.transform.position;
            Vector3 lastEulerAngle = this.movePlayer.gameObject.transform.eulerAngles;
            this.gameObject.transform.position = chosenClickedPlayer.gameObject.transform.position;
            this.gameObject.transform.eulerAngles = chosenClickedPlayer.gameObject.transform.eulerAngles;
            chosenClickedPlayer.gameObject.transform.position = lastPosition;
            chosenClickedPlayer.gameObject.transform.eulerAngles = lastEulerAngle;
            chosenClickedPlayer.playerWasChosen = false;

            SwapMovementtValues(this.movePlayer, chosenClickedPlayer.gameObject.GetComponent<MovePlayer>());
        }   
    }

    private PlayerClicker GetMovePlayerVariable(GameObject[] players)
    {        
        foreach (GameObject player in players)
        {
            PlayerClicker clickedPlayer = player.GetComponent<PlayerClicker>();
            if (clickedPlayer.playerWasChosen == true)
            {
                return clickedPlayer;
            }

        }
        return null;
    }

    private void SwapMovementtValues(MovePlayer rootPlayer, MovePlayer chosenPlayer)
    {
        int lastInitialIndex = rootPlayer.initialIndex;
        int lastCurrentIndex = rootPlayer.currentIndex;
        rootPlayer.initialIndex = chosenPlayer.initialIndex;
        rootPlayer.currentIndex = chosenPlayer.currentIndex;
        chosenPlayer.initialIndex = lastInitialIndex;
        chosenPlayer.currentIndex = lastCurrentIndex;
    }
}
