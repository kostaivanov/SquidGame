using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlusMinusController : MonoBehaviour, ICollectable
{
    [SerializeField] private GameObject[] collectables;
    private MovePlayer movePlayer;
    private List<Collider2D> colliders;

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
            //movePlayer = otherObject.GetComponent<MovePlayer>();
            Debug.Log("trap = " + otherObject.gameObject.name);
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

            //if (movePlayer.gameObject.name.StartsWith("B"))
            //{
            //    movePlayer.collectableFound = false;
            //    DetectIfPlusOrMinus();
            //    Activate();
            //    StartCoroutine(Deactivate());
            //}
            //if (movePlayer.gameObject.name.StartsWith("R"))
            //{
            //    movePlayer.collectableFound = false;
            //    DetectIfPlusOrMinus();
            //    Activate();
            //    StartCoroutine(Deactivate());
            //}
        }
    }
    private void OnTriggerExit2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            if (movePlayer != null)
            {
                movePlayer = null;
            }
        }
    }

    private void DetectIfPlusOrMinus()
    {
        string firstLetter = this.gameObject.name.Substring(0, 1);
        Debug.Log(firstLetter);
        movePlayer.plusOn = false;
        movePlayer.minusOn = false;
        if (firstLetter == "+")
        {
            movePlayer.plusOn = true;
        }
        else if (firstLetter == "-")
        {
            movePlayer.minusOn = true;
        }
    }

    public IEnumerator Deactivate()
    {
        yield return new WaitForSecondsRealtime(1f);

        //this.GetComponent<SpriteRenderer>().enabled = false;
        //this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        //GameObject[] _boxes = GameObject.FindGameObjectsWithTag("Platform");

        //InstantiateItems.Shuffle(_boxes, this.gameObject);
        InstantiateItems.SpawnRandomObject(this.collectables, this.gameObject);
        Destroy(this.gameObject);
    }

    public void Activate()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
    }
}
