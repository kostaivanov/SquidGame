using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlusMinusController : MonoBehaviour, ICollectable
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
            if (movePlayer.gameObject.name.StartsWith("B"))
            {
                movePlayer.collectableFound = false;
                Activate();
                StartCoroutine(Deactivate());
            }
            if (movePlayer.gameObject.name.StartsWith("R"))
            {
                movePlayer.collectableFound = false;
                Activate();
            }
        }
    }

    public IEnumerator Deactivate()
    {
        yield return new WaitForSecondsRealtime(1f);

        this.GetComponent<SpriteRenderer>().enabled = false;
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;

        GameObject[] _boxes = GameObject.FindGameObjectsWithTag("Platform");

        InstantiateItems.Shuffle(_boxes, this.gameObject);
    }


    public void Activate()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

    }
}
