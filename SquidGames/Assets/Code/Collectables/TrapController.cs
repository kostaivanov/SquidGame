using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour, ICollectable
{
    private const int numberOfMoves = 2;

    private MovePlayer movePlayer;
    [SerializeField] private GameObject[] collectables;
    private List<Collider2D> colliders;
    // Start is called before the first frame update
    void Start()
    {
        colliders = new List<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            if (!colliders.Contains(otherObject))
            {
                colliders.Add(otherObject);
            }
            if (colliders.Count > 0)
            {
                if (colliders[0].gameObject.name == otherObject.gameObject.name)
                {
                    movePlayer = colliders[0].GetComponent<MovePlayer>();
                    //movePlayer.trap = true;
                    Debug.Log("moving trap = " + otherObject.gameObject.name);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D otherObject)
    {
        //&& movePlayer.collectableFound == true
        if (movePlayer != null  && movePlayer.move == false)
        {
            //movePlayer.collectableFound = false;

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
            //movePlayer.trap = false;
            movePlayer = null;
        }
    }

    private IEnumerator CallMovementFunciton(string trapTag, MovePlayer _movePlayer, int moveNumber)
    {
        yield return new WaitForSecondsRealtime(1f);
        //_movePlayer.trap = false;
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
