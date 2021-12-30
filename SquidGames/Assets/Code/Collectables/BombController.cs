using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class BombController : MonoBehaviour, ICollectable, IDestroyable
{
    private MovePlayer movePlayer;
    private PlayerHealth playerHealth;

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            movePlayer = otherObject.GetComponent<MovePlayer>();
            playerHealth = otherObject.GetComponent<PlayerHealth>();
        }
    }

    private void OnTriggerStay2D(Collider2D otherObject)
    {
        if (movePlayer != null && movePlayer.collectableFound == true)
        {
            if (movePlayer.gameObject.name.StartsWith("B") )
            {
                movePlayer.collectableFound = false;
                Activate();
                StartCoroutine(Explode(movePlayer.gameObject, movePlayer.startPosition));
            }
            if (movePlayer.gameObject.name.StartsWith("R") )
            {
                movePlayer.collectableFound = false;
                Activate();
                StartCoroutine(Explode(movePlayer.gameObject, movePlayer.startPosition));

            }
        }
    }

    private IEnumerator Explode(GameObject obj, Vector3 position)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Deactivate(obj);
        Restart(obj, position);
    }

    public void Activate()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Deactivate(GameObject obj)
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        foreach (Transform bodyPart in obj.transform)
        {
            bodyPart.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void Restart(GameObject obj, Vector3 position)
    {
        obj.transform.position = position;

        //GameObject[] _boxes = GameObject.FindGameObjectsWithTag("Platform");
        //GameObject[] _bombs = GameObject.FindGameObjectsWithTag("Bomb");
       
        //InstantiateItems.Shuffle(_boxes, _bombs);
        GameObject[] _moveButtons = FindButtonMembers(obj);

        foreach (Transform bodyPart in obj.transform)
        {
            bodyPart.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        foreach (GameObject button in _moveButtons)
        {
            button.GetComponent<Button>().interactable = true;
        }

        if (obj.name.StartsWith("B"))
        {
            MovePlayer _movePlayer = obj.GetComponent<MovePlayer>();
            _movePlayer.currentIndexBlue = -1;
            _movePlayer.initialBlueIndex = _movePlayer.currentIndexBlue;
        }
        else if (obj.name.StartsWith("R"))
        {
            MovePlayer _movePlayer = obj.GetComponent<MovePlayer>();
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
        else if(obj.name.StartsWith("B"))
        {
            _moveButtons = GameObject.FindGameObjectsWithTag("BlueMoveButton");
        }
        return _moveButtons;
    }
}
