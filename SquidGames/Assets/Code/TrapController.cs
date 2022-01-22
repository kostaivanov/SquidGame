using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    private MovePlayer movePlayer;
    [SerializeField] private GameObject[] collectables;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            movePlayer = otherObject.GetComponent<MovePlayer>();
        }
    }

    private void OnTriggerStay2D(Collider2D otherObject)
    {
        if (movePlayer != null && movePlayer.collectableFound == true)
        {
            string _moveNumber = "2";
            movePlayer.collectableFound = false;
            if (otherObject.gameObject.name.StartsWith("R"))
            {
                if (this.gameObject.tag == "GoForward")
                {
                    if (movePlayer.moveRed == false)
                    {
                        EnableCollectable();
                    }

                    StartCoroutine(GoForward(movePlayer, _moveNumber));
                }
                else if (this.gameObject.tag == "GoBackward")
                {
                    if (movePlayer.moveRed == false)
                    {
                        EnableCollectable();
                    }

                    StartCoroutine(GoBack(movePlayer, _moveNumber));

                }
            }
            else if(otherObject.gameObject.name.StartsWith("B"))
            {
                //movePlayer.collectableFound = false;

                if (this.gameObject.tag == "GoForward")
                {
                    if (movePlayer.moveBlue == false)
                    {
                        EnableCollectable();
                    }

                    StartCoroutine(GoForward(movePlayer, _moveNumber));
                }
                else if (this.gameObject.tag == "GoBackward")
                {
                    if (movePlayer.moveBlue == false)
                    {
                        EnableCollectable();
                    }

                    StartCoroutine(GoBack(movePlayer, _moveNumber));
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (movePlayer != null)
        {
            movePlayer = null;
        }
    }

    private IEnumerator GoBack(MovePlayer _movePlayer, string moveNumber)
    {
        yield return new WaitForSecondsRealtime(1f);

        InstantiateItems.SpawnRandomObject(this.collectables, this.gameObject);

        _movePlayer.MovePlayerOnPuroposeBackward(moveNumber, this.gameObject);

        Destroy(this.gameObject);
    }

    private IEnumerator GoForward(MovePlayer _movePlayer, string moveNumber)
    {
        yield return new WaitForSecondsRealtime(1f);
        Debug.Log("kurec");

        InstantiateItems.SpawnRandomObject(this.collectables, this.gameObject);

        _movePlayer.MovePlayerOnPuroposeForward(moveNumber, this.gameObject);
        Destroy(this.gameObject);
    }

    public void EnableCollectable()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
    }
}
