using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour, ICollectable
{
    private const int numberOfMoves = 2;

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
        if (movePlayer != null && movePlayer.collectableFound == true && movePlayer.move == false)
        {
            movePlayer.collectableFound = false;
            if (this.gameObject.tag == "GoForward")
            {
                if (movePlayer.move == false)
                {
                    Activate();
                }

                StartCoroutine(GoForward(movePlayer, numberOfMoves));
            }
            else if (this.gameObject.tag == "GoBackward")
            {
                if (movePlayer.move == false)
                {
                    Activate();
                    Debug.Log("playing ?????");

                }

                StartCoroutine(GoBack(movePlayer, numberOfMoves));

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

    private IEnumerator GoBack(MovePlayer _movePlayer, int moveNumber)
    {
        yield return new WaitForSecondsRealtime(1f);

        InstantiateItems.SpawnRandomObject(this.collectables, this.gameObject);

        _movePlayer.MovePlayerOnPuroposeBackward(moveNumber, _movePlayer.gameObject);

        Destroy(this.gameObject);
    }

    private IEnumerator GoForward(MovePlayer _movePlayer, int moveNumber)
    {
        yield return new WaitForSecondsRealtime(1f);

        InstantiateItems.SpawnRandomObject(this.collectables, this.gameObject);

        _movePlayer.MovePlayerOnPuroposeForward(moveNumber, _movePlayer.gameObject);
        Destroy(this.gameObject);
    }

    public void Activate()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
    }

    public IEnumerator Deactivate()
    {
        throw new System.NotImplementedException();
    }
}
