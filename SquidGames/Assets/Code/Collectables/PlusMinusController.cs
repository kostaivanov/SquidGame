using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusMinusController : MonoBehaviour, ICollectable
{
    private MovePlayer movePlayer;


    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        Debug.Log("Namerihme bombaaaa11111");

        if (otherObject.gameObject.tag == "Player")
        {
            movePlayer = otherObject.GetComponent<MovePlayer>();
        }
    }

    private void OnTriggerStay2D(Collider2D otherObject)
    {
        if (movePlayer != null && movePlayer.collectableFound == true)
        {
            Debug.Log("Namerihme bombaaaa11111");
            if (movePlayer.gameObject.name.StartsWith("B"))
            {
                Debug.Log("Namerihme bombaaaa");
                movePlayer.collectableFound = false;
                Activate();
            }
            if (movePlayer.gameObject.name.StartsWith("R"))
            {
                movePlayer.collectableFound = false;
                Activate();
            }
        }
    }

    public void Activate()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

    }
}
