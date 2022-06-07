using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlusMinusController : MonoBehaviour, ICollectable
{
    //[SerializeField] private GameObject[] collectables;
    private List<MovePlayer> movePlayerList;
    private List<Collider2D> colliders;
    private Collider2D collider2D;
    private bool entered;

    void Start()
    {
        colliders = new List<Collider2D>();
        collider2D = GetComponent<Collider2D>();
        movePlayerList = new List<MovePlayer>();
        entered = false;
    }

    private void OnEnable()
    {
        //collider2D.enabled = true;
    }
    private void OnDisable()
    {
        collider2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            entered = true;
            if (!colliders.Contains(otherObject))
            {
                colliders.Add(otherObject);
                movePlayerList.Add(otherObject.gameObject.GetComponent<MovePlayer>());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player" && movePlayerList != null && entered == true)
        {
            foreach (MovePlayer player in movePlayerList)
            {
                if (player != null && player.collectableFound == true && player.move == false)
                {
                    player.collectableFound = false;

                    DetectIfPlusOrMinus(player);

                    Activate();
                    StartCoroutine(Deactivate());
                    player.holdsCollectable = false;
                    entered = false;
                    //Debug.Log("Ontrigger Stay Players = " + player.gameObject.name);
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
        InstantiateItems.SpawnRandomObject(this.gameObject);
        yield return new WaitForSecondsRealtime(1f);

        Destroy(this.gameObject);
    }

    public void Activate()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
    }
}
