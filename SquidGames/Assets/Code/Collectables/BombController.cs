using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BombController : MonoBehaviour, ICollectable, IDestroyable
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
                StartCoroutine(Explode(movePlayer.gameObject, movePlayer.startPosition));
            }
            if (movePlayer.gameObject.name.StartsWith("R") )
            {
                movePlayer.collectableFound = false;
                Activate();
                StartCoroutine(Explode(movePlayer.gameObject, movePlayer.startPosition));

            }
        }
    }

    private IEnumerator Explode(GameObject obj, Vector3 position)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Deactivate(obj);
        Restart(obj, position);
    }

    public void Activate()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Deactivate(GameObject obj)
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        foreach (Transform bodyPart in obj.transform)
        {
            bodyPart.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void Restart(GameObject obj, Vector3 position)
    {
        obj.transform.position = position;

        GameObject[] _boxes = GameObject.FindGameObjectsWithTag("Platform");
        GameObject[] _bombs = GameObject.FindGameObjectsWithTag("Bomb");

        //InstantiateItems.Shuffle(_boxes, _bombs);

        foreach (Transform bodyPart in obj.transform)
        {
            bodyPart.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (obj.name.StartsWith("B"))
        {
            obj.GetComponent<MovePlayer>().currentIndexBlue = -1;
        }
        else if(obj.name.StartsWith("B"))
        {
            obj.GetComponent<MovePlayer>().currentIndexRed = -1;

        }
    }
}
