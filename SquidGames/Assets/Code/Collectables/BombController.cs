using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour, ICollectable
{
    private MovePlayer movePlayer;

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
            if (movePlayer.gameObject.name.StartsWith("B") )
            {
                movePlayer.collectableFound = false;
                Activate();
            }
            if (movePlayer.gameObject.name.StartsWith("R") )
            {
                movePlayer.collectableFound = false;
                Activate();
            }
        }
    }

    public void Activate()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
    }
}
