using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TrapController : MonoBehaviour, ICollectable
{
    private const int numberOfMoves = 2;

    private List<MovePlayer> movePlayerList;
    [SerializeField] private GameObject[] collectables;
    private List<Collider2D> colliders;
    [SerializeField] private LayerMask collectablesLayer, trapsLayer;

    // Start is called before the first frame update
    void Start()
    {
        colliders = new List<Collider2D>();
        movePlayerList = new List<MovePlayer>();
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            //Debug.Log("moving trap = " + movePlayer.gameObject.name);

            MovePlayer currentMovePlayer = otherObject.gameObject.GetComponent<MovePlayer>();
            if (!colliders.Contains(otherObject) && currentMovePlayer.currentIndex == int.Parse(this.gameObject.transform.parent.name))
            {
                colliders.Add(otherObject);
            }
            if (colliders.Count > 0)
            {
                foreach (Collider2D coll in colliders)
                {
                    movePlayerList.Add(coll.GetComponent<MovePlayer>());
                }
                //if (colliders[0].gameObject.name == otherObject.gameObject.name)
                //{
                //    movePlayer = colliders[0].GetComponent<MovePlayer>();
                //    //movePlayer.trap = true;
                //}
            }
        }
    }

    private void OnTriggerStay2D(Collider2D otherObject)
    {
        //&& movePlayer.collectableFound == true
        if (otherObject.gameObject.tag == "Player" && movePlayerList != null)
        {
            foreach (MovePlayer p in movePlayerList)
            {
                if (p.move == false && p.trap == true)
                {
                    Debug.Log("trapy move forward or backward = " + otherObject.gameObject.name);
                    p.trap = false;

                    //if (movePlayer.move == false)
                    //{
                    Activate();
                    //}

                    StartCoroutine(CallMovementFunciton(this.gameObject.tag, p, numberOfMoves));
                }
            }

            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (movePlayerList != null && !movePlayerList.Any())
        {
            //movePlayer.trap = false;
            movePlayerList.Clear();
            //movePlayerList = null;
        }        
    }

    internal bool StayOnTopOfCollectable(LayerMask layer)
    {
        Collider2D rayCastHit = Physics2D.OverlapCircle(this.transform.position, 0.5f, layer);

        if (rayCastHit != null)
        {
            return true;
        }
        return false;
    }

    private IEnumerator CallMovementFunciton(string trapTag, MovePlayer _movePlayer, int moveNumber)
    {
        yield return new WaitForSecondsRealtime(1f);
        //_movePlayer.trap = false;
        InstantiateItems.SpawnRandomObject(this.collectables, this.gameObject);

        _movePlayer.MoveByTrapDirection(trapTag, moveNumber, _movePlayer.gameObject);
        if (movePlayerList != null)
        {
            movePlayerList.Clear();
            colliders.Clear();
            Debug.Log("exiting?");
        }
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
