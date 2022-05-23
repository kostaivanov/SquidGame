using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlusMinusController : MonoBehaviour, ICollectable
{
    [SerializeField] private GameObject[] collectables;
    private MovePlayer movePlayer;
    private List<Collider2D> colliders;
    private Collider2D collider2D;

    void Start()
    {
        colliders = new List<Collider2D>();
        collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            Debug.Log(otherObject.gameObject.name + " is abt to take = " + this.gameObject.name);
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
                    //Debug.Log("plus or minus = " + otherObject.gameObject.name);
                }
            }
            //movePlayer = otherObject.GetComponent<MovePlayer>();
            //Debug.Log("trap = " + otherObject.gameObject.name);
        }
        //Debug.Log("opaa");
    }

    private void OnTriggerStay2D(Collider2D otherObject)
    {
        if (movePlayer != null && movePlayer.collectableFound == true && movePlayer.move == false)
        {
            movePlayer.collectableFound = false;
            DetectIfPlusOrMinus();
            Activate();
            StartCoroutine(Deactivate());
        }
    }
    private void OnTriggerExit2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            if (movePlayer != null)
            {
                movePlayer = null;
                colliders.Clear();
            }
        }
    }

    private void DetectIfPlusOrMinus()
    {
        string firstLetter = this.gameObject.name.Substring(0, 1);
        //Debug.Log(firstLetter);
        movePlayer.plusOn = false;
        movePlayer.minusOn = false;
        if (firstLetter == "+")
        {
            movePlayer.plusOn = true;
            Debug.Log(movePlayer.gameObject.name + " took the PLUS");
        }
        else if (firstLetter == "-")
        {
            Debug.Log(movePlayer.gameObject.name + " took the MINUS");
            movePlayer.minusOn = true;
        }
    }

    public IEnumerator Deactivate()
    {
        collider2D.enabled = false;
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
