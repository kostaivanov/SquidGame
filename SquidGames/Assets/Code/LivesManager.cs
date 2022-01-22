using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour, IDestroyable
{
    private GameObject[] players;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        //BombController bomb = new BombController();

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
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Deactivate(GameObject bomb, GameObject player, Vector3 position)
    {
        bomb.GetComponent<SpriteRenderer>().enabled = false;
        foreach (Transform bodyPart in player.transform)
        {
            bodyPart.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void Restart(GameObject bomb, GameObject player, Vector3 position)
    {
        player.transform.position = position;
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        //GameObject[] _boxes = GameObject.FindGameObjectsWithTag("Platform");
        //GameObject[] _bombs = GameObject.FindGameObjectsWithTag("Bomb");
       
        //InstantiateItems.Shuffle(_boxes, _bombs);
        GameObject[] _moveButtons = FindButtonMembers(player);

        foreach (Transform bodyPart in player.transform)
        {
            bodyPart.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            Debug.Log(bodyPart.gameObject.name);
        }

        foreach (GameObject button in _moveButtons)
        {
            button.GetComponent<Button>().interactable = true;
        }

        if (player.name.StartsWith("B"))
        {
            MovePlayer _movePlayer = player.GetComponent<MovePlayer>();
            _movePlayer.currentIndexBlue = -1;
            _movePlayer.initialBlueIndex = _movePlayer.currentIndexBlue;
        }
        else if (player.name.StartsWith("R"))
        {
            MovePlayer _movePlayer = player.GetComponent<MovePlayer>();
            _movePlayer.currentIndexRed = -1;
            _movePlayer.initialRedIndex = _movePlayer.currentIndexRed;
        }
        if (playerHealth != null)
        {
            playerHealth.dead = false;
            playerHealth.numbersChanged = false;
            //Debug.Log("restart");
        }
        playerHealth.dead = true;

    }

    private GameObject[] FindButtonMembers(GameObject obj)
    {
        GameObject[] _moveButtons = new GameObject[3];
        if (obj.name.StartsWith("R"))
        {
            _moveButtons = GameObject.FindGameObjectsWithTag("RedMoveButton");
        }
        else if (obj.name.StartsWith("B"))
        {
            _moveButtons = GameObject.FindGameObjectsWithTag("BlueMoveButton");
        }
        return _moveButtons;
    }
}
