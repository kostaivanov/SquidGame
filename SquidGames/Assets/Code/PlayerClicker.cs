using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClicker : MonoBehaviour
{
    private bool playerWasChosen;
    private MovePlayer movePlayer;
    internal LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        playerWasChosen = false;
        movePlayer = GetComponent<MovePlayer>();
        playerLayer = LayerMask.GetMask("GroundLayer");
    }

    private void Update()
    {
        Debug.Log(this.gameObject.name + " = " + playerWasChosen);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                if (this.gameObject.name == hit.collider.gameObject.name)
                {
                    playerWasChosen = true;
                }
                Debug.Log("Target Position: " + hit.collider.gameObject.name);
            }
        }
    }

    private void OnEnable()
    {
        OnClickSwitch.OnClicked += SwapPlayer;
        OnClickBomb.OnClicked += BombPlayer;
    }

    private void OnDisable()
    {
        OnClickSwitch.OnClicked -= SwapPlayer;
        OnClickBomb.OnClicked -= BombPlayer;
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

    private void BombPlayer(string buttonName, GameObject[] players, LivesManager livesmMnager)
    {
        if (this.gameObject.name.Substring(0, 1) == buttonName)
        {
            PlayerClicker chosenClickedPlayer = GetMovePlayerVariable(players);
            if (chosenClickedPlayer != null)
            {
                Debug.Log(chosenClickedPlayer.gameObject.name);
                livesmMnager.Restart(null, chosenClickedPlayer.gameObject, chosenClickedPlayer.gameObject.GetComponent<MovePlayer>().startPosition);
                chosenClickedPlayer.playerWasChosen = false;
            }
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
