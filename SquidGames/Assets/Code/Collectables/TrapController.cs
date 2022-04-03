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
            movePlayer.trap = true;
        }
    }

    private void OnTriggerStay2D(Collider2D otherObject)
    {
        if (movePlayer != null && movePlayer.collectableFound == true && movePlayer.move == false)
        {
            movePlayer.collectableFound = false;

            if (movePlayer.move == false)
            {
                Activate();
            }

            StartCoroutine(CallMovementFunciton(this.gameObject.tag, movePlayer, numberOfMoves));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (movePlayer != null)
        {
            movePlayer.trap = false;
            movePlayer = null;
        }
    }

    private IEnumerator CallMovementFunciton(string trapTag, MovePlayer _movePlayer, int moveNumber)
    {
        yield return new WaitForSecondsRealtime(1f);

        InstantiateItems.SpawnRandomObject(this.collectables, this.gameObject);


        _movePlayer.MoveByTrapDirection(trapTag, moveNumber, _movePlayer.gameObject);

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
