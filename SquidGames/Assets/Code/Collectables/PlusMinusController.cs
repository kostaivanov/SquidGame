using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlusMinusController : MonoBehaviour, ICollectable
{
    [SerializeField] private GameObject[] collectables;
    private List<MovePlayer> movePlayerList;
    private List<Collider2D> colliders;
    private Collider2D collider2D;

    void Start()
    {
        colliders = new List<Collider2D>();
        collider2D = GetComponent<Collider2D>();
        movePlayerList = new List<MovePlayer>();
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            //Debug.Log(otherObject.gameObject.name + " is abt to take = " + this.gameObject.name);
            if (!colliders.Contains(otherObject))
            {
                colliders.Add(otherObject);
                movePlayerList.Add(otherObject.gameObject.GetComponent<MovePlayer>());
                //Debug.Log(otherObject.gameObject.name + " is abt to take = " + this.gameObject.name);
            }
            if (colliders.Count > 0)
            {
                foreach (MovePlayer player in movePlayerList)
                {
                    if (player.holdsCollectable == false && player.gameObject.name == otherObject.gameObject.name)
                    {
                        player.holdsCollectable = true;
                        Debug.Log("Ontrigger Enter Players = "+ player.gameObject.name + " - " + Time.realtimeSinceStartup);
                        break;
                    }
                }
                //if (colliders[0].gameObject.name == otherObject.gameObject.name)
                //{
                //    movePlayer = colliders[0].GetComponent<MovePlayer>();
                //    movePlayer.holdsCollectable = true;

                //    //movePlayer.trap = true;
                //    //Debug.Log("plus or minus = " + otherObject.gameObject.name);
                //}
            }
            //movePlayer = otherObject.GetComponent<MovePlayer>();
            //Debug.Log("trap = " + otherObject.gameObject.name);
        }
        //Debug.Log("opaa");
    }

    private void OnTriggerStay2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player" && movePlayerList != null)
        {
            foreach (MovePlayer player in movePlayerList)
            {
                Debug.Log("Ontrigger Stay Players = " + player.gameObject.name);
                if (player != null && collider2D != null && player.holdsCollectable == true && player.collectableFound == true && player.move == false)
                {
                    player.collectableFound = false;

                    DetectIfPlusOrMinus(player);

                    Activate();
                    StartCoroutine(Deactivate());
                    player.holdsCollectable = false;
                    break;
                }
            }            
        }      
    }

    private void OnTriggerExit2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            if (movePlayerList != null)
            {
                movePlayerList.Clear();
                colliders.Clear();
            }
        }
    }

    private void DetectIfPlusOrMinus(MovePlayer player)
    {
        string firstLetter = this.gameObject.name.Substring(0, 1);
        //Debug.Log(firstLetter);
        player.plusOn = false;
        player.minusOn = false;
        if (firstLetter == "+")
        {
            player.plusOn = true;
            Debug.Log(player.gameObject.name + " took the PLUS");
        }
        else if (firstLetter == "-")
        {
            Debug.Log(player.gameObject.name + " took the MINUS");
            player.minusOn = true;
        }
        collider2D.enabled = false;
    }

    public IEnumerator Deactivate()
    {
        InstantiateItems.SpawnRandomObject(this.collectables, this.gameObject);
        yield return new WaitForSecondsRealtime(1f);

        //this.GetComponent<SpriteRenderer>().enabled = false;
        //this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        //GameObject[] _boxes = GameObject.FindGameObjectsWithTag("Platform");

        //InstantiateItems.Shuffle(_boxes, this.gameObject);
        Destroy(this.gameObject);
    }

    public void Activate()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
    }
}
